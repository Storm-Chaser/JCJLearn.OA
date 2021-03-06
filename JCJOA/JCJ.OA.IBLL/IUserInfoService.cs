﻿using JCJ.OA.Model;
using JCJ.OA.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCJ.OA.IBLL
{
    public partial interface IUserInfoService:IBaseService<UserInfo>
    {
        bool DeleteEntities(List<int> list);

        IQueryable<UserInfo> LoadSearchEntities(UserInfoSearch userInfoSearch);

        bool SetUserRoleInfo(int userId, List<int> roleIdList);

        bool SetUserActionInfo(int userId, int actionId, bool isPass);
    }
}
