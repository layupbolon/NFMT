using System;
using System.Xml;
using NFMT.Common;
using NFMT.PassPort.DAL;

namespace NFMT.PassPort
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“UserService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 UserService.svc 或 UserService.svc.cs，然后开始调试。
    public class UserService : IUserService
    {
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(UserService));

        /// <summary>
        /// 登录,返回用户对象
        /// </summary>
        /// <param name="accountName">用户名</param>
        /// <param name="passWord">密码</param>
        /// <returns></returns>
        public ResultModel Login(string accountName, string passWord)
        {
            try
            {
                User.BLL.AccountBLL bll = new User.BLL.AccountBLL();
                ResultModel result = bll.CheckLogin(accountName, passWord);
                if (result.ResultStatus == 0 && result.ReturnValue != null)
                {
                    int i;
                    if (int.TryParse(result.ReturnValue.ToString(), out i) && i == 1)
                    {
                        //生成唯一token,写Cache
                        string token = CreateToken();
                        CacheManager.CacheInsert(token, accountName);

                        result.ReturnValue = token;
                        //result.ReturnValue = User.UserProvider.GetUserModel(token, accountName);
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                this.log.ErrorFormat(ex.Message);
                return null;
            }
            
        }

        /// <summary>
        /// 检查用户是否已登录
        /// </summary>
        /// <param name="token">票据</param>
        /// <returns></returns>
        public ResultModel CheckLoginStatus(string token)
        {
            try
            {
                ResultModel result = new ResultModel();

                //根据token获取Cache的中对应的UserModel并返回，若token不存在，则未登录用户。
                string value = CacheManager.GetCacheValue(token);
                if (string.IsNullOrEmpty(value))
                {
                    result.Message = "用户信息获取失败";
                    result.ResultStatus = -1;
                }
                else
                {
                    result.Message = "用户信息获取成功";
                    result.ResultStatus = 0;
                    result.ReturnValue = value;
                }

                return result;
            }
            catch (Exception ex)
            {
                this.log.ErrorFormat(ex.Message);
                return null;
            }
            
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <param name="token">票据</param>
        /// <returns></returns>
        public ResultModel LoginOut(string token)
        {
            //清除token对应的所有Cache
            CacheManager.DeleteCache(token);

            return new ResultModel { ResultStatus = 0 };
        }

        private string CreateToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
