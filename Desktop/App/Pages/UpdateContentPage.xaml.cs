﻿using Microsoft.UI;
using Microsoft.UI.Xaml;
using System.Text;
using Windows.ApplicationModel;
using Windows.System;
using Windows.UI;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Xunkong.Desktop.Pages;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class UpdateContentPage : Microsoft.UI.Xaml.Controls.Page
{


    private Version ThisVersion = XunkongEnvironment.AppVersion;

    private readonly GithubService githubService;


    public UpdateContentPage()
    {
        this.InitializeComponent();
        githubService = ServiceProvider.GetService<GithubService>()!;
        Loaded += UpdateContentPage_Loaded;
    }

    private async void UpdateContentPage_Loaded(object sender, RoutedEventArgs e)
    {
        await InitializeCommand.ExecuteAsync(null);
    }


    [RelayCommand]
    private async Task InitializeAsync()
    {
        try
        {
            var sb = new StringBuilder();
            var releases = await githubService.GetReleaseAsync(1, 30);
            if (XunkongEnvironment.IsStoreVersion)
            {
                releases = releases.Where(x => x.Prerelease == false).ToList();
            }
            int count = 10;
            foreach (var release in releases)
            {
                if (Version.TryParse(release.TagName, out var version))
                {
                    if (version <= ThisVersion)
                    {
                        sb.AppendLine($"# {release.TagName} {release.Name}");
                        sb.AppendLine();
                        sb.AppendLine($"> 更新于 {release.PublishedAt.LocalDateTime:yyyy-MM-dd HH:mm:ss}");
                        sb.AppendLine();
                        sb.AppendLine(release.Body);
                        sb.AppendLine();
                        if (--count == 0)
                        {
                            break;
                        }
                    }
                }
            }

            if (MainWindow.Current.IsMicaOrAcrylic())
            {
                webview.DefaultBackgroundColor = Colors.Transparent;
            }
            else
            {
                webview.DefaultBackgroundColor = MainWindow.Current.ActualTheme is ElementTheme.Light ? Color.FromArgb(0xFF, 0xF3, 0xF3, 0xF3) : Color.FromArgb(0xFF, 0x20, 0x20, 0x20);
            }

            var html = Markdig.Markdown.ToHtml(sb.ToString());
            var theme = MainWindow.Current.ActualTheme;
            var path = theme switch
            {
                ElementTheme.Light => Path.Combine(Package.Current.InstalledLocation.Path, @"Assets\Others\github-markdown-light_5.1.0.css"),
                ElementTheme.Dark => Path.Combine(Package.Current.InstalledLocation.Path, @"Assets\Others\github-markdown-dark_5.1.0.css"),
                _ => Path.Combine(Package.Current.InstalledLocation.Path, @"Assets\Others\github-markdown_5.1.0.css"),
            };
            var css = await File.ReadAllTextAsync(path);
            html = $$"""
                <!DOCTYPE html>
                <html>
                <head>
                <base target="_blank">
                <meta name="color-scheme" content="light dark">
                <style>
                body::-webkit-scrollbar {display: none;}
                {{css}}
                </style>
                </head>
                <body style="background-color: {{(theme is ElementTheme.Light ? "#FFFFFF40" : "#FFFFFF09")}};">
                <br>
                <article class="markdown-body" style="background-color: transparent;">
                {{html}}
                </article>
                <br>
                </body>
                </html>
                """;
            await webview.EnsureCoreWebView2Async();
            webview.CoreWebView2.Settings.AreDevToolsEnabled = false;
            webview.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;
            webview.CoreWebView2.Settings.AreBrowserAcceleratorKeysEnabled = false;
            webview.NavigateToString(html);
            AppSetting.SetValue(SettingKeys.LastVersion, ThisVersion);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "更新内容");
            NotificationProvider.Error(ex, "更新内容");
        }
    }


    [RelayCommand]
    private async Task OpenReleaseHistoryPageAsync()
    {
        await Launcher.LaunchUriAsync(new("https://github.com/xunkong/xunkong/releases"));
    }


    [RelayCommand]
    private void NotShowThisVersion()
    {
        AppSetting.SetValue(SettingKeys.LastVersion, ThisVersion);
        NotificationProvider.Success("此版本不再显示此页面");
    }


}
