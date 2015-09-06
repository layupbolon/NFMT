using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.App_Code
{
    public class Utility
    {
        public void WriteSession()
        {
        }

        public static NFMT.Common.UserModel GetUser()
        {
            NFMT.Common.UserModel user = null;

            if (HttpContext.Current.Session["nfmt_user"] != null)
                user = HttpContext.Current.Session["nfmt_user"] as NFMT.Common.UserModel;

            return user;
        }
    }
}