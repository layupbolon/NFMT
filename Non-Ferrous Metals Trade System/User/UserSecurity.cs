using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.User
{
    public class UserSecurity : Common.UserModel
    {
        private Model.Corporation corp;
        private Model.Department dept;
        private List<Data.Model.Asset> assets;
        private Dictionary<Data.StyleEnum, Data.DetailCollection> authValues;
        private List<Model.AuthGroup> authGroups;
        private List<Model.Role> roles;
        private List<Model.Menu> menus;
        private List<Model.AuthOption> authOptions;
        private List<Model.AuthOptionDetail> authOptionDetails;
        private List<Model.AuthOptionDetailEmpRef> refs;

        public UserSecurity()
        {
            corp = new Model.Corporation();
            dept = new Model.Department();
            assets = new List<Data.Model.Asset>();
            authValues = new Dictionary<Data.StyleEnum, Data.DetailCollection>();
            authGroups = new List<Model.AuthGroup>();
            roles = new List<Model.Role>();
            menus = new List<Model.Menu>();
            authOptions = new List<Model.AuthOption>();
            authOptionDetails = new List<Model.AuthOptionDetail>();
            refs = new List<Model.AuthOptionDetailEmpRef>();
        }

        public NFMT.Common.UserModel User { get; set; }

        /// <summary>
        /// 集团
        /// </summary>
        public Model.Bloc Bloc { get; set; }

        /// <summary>
        /// 公司列表
        /// </summary>
        public Model.Corporation Corp
        {
            get { return this.corp; }
            set { this.corp = value; }
        }

        /// <summary>
        /// 部门列表
        /// </summary>
        public Model.Department Dept
        {
            get { return this.dept; }
            set { this.dept = value; }
        }

        /// <summary>
        /// 员工
        /// </summary>
        public Model.Employee Emp { get; set; }

        /// <summary>
        /// 账户
        /// </summary>
        public Model.Account Account { get; set; }

        /// <summary>
        /// 品种列表
        /// </summary>
        public List<Data.Model.Asset> Assets
        {
            get { return this.assets; }
            set { this.assets = value; }
        }

        /// <summary>
        /// 权限值词典
        /// </summary>
        public Dictionary<Data.StyleEnum, Data.DetailCollection> AuthValues
        {
            get { return this.authValues; }
            set { this.authValues = value; }
        }

        /// <summary>
        /// 权限组列表
        /// </summary>
        public List<Model.AuthGroup> AuthGroups
        {
            get { return this.authGroups; }
            set { this.authGroups = value; }
        }

        /// <summary>
        /// 角色列表
        /// </summary>
        public List<Model.Role> Roles
        {
            get { return this.roles; }
            set { this.roles = value; }
        }

        /// <summary>
        /// 菜单列表
        /// </summary>
        public List<Model.Menu> Menus
        {
            get { return this.menus; }
            set { this.menus = value; }
        }

        public List<Model.AuthOption> AuthOptions
        {
            get { return this.authOptions; }
            set { this.authOptions = value; }
        }

        public List<Model.AuthOptionDetail> AuthOptionDetails
        {
            get { return this.authOptionDetails; }
            set { this.authOptionDetails = value; }
        }

        public List<Model.AuthOptionDetailEmpRef> Refs
        {
            get { return this.refs; }
            set { this.refs = value; }
        }
    }
}
