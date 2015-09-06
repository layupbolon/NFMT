using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.User
{
    public static class MenuProvider
    {
        public static Dictionary<int, string> menuOperateList = new Dictionary<int, string>();

        public static string GetMenuWithOperateItem(Common.UserModel user,int empId,string menuIds)
        {
            string returnStr = string.Empty;
            //lock (menuOperateList)
            //{
            //    if (menuOperateList.ContainsKey(empId))
            //        returnStr = menuOperateList[empId];
            //    else
            //    {
                    BLL.MenuBLL bll = new BLL.MenuBLL();
                    Common.ResultModel result = bll.GetMenuWithOperateItem(user, empId, menuIds);
                    if (result.ResultStatus != 0)
                        return string.Empty;

                    returnStr = result.ReturnValue.ToString();
            //        menuOperateList.Add(empId, returnStr);
            //    }
            //}

            return returnStr;
        }

        public static void RefreshByEmpId(int empId)
        {
            if (menuOperateList != null)
            {
                foreach (KeyValuePair<int, string> kvp in menuOperateList)
                {
                    if (kvp.Key == empId)
                        menuOperateList.Remove(empId);
                }
            }
        }

        public static void RefreshMenus()
        {
            if (menuOperateList != null)
            {
                menuOperateList.Clear();
            }
        }
    }
}
