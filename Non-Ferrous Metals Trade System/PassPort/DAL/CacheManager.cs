using System;
using System.Web;

namespace NFMT.PassPort.DAL
{
    public class CacheManager
    {
        public static void CacheInsert(string key, object value)
        {
            //Insert存在相同的键会替换，无返回值
            //Add 存在相同的键会异常，返回缓存成功的对象
            //Cache的过期策略使用滑动过期
            HttpRuntime.Cache.Insert(key, value, null, DateTime.MaxValue,TimeSpan.FromMinutes(Common.DefaultValue.CacheExpiration));
        }

        public static string GetCacheValue(string key)
        {
            if (HttpRuntime.Cache[key] != null)
            {
                return HttpRuntime.Cache[key].ToString();
            }
            return string.Empty;
        }

        public static void DeleteCache(string key)
        {
            if (HttpRuntime.Cache[key] != null)
            {
                HttpRuntime.Cache.Remove(key);
            }
        }
    }
}