﻿ 

using JCJ.OA.IDAL;
using JCJ.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JCJ.OA.DLL
{
		
	public partial class ActionInfoDal :BaseDal<ActionInfo>,IActionInfoDal
    {

    }
		
	public partial class DepartmentDal :BaseDal<Department>,IDepartmentDal
    {

    }
		
	public partial class R_UserInfo_ActionInfoDal :BaseDal<R_UserInfo_ActionInfo>,IR_UserInfo_ActionInfoDal
    {

    }
		
	public partial class RoleInfoDal :BaseDal<RoleInfo>,IRoleInfoDal
    {

    }
		
	public partial class UserInfoDal :BaseDal<UserInfo>,IUserInfoDal
    {

    }
	
}