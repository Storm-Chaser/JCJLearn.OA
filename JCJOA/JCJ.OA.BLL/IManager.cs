 

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
	
	public partial class ActionInfoService :BaseService<ActionInfo>,IActionInfoService
    {
        public override void SetCurrentDal()
        {
			CurrentDal = this.DbSession.ActionInfoDal;
        }
    }   
	
	public partial class ArticelService :BaseService<Articel>,IArticelService
    {
        public override void SetCurrentDal()
        {
			CurrentDal = this.DbSession.ArticelDal;
        }
    }   
	
	public partial class ArticelClassService :BaseService<ArticelClass>,IArticelClassService
    {
        public override void SetCurrentDal()
        {
			CurrentDal = this.DbSession.ArticelClassDal;
        }
    }   
	
	public partial class ArticelCommentService :BaseService<ArticelComment>,IArticelCommentService
    {
        public override void SetCurrentDal()
        {
			CurrentDal = this.DbSession.ArticelCommentDal;
        }
    }   
	
	public partial class DepartmentService :BaseService<Department>,IDepartmentService
    {
        public override void SetCurrentDal()
        {
			CurrentDal = this.DbSession.DepartmentDal;
        }
    }   
	
	public partial class PhotoClassService :BaseService<PhotoClass>,IPhotoClassService
    {
        public override void SetCurrentDal()
        {
			CurrentDal = this.DbSession.PhotoClassDal;
        }
    }   
	
	public partial class PhotoInfoService :BaseService<PhotoInfo>,IPhotoInfoService
    {
        public override void SetCurrentDal()
        {
			CurrentDal = this.DbSession.PhotoInfoDal;
        }
    }   
	
	public partial class R_UserInfo_ActionInfoService :BaseService<R_UserInfo_ActionInfo>,IR_UserInfo_ActionInfoService
    {
        public override void SetCurrentDal()
        {
			CurrentDal = this.DbSession.R_UserInfo_ActionInfoDal;
        }
    }   
	
	public partial class RoleInfoService :BaseService<RoleInfo>,IRoleInfoService
    {
        public override void SetCurrentDal()
        {
			CurrentDal = this.DbSession.RoleInfoDal;
        }
    }   
	
	public partial class SensitiveWordService :BaseService<SensitiveWord>,ISensitiveWordService
    {
        public override void SetCurrentDal()
        {
			CurrentDal = this.DbSession.SensitiveWordDal;
        }
    }   
	
	public partial class UserInfoService :BaseService<UserInfo>,IUserInfoService
    {
        public override void SetCurrentDal()
        {
			CurrentDal = this.DbSession.UserInfoDal;
        }
    }   
	
}