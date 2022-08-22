﻿using Microsoft.UI.Xaml;
using Octokit;
using System.Text;
using Windows.ApplicationModel;
using Windows.System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Xunkong.Desktop.Pages;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class UpdateContentPage : Microsoft.UI.Xaml.Controls.Page
{


    private Version? lastVersion;

    private Version thisVersion = XunkongEnvironment.AppVersion;


    public UpdateContentPage()
    {
        this.InitializeComponent();
        Loaded += UpdateContentPage_Loaded;
    }

    private async void UpdateContentPage_Loaded(object sender, RoutedEventArgs e)
    {
        if (Version.TryParse(AppSetting.GetValue(SettingKeys.LastVersion, "", false), out var version))
        {
            lastVersion = version;
        }
        await InitializeCommand.ExecuteAsync(null);
    }


    [RelayCommand]
    private async Task InitializeAsync()
    {
        try
        {
            var client = new GitHubClient(new ProductHeaderValue("xunkong"));
            var sb = new StringBuilder();
            if (lastVersion is null || lastVersion >= thisVersion)
            {
                var release = await client.Repository.Release.Get("xunkong", "xunkong", thisVersion.ToString(3));
                sb.AppendLine($"# {release.TagName} {release.Name}");
                sb.AppendLine();
                sb.AppendLine($"> 更新于 {release.PublishedAt:yyyy-MM-dd HH:mm:ss}");
                sb.AppendLine();
                sb.AppendLine(release.Body);
                sb.AppendLine();
            }
            else
            {
                var releases = await client.Repository.Release.GetAll("xunkong", "xunkong", new ApiOptions { PageCount = 1, PageSize = 10, StartPage = 1 });
                foreach (var release in releases)
                {
                    if (Version.TryParse(release.TagName, out var version))
                    {
                        if (lastVersion < version && version <= thisVersion)
                        {
                            sb.AppendLine($"# {release.TagName} {release.Name}");
                            sb.AppendLine();
                            sb.AppendLine($"> 更新于 {release.PublishedAt:yyyy-MM-dd HH:mm:ss}");
                            sb.AppendLine();
                            sb.AppendLine(release.Body);
                            sb.AppendLine();
                        }
                    }
                }
            }
            var html = Markdig.Markdown.ToHtml(sb.ToString());
            var css = await File.ReadAllTextAsync(Path.Combine(Package.Current.InstalledPath, @"Assets\Others\github-markdown_5.1.0.css"));
            html = $$"""
                <head>
                <base target="_blank">
                <meta name="color-scheme" content="light dark">
                <style>
                body::-webkit-scrollbar {display: none;}
                {{css}}
                </style>
                </head>
                <body style="background-color: transparent;">
                <br>
                <article class="markdown-body" style="background-color: transparent;">
                {{html}}
                </article>
                <br>
                </body>
                """;
            await webview.EnsureCoreWebView2Async();
            webview.CoreWebView2.Settings.AreDevToolsEnabled = false;
            webview.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;
            webview.CoreWebView2.Settings.AreBrowserAcceleratorKeysEnabled = false;
            webview.NavigateToString(html);
            AppSetting.TrySetValue(SettingKeys.ShowUpdateContentOnLoaded, false);
            AppSetting.TrySetValue(SettingKeys.LastVersion, thisVersion.ToString());
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


}