/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：DetailProvider.cs
// 文件功能描述：BdStyleDetail的提供者类。
// 创建人：pekah.chow
// 创建时间： 2014年6月27日
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.Data
{
    public class DetailProvider
    {
        private static Dictionary<StyleEnum, DetailCollection> detailDic;

        static DetailProvider()
        {
            detailDic = new Dictionary<StyleEnum, DetailCollection>();
        }

        /// <summary>
        /// 通过StyleEnum获取对应的明细数据
        /// </summary>
        /// <param name="style">StyleEnum对象</param>
        /// <returns>NFMT.Data.Model.BdStyleDetail集合</returns>
        public static DetailCollection Details(StyleEnum style)
        {
            if (detailDic.ContainsKey(style))
                return detailDic[style];

            RegisterDetails(style);

            return detailDic[style];
        }

        public static DetailCollection Details(int styleId)
        {
            StyleEnum style = (StyleEnum)styleId;

            return Details(style);
        }

        private static void RegisterDetails(StyleEnum style)
        {
            BLL.BDStyleBLL bll = new BLL.BDStyleBLL();

            NFMT.Common.ResultModel result = bll.Load(style);
            if (result.ResultStatus != 0)
                throw new Exception("加载类型明细失败");

            DetailCollection details = result.ReturnValue as DetailCollection;

            if (details == null)
                throw new Exception("加载类型明细失败");

            lock (detailDic)
            {
                if (!detailDic.ContainsKey(style))
                    detailDic.Add(style, details);
            }            
        }

        public static void RefreshBDStyle()
        {
            if (detailDic != null)
            {
                foreach (KeyValuePair<StyleEnum, DetailCollection> kvp in detailDic)
                {
                    if (kvp.Value != null)
                        kvp.Value.Clear();
                }
                detailDic.Clear();
            }
        }
    }
}
