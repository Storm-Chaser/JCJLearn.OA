using JCJ.OA.IBLL;
using JCJ.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCJ.OA.BLL
{
    public class UserInfoService : BaseService<UserInfo>, IUserInfoService
    {
        /// <summary>
        /// 批量删除用户数据
        /// </summary>
        /// <param name="list">要删除的记录的编号</param>
        /// <returns></returns>
        public bool DeleteEntities(List<int> list)
        {
            var userInfoList = this.DbSession.userInfoDal.LoadEntities(u => list.Contains(u.ID));
            foreach(var userInfo in userInfoList)
            {
                this.DbSession.userInfoDal.DeleteEntity(userInfo);
            }
            return this.DbSession.SaveChanges();
        }

        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.userInfoDal;
        }
    }
}
