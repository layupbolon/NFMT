/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：DetailCollection.cs
// 文件功能描述：BdStyleDetail集合。
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
    public class DetailCollection : ICollection<Model.BDStyleDetail>
    {
        List<Model.BDStyleDetail> details = new List<Model.BDStyleDetail>();

        /// <summary>
        /// 通过DetailCode获取NFMT.Data.Model.BDStyleDetail对象
        /// </summary>
        /// <param name="detailCode"></param>
        /// <returns></returns>
        public Model.BDStyleDetail this[string detailCode]
        {
            get
            {
                foreach (Model.BDStyleDetail detail in details)
                {
                    if (detail.DetailCode == detailCode)
                    {
                        return detail;
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// 通过DetailId获取NFMT.Data.Model.BDStyleDetail对象
        /// </summary>
        /// <param name="detailId"></param>
        /// <returns></returns>
        public Model.BDStyleDetail this[int detailId]
        {
            get
            {
                foreach (Model.BDStyleDetail detail in details)
                {
                    if (detail.StyleDetailId == detailId)
                    {
                        return detail;
                    }
                }

                return null;
            }
        }
        /// <summary>
        /// 添加NFMT.Data.Model.BDStyleDetail对象至集合中
        /// </summary>
        /// <param name="item"></param>
        public void Add(Model.BDStyleDetail item)
        {
            details.Add(item);
        }

        /// <summary>
        /// 清除集合所有包含的NFMT.Data.Model.BDStyleDetail
        /// </summary>
        public void Clear()
        {
            details.Clear();
        }

        public bool Contains(Model.BDStyleDetail item)
        {
            return details.Contains(item);
        }

        public void CopyTo(Model.BDStyleDetail[] array, int arrayIndex)
        {
            details.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return details.Count; }
        }

        bool ICollection<Model.BDStyleDetail>.IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(Model.BDStyleDetail item)
        {
            return details.Remove(item);
        }

        public IEnumerator<Model.BDStyleDetail> GetEnumerator()
        {
            return details.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return details.GetEnumerator();
        }
    }
}
