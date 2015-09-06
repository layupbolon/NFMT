using System;

namespace NFMT.Common
{
    public class DefaultValue
    {
        ////开发环境
        //private const string domain = "maikegroup.com";
        //private const string _cookie = "Test";
        //private const string nfmtSiteName = "http://site.maikegroup.com/";
        //private const string nfmtPassPort = "http://passport.maikegroup.com/";
        //private const string systemName = "业务管理系统(" + sign + ")";
        //private const string _V1ConnectString = "Data Source=192.168.13.205;Initial Catalog=BussinessNow;User ID=sa;Password=7788250";
        //private const string sign = "本地";

        ////头寸管理系统--测试
        //private const string domain = "101.231.199.229";
        //private const string _cookie = "Fin";
        //private const string nfmtSiteName = "http://101.231.199.229/FinSite/";
        //private const string nfmtPassPort = "http://101.231.199.229/FinPS/";
        //private const string systemName = "迈科融资头寸管理系统(" + sign + ")";
        //private const string _V1ConnectString = "Data Source=192.168.13.205;Initial Catalog=BussinessNow;User ID=sa;Password=7788250";
        //private const string sign = "测试";

        ////头寸管理系统--正式
        //private const string domain = "101.231.199.233";
        //private const string _cookie = "Fin";
        //private const string nfmtSiteName = "http://101.231.199.233:21650/FinSite/";
        //private const string nfmtPassPort = "http://101.231.199.233:21650/FinPS/";
        //private const string systemName = "迈科融资头寸管理系统(" + sign + ")";
        //private const string _V1ConnectString = "Data Source=192.168.18.251;Initial Catalog=BusinessNow;User Id=sa;Password=mk7788250;Application Name=BusinessNow";
        //private const string sign = "正式";

        ////迈科资产V2
        //private const string domain = "192.168.18.43";
        //private const string _cookie = "MKZC";
        //private const string nfmtSiteName = "http://192.168.18.43/site/";
        //private const string nfmtPassPort = "http://192.168.18.43/passport/";
        //private const string systemName = "有色金属业务管理系统(" + sign + ")";
        //private const string _V1ConnectString = "Data Source=192.168.13.205;Initial Catalog=BussinessNow;User ID=sa;Password=7788250";
        //private const string sign = "测试";

        //迈科资产V2测试环境
        private const string domain = "maikegroup.com";
        private const string _cookie = "Test";
        private const string nfmtSiteName = "http://site.maikegroup.com/";
        private const string nfmtPassPort = "http://passport.maikegroup.com/";
        private const string systemName = "迈科资产(" + sign + ")";
        private const string _V1ConnectString = "Data Source=192.168.13.205;Initial Catalog=BussinessNow;User ID=sa;Password=7788250";
        private const string sign = "本地";

        private static DateTime defaultTime = DateTime.Parse("1900-01-01");
        private static System.Data.DataTable defaultTable = new System.Data.DataTable();
        private const double cacheExpiration = 480;
        private const string cookieName = _cookie + "_COOKIENAME";
        private const string fileUrl = "http://127.0.0.1/8888/Files";
        private const string filePath = "~/Files";
        private const string defaultPassword = "maike123";
        private const string urlCookieName = "urlCookie";

        public static string Sign
        {
            get { return sign; }
        }

        /// <summary>
        /// 系统默认密码
        /// </summary>
        public static string DefaultPassword
        {
            get { return defaultPassword; }
        }

        /// <summary>
        /// 系统名称
        /// </summary>
        public static string SystemName
        {
            get { return systemName; }
        }

        /// <summary>
        /// V1数据库链接
        /// </summary>
        public static string V1ConnectString
        {
            get { return _V1ConnectString; }
        }

        /// <summary>
        /// 默认时间
        /// 默认值为:1800-01-01
        /// </summary>
        public static DateTime DefaultTime
        {
            get { return defaultTime; }
        }

        /// <summary>
        /// 默认DataTable
        /// 默认值为空Table
        /// </summary>
        public static System.Data.DataTable DefaultTable
        {
            get { return defaultTable; }
        }

        /// <summary>
        /// 系统用户
        /// </summary>
        public static UserModel SysUser
        {
            get
            {
                UserModel user = new UserModel();

                user.AccountId = 1;
                user.AccountName = "System";
                user.EmpName = "系统用户";
                user.EmpId = 1;

                return user;
            }
        }

        /// <summary>
        /// Cache过期时间
        /// </summary>
        public static double CacheExpiration
        {
            get { return cacheExpiration; }
        }

        /// <summary>
        /// Cookie名称
        /// </summary>
        public static string CookieName
        {
            get { return cookieName; }
        }

        /// <summary>
        /// UrlCookie名称
        /// </summary>
        public static string UrlCookieName
        {
            get { return urlCookieName; }
        }

        /// <summary>
        /// 文件查看Url
        /// </summary>
        public static string FileUrl
        {
            get { return fileUrl; }
        }

        /// <summary>
        /// 文件上传至服务端的路径
        /// </summary>
        public static string FilePath
        {
            get { return filePath; }
        }

        public static string ImgUrl
        {
            get { return nfmtSiteName; }
        }

        public static string Domain
        {
            get { return domain; }
        }

        public static string NftmSiteName
        {
            get { return nfmtSiteName; }
        }

        public static string NfmtSiteName
        {
            get { return nfmtSiteName; }
        }

        public static string NfmtPassPort
        {
            get { return nfmtPassPort; }
        }

        private static BasicAuth clearAuth = new BasicAuth();
        public static BasicAuth ClearAuth
        {
            get { return clearAuth; }
        }
    }
}
