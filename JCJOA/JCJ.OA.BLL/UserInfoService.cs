using JCJ.OA.IBLL;
using JCJ.OA.Model;
using JCJ.OA.Model.Search;
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

        /// <summary>
        /// 完成用户信息搜索
        /// </summary>
        /// <param name="userInfoSearch"></param>
        /// <returns></returns>
        public IQueryable<UserInfo> LoadSearchEntities(UserInfoSearch userInfoSearch)
        {
            var temp = this.DbSession.userInfoDal.LoadEntities(u => u.DelFlag == 0);
            if (!string.IsNullOrEmpty(userInfoSearch.UName))
            {
                temp = temp.Where<UserInfo>(u => u.UName.Contains(userInfoSearch.UName));
            }
            if (!string.IsNullOrEmpty(userInfoSearch.URemark))
            {
                temp = temp.Where<UserInfo>(u => u.Remark.Contains(userInfoSearch.URemark));
            }
            userInfoSearch.TotalCount = temp.Count();
            return temp.OrderBy<UserInfo, int>(u => u.ID).Skip<UserInfo>((userInfoSearch.PageIndex - 1) * userInfoSearch.PageSize).Take<UserInfo>(userInfoSearch.PageSize); 
        }

        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.userInfoDal;
        }
    }
}
