using JCJ.OA.IBLL;
using JCJ.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCJ.OA.BLL
{
    public partial class RoleInfoService:BaseService<RoleInfo>,IRoleInfoService
    {
        /// <summary>
        /// 完成角色权限的分配
        /// </summary>
        /// <param name="roleId">角色编号</param>
        /// <param name="actionIdList">要分配的权限的编号集合</param>
        /// <returns></returns>
        public bool SetRoleActionInfo(int roleId, List<int> actionIdList)
        {
            var roleInfo = this.DbSession.RoleInfoDal.LoadEntities(r => r.ID == roleId).FirstOrDefault();
            if (roleInfo != null)
            {
                roleInfo.ActionInfo.Clear();
                foreach (int actionId in actionIdList)
                {
                    var actionInfo = this.DbSession.ActionInfoDal.LoadEntities(a => a.ID == actionId).FirstOrDefault();
                    roleInfo.ActionInfo.Add(actionInfo);
                }
                return this.DbSession.SaveChanges();
            }
            return false;
        }
    }
}
