﻿using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Windows.AppLifecycle;
using System.Net.Http;
using System.Text.Json.Nodes;
using System.Web;
using Windows.ApplicationModel.Activation;
using Windows.System;
using Xunkong.GenshinData.Achievement;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Xunkong.Desktop.Pages;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
[INotifyPropertyChanged]
public sealed partial class ImportAchievementPage : Page
{

    [ObservableProperty]
    private string? dataFrom;

    [ObservableProperty]
    private string? dataParam;

    [ObservableProperty]
    private string? exportApp;

    [ObservableProperty]
    private string? exportAppVersion;

    [ObservableProperty]
    private string? uiafVersion;

    [ObservableProperty]
    private string? exportTime;


    private List<AchievementData>? achievementDatas;

    [ObservableProperty]
    private string error;

    [ObservableProperty]
    private int achievementCount;


    private List<string> uids;


    public ImportAchievementPage()
    {
        this.InitializeComponent();
        Loaded += ImportAchievementPage_Loaded;
    }



    public ImportAchievementPage(IProtocolActivatedEventArgs args)
    {
        this.InitializeComponent();
        ToolWindow.Current.ResizeToCenter(360, 480);
        InitializeTitle(args);
        Loaded += ImportAchievementPage_Loaded;
    }




    private void InitializeTitle(IProtocolActivatedEventArgs args)
    {
        if (args is ProtocolActivatedEventArgs args1)
        {
            var qc = HttpUtility.ParseQueryString(args1.Uri.Query);
            var caller = qc["caller"];
            c_TextBlock_Title.Text = "导入成就";
            c_TextBlock_Caller.Text = $"调用方：{(string.IsNullOrWhiteSpace(caller) ? "未知" : caller)}";
            DataFrom = qc["from"]?.ToString();
            DataParam = qc["param"]?.ToString().Replace("\"", "").Trim();
        }
    }



    private async void ImportAchievementPage_Loaded(object sender, RoutedEventArgs e)
    {
        try
        {
            string? text = null;
            if (DataFrom?.ToLower() is "clipboard")
            {
                text = await ClipboardHelper.GetTextAsync();
            }
            if (DataFrom?.ToLower() is "file")
            {
                if (File.Exists(DataParam))
                {
                    text = await File.ReadAllTextAsync(DataParam);
                }
            }
            if (DataFrom?.ToLower() is "web")
            {
                text = await CreateHttpClient().GetStringAsync(DataParam);
            }
            ParseJsonString(text);
            if (achievementDatas?.Any() ?? false)
            {
                AchievementCount = achievementDatas.Count;
                using var dapper = DatabaseProvider.CreateConnection();
                uids = dapper.Query<string>("SELECT DISTINCT Uid FROM AchievementData;").ToList();
                c_AutoSuggestBox_Uid.ItemsSource = uids;
            }
            else
            {
                Error = "没有可导入的成就";
            }
        }
        catch (Exception ex)
        {
            Error = "数据解析失败";
            Logger.Error(ex, "成就数据解析失败");
        }
    }



    private void ParseJsonString(string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return;
        }
        var baseNode = JsonNode.Parse(text);
        if (baseNode?["info"] is JsonNode infoNode)
        {
            ExportApp = infoNode?["export_app"]?.ToString();
            ExportAppVersion = infoNode?["export_app_version"]?.ToString();
            UiafVersion = infoNode?["uiaf_version"]?.ToString();
            if (long.TryParse(infoNode?["export_timestamp"]?.ToString(), out long timestamp))
            {
                if (timestamp > int.MaxValue)
                {
                    ExportTime = DateTimeOffset.FromUnixTimeMilliseconds(timestamp).ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");

                }
                else
                {

                    ExportTime = DateTimeOffset.FromUnixTimeSeconds(timestamp).ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
                }
            }
        }
        if (baseNode?["list"] is JsonNode listNode)
        {
            achievementDatas = JsonSerializer.Deserialize<List<AchievementData>>(listNode);
        }
    }



    private HttpClient CreateHttpClient()
    {
        var handler = new HttpClientHandler { AutomaticDecompression = System.Net.DecompressionMethods.All };
        var client = new HttpClient(handler);
        client.DefaultRequestHeaders.Add("User-Agent", $"Xunkong/{XunkongEnvironment.AppName}");
        return client;
    }


    private void c_AutoSuggestBox_Uid_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
    {
        if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
        {
            if (string.IsNullOrWhiteSpace(sender.Text))
            {
                sender.ItemsSource = uids;
            }
            else
            {
                sender.ItemsSource = uids?.Where(x => x.StartsWith(sender.Text)).ToList();
            }
        }
    }


    [RelayCommand]
    private async Task ImportAchievementDataAsync()
    {
        try
        {
            if (int.TryParse(c_AutoSuggestBox_Uid.Text, out int uid))
            {
                if (achievementDatas != null)
                {
                    c_Button_Import.Content = "导入中，请稍等";
                    c_Button_Import.IsEnabled = false;
                    await Task.Delay(100);
                    var now = DateTimeOffset.Now;
                    foreach (var item in achievementDatas)
                    {
                        item.Uid = uid;
                        item.LastUpdateTime = now;
                        if (item.Status == 0)
                        {
                            item.Status = 3;
                        }
                    }
                    await Task.Run(() =>
                    {
                        using var dapper = DatabaseProvider.CreateConnection();
                        using var t = dapper.BeginTransaction();
                        dapper.Execute("""
                            INSERT INTO AchievementData (Uid, Id, Current, Status, FinishedTime, LastUpdateTime)
                            VALUES (@Uid, @Id, @Current, @Status, @FinishedTime, @LastUpdateTime)
                            ON CONFLICT DO UPDATE SET Current=@Current, Status=@Status, FinishedTime=@FinishedTime, LastUpdateTime=@LastUpdateTime;
                            """, achievementDatas, t);
                        t.Commit();
                    });
                    c_Button_Import.Content = "导入完成";
                    if (c_CheckBox_AutoRedirect.IsChecked ?? false)
                    {
                        var instance = AppInstance.FindOrRegisterForKey("Main");
                        if (instance.IsCurrent)
                        {
                            instance.UnregisterKey();
                        }
                        else
                        {
                            await Launcher.LaunchUriAsync(new Uri($"xunkong://post-message/{ProtocolMessage.ChangeSelectedUidInAchievementPage}?caller=Xunkong&uid={uid}"), new LauncherOptions { TargetApplicationPackageFamilyName = XunkongEnvironment.FamilyName });
                        }
                    }
                    await Task.Delay(1000);
                    Environment.Exit(0);
                }
            }
        }
        catch (Exception ex)
        {
            Error = ex.Message;
            Logger.Error(ex, "导入成就数据");
        }
    }
}
