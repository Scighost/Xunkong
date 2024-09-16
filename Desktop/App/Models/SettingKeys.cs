﻿namespace Xunkong.Desktop.Models;

internal abstract class SettingKeys
{
    /// <summary>
    /// 最大化窗口
    /// </summary>
    public const string IsMainWindowMaximum = nameof(IsMainWindowMaximum);

    /// <summary>
    /// 窗口位置
    /// </summary>
    public const string MainWindowRect = nameof(MainWindowRect);

    /// <summary>
    /// 最后选中的米游社uid
    /// </summary>
    [Backup]
    public const string LastSelectUserInfoUid = nameof(LastSelectUserInfoUid);

    /// <summary>
    /// 最后选中的原神uid
    /// </summary>
    [Backup]
    public const string LastSelectGameRoleUid = nameof(LastSelectGameRoleUid);

    /// <summary>
    /// 祈愿记录页面最后选中的uid
    /// </summary>
    [Backup]
    public const string LastSelectedUidInWishlogSummaryPage = nameof(LastSelectedUidInWishlogSummaryPage);

    /// <summary>
    /// 成就管理页面最后选中的uid
    /// </summary>
    [Backup]
    public const string LastSelectedUidInAchievementPage = nameof(LastSelectedUidInAchievementPage);

    /// <summary>
    /// 上个版本
    /// </summary>
    public const string LastVersion = nameof(LastVersion);

    /// <summary>
    /// 应用主题
    /// </summary>
    [Backup]
    public const string ApplicationTheme = nameof(ApplicationTheme);

    /// <summary>
    /// 导航栏是否关闭
    /// </summary>
    [Backup]
    public const string NavigationViewPaneClose = nameof(NavigationViewPaneClose);

    /// <summary>
    /// 主页壁纸
    /// </summary>
    public const string EnableHomePageWallpaper = nameof(EnableHomePageWallpaper);

    /// <summary>
    /// 显示过欢迎页面
    /// </summary>
    public const string HasShownWelcomePage = nameof(HasShownWelcomePage);

    /// <summary>
    /// 启用实时便笺通知
    /// </summary>
    [Obsolete("", true)]
    public const string EnableDailyNoteNotification = nameof(EnableDailyNoteNotification);

    /// <summary>
    /// 禁用后台任务日志
    /// </summary>
    [Obsolete("", true)]
    public const string DisableBackgroundTaskOutputLog = nameof(DisableBackgroundTaskOutputLog);

    /// <summary>
    /// 实时便笺通知，数值阈值
    /// </summary>
    [Obsolete("", true)]
    public const string DailyNoteNotification_ResinThreshold = nameof(DailyNoteNotification_ResinThreshold);

    /// <summary>
    /// 实时便笺通知，洞天宝钱阈值
    /// </summary>
    [Obsolete("", true)]
    public const string DailyNoteNotification_HomeCoinThreshold = nameof(DailyNoteNotification_HomeCoinThreshold);

    /// <summary>
    /// 签到成功通知
    /// </summary>
    [Obsolete("", true)]
    public const string DailyCheckInSuccessNotification = nameof(DailyCheckInSuccessNotification);

    /// <summary>
    /// 签到失败通知
    /// </summary>
    [Obsolete("", true)]
    public const string DailyCheckInErrorNotification = nameof(DailyCheckInErrorNotification);

    /// <summary>
    /// 游戏exe文件路径
    /// </summary>
    [Obsolete("", true)]
    public const string GameExePath = nameof(GameExePath);

    /// <summary>
    /// 目标fps
    /// </summary>
    [Backup]
    public const string TargetFPS = nameof(TargetFPS);

    /// <summary>
    /// 无边框窗口开始游戏
    /// </summary>
    [Backup]
    public const string IsPopupWindow = nameof(IsPopupWindow);

    /// <summary>
    /// 游戏窗口宽度
    /// </summary>
    public const string StartGameWindowWidth = nameof(StartGameWindowWidth);

    /// <summary>
    /// 游戏窗口高度
    /// </summary>
    public const string StartGameWindowHeight = nameof(StartGameWindowHeight);

    /// <summary>
    /// 游戏截图文件夹路径
    /// </summary>
    public const string ScreenFolderPath = nameof(ScreenFolderPath);

    /// <summary>
    /// 启动应用时自动签到
    /// </summary>
    [Backup]
    public const string SignInAllAccountsWhenStartUpApplication = nameof(SignInAllAccountsWhenStartUpApplication);

    /// <summary>
    /// 主页图片刷新时间
    /// </summary>
    [Obsolete("固定为 5s", true)]
    public const string WallpaperRefreshTime = nameof(WallpaperRefreshTime);

    /// <summary>
    /// 祈愿记录备份声明
    /// </summary>
    public const string WishlogBackupAgreement = nameof(WishlogBackupAgreement);

    /// <summary>
    /// 是否在使用按流量计费的网络时下载主页图片
    /// </summary>
    [Backup]
    public const string DownloadWallpaperOnMeteredInternet = nameof(DownloadWallpaperOnMeteredInternet);

