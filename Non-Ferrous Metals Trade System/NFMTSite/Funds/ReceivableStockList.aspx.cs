﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Funds
{
    public partial class ReceivableStockList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //this.navigation1.Routes.Add("库存收款分配", string.Empty);
            }
        }
    }
}