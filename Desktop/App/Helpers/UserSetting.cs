﻿using System.ComponentModel;

namespace Xunkong.Desktop.Helpers;

[Obsolete("不再使用数据库存储设置项", true)]
internal static class UserSetting
{

    public static T? GetValue<T>(string key, T? defaultValue = default, bool throwError = false)
    {
        try
        {
            using var dapper = DatabaseProvider.CreateConnection();
            var value = dapper.QuerySingleOrDefault<string>("SELECT Value FROM Setting WHERE Key=@Key LIMIT 1;", new { Key = key });
            if (value is null)
            {
                return defaultValue;
            }
            try
            {
                var converter = TypeDescriptor.GetConverter(typeof(T));
                if (converter == null)
                {
                    return defaultValue;
                }
                return (T?)converter.ConvertFromString(value);
            }
            catch (NotSupportedException)
            {
                return defaultValue;
            }
        }
        catch
        {
            if (throwError)
            {
                throw;
            }
            return defaultValue;
        }
    }


    public static void SetValue<T>(string key, T value)
    {
        using var dapper = DatabaseProvider.CreateConnection();
        dapper.Execute("INSERT OR REPLACE INTO Setting (Key, Value) VALUES (@Key, @Value);", new { Key = key, Value = value?.ToString() });
    }


    public static bool TryGetValue<T>(string key, out T? result, T? defaultValue = default)
    {
        result = defaultValue;
        try
        {
            using var dapper = DatabaseProvider.CreateConnection();
            var value = dapper.QuerySingleOrDefault<string>("SELECT Value FROM Setting WHERE Key=@Key LIMIT 1;", new { Key = key });
            if (value is null)
            {
                return false;
            }
            try
            {
                var converter = TypeDescriptor.GetConverter(typeof(T));
                if (converter == null)
                {
                    return false;
                }
                result = (T?)converter.ConvertFromString(value);
                return true;
            }
            catch (NotSupportedException)
            {
                return false;
            }
        }
        catch
        {
            return false;
        }
    }


    public static bool TrySetValue<T>(string key, T value)
    {
        try
        {
            using var dapper = DatabaseProvider.CreateConnection();
            dapper.Execute("INSERT OR REPLACE INTO Setting (Key, Value) VALUES (@Key, @Value);", new { Key = key, Value = value?.ToString() });
            return true;
        }
        catch
        {
            return false;
        }
    }


}