    /// <summary>
    /// 启动应用时的原粹树脂和洞天宝钱提醒
    /// </summary>
    [Obsolete("不再使用", true)]
    public const string ResinAndHomeCoinNotificationWhenStartUp = nameof(ResinAndHomeCoinNotificationWhenStartUp);

    /// <summary>
    /// 应用启动时打开更新内容界面
    /// </summary>
    [Obsolete("", true)]
    public const string ShowUpdateContentOnLoaded = nameof(ShowUpdateContentOnLoaded);

    /// <summary>
    /// 窗口背景材质
    /// </summary>
    [Backup]
    public const string WindowBackdrop = nameof(WindowBackdrop);

    /// <summary>
    /// 壁纸存储位置
    /// </summary>
    public const string WallpaperSaveFolder = nameof(WallpaperSaveFolder);

    /// <summary>
    /// 使用自定义壁纸
    /// </summary>
    public const string UseCustomWallpaper = nameof(UseCustomWallpaper);

    /// <summary>
    /// 启用页面缓存
    /// </summary>
    [Backup]
    public const string EnableNavigationCache = nameof(EnableNavigationCache);

    /// <summary>
    /// 过场动画文件夹
    /// </summary>
    public const string CutsceneFolder = nameof(CutsceneFolder);

    /// <summary>
    /// 显示实时便笺
    /// </summary>
    [Backup]
    public const string EnableDailyNotesInHomePage = nameof(EnableDailyNotesInHomePage);

    /// <summary>
    /// 推荐的壁纸
    /// </summary>
    public const string RecommendWallpaper = nameof(RecommendWallpaper);

    /// <summary>
    /// 启用预览版
    /// </summary>
    [Backup]
    public const string EnablePrerelease = nameof(EnablePrerelease);

    /// <summary>
    /// 游戏服务器
    /// </summary>
    public const string GameServerIndex = nameof(GameServerIndex);

    /// <summary>
    /// 国服游戏路径
    /// </summary>
    public const string GameExePathCN = nameof(GameExePathCN);

    /// <summary>
    /// 国际服游戏路径
    /// </summary>
    public const string GameExePathGlobal = nameof(GameExePathGlobal);

    /// <summary>
    /// 云原神游戏路径
    /// </summary>
    public const string GameExePathCNCloud = nameof(GameExePathCNCloud);


    /// <summary>
    /// 启动游戏后检查参量质变仪和洞天宝钱
    /// </summary>
    [Backup]
    public const string CheckTransformerAndHomeCoinWhenStartGame = nameof(CheckTransformerAndHomeCoinWhenStartGame);


    /// <summary>
    /// 游戏截图备份路径
    /// </summary>
    public const string GameScreenshotBackupFolder = nameof(GameScreenshotBackupFolder);

    /// <summary>
    /// 使用旧版我的角色页面
    /// </summary>
    [Backup]
    public const string UseCharacterPageOldVersion = nameof(UseCharacterPageOldVersion);

    /// <summary>
    /// 隐藏未上线的角色
    /// </summary>
    [Backup]
    public const string HideUnusableCharacter = nameof(HideUnusableCharacter);

    /// <summary>
    /// 显示新手祈愿记录
    /// </summary>
    [Backup]
    public const string ShowNoviceWishType = nameof(ShowNoviceWishType);

    /// <summary>
    /// 磁贴刷新遇到错误时不再提醒
    /// </summary>
    public const string DoNotRemindDailyNoteTaskError = nameof(DoNotRemindDailyNoteTaskError);

    /// <summary>
    /// 自动磁贴刷新
    /// </summary>
    public const string EnableDailyNoteTask = nameof(EnableDailyNoteTask);

    /// <summary>
    /// 磁贴自动刷新时间间隔（分钟）
    /// </summary>
    public const string DailyNoteTaskTimeInterval = nameof(DailyNoteTaskTimeInterval);

    /// <summary>
    /// 允许使用 AppCenter 上传事件日志
    /// </summary>
    [Backup]
    public const string AgreeTrackEventByAppCenter = nameof(AgreeTrackEventByAppCenter);

    /// <summary>
    /// 实时便笺缓存时间
    /// </summary>
    [Backup]
    public const string DailyNoteCacheMinutes = nameof(DailyNoteCacheMinutes);

    /// <summary>
    /// 图片浏览器主题
    /// </summary>
    [Backup]
    public const string ImageViewerRequestTheme = nameof(ImageViewerRequestTheme);

    /// <summary>
    /// 留影叙佳期
    /// </summary>
    [Backup]
    public const string EnableBirthdayStarInHomePage = nameof(EnableBirthdayStarInHomePage);

    /// <summary>
    /// 即将结束的活动
    /// </summary>
    [Backup]
    public const string EnableFinishingActivityInHomePage = nameof(EnableFinishingActivityInHomePage);

    /// <summary>
    /// 壁纸图片格式
    /// </summary>
    [Backup]
    public const string WallpaperRequestFormat = nameof(WallpaperRequestFormat);

}



public class BackupAttribute : Attribute { }
