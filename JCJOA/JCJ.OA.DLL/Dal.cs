 

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
		
	public partial class ArticelDal :BaseDal<Articel>,IArticelDal
    {

    }
		
	public partial class ArticelClassDal :BaseDal<ArticelClass>,IArticelClassDal
    {

    }
		
	public partial class ArticelCommentDal :BaseDal<ArticelComment>,IArticelCommentDal
    {

    }
		
	public partial class DepartmentDal :BaseDal<Department>,IDepartmentDal
    {

    }
		
	public partial class KeyWordsRankDal :BaseDal<KeyWordsRank>,IKeyWordsRankDal
    {

    }
		
	public partial class PhotoClassDal :BaseDal<PhotoClass>,IPhotoClassDal
    {

    }
		
	public partial class PhotoInfoDal :BaseDal<PhotoInfo>,IPhotoInfoDal
    {

    }
		
	public partial class R_UserInfo_ActionInfoDal :BaseDal<R_UserInfo_ActionInfo>,IR_UserInfo_ActionInfoDal
    {

    }
		
	public partial class RoleInfoDal :BaseDal<RoleInfo>,IRoleInfoDal
    {

    }
		
	public partial class SearchDetailsDal :BaseDal<SearchDetails>,ISearchDetailsDal
    {

    }
		
	public partial class SensitiveWordDal :BaseDal<SensitiveWord>,ISensitiveWordDal
    {

    }
		
	public partial class UserInfoDal :BaseDal<UserInfo>,IUserInfoDal
    {

    }
		
	public partial class VideoClassDal :BaseDal<VideoClass>,IVideoClassDal
    {

    }
		
	public partial class VideoFileInfoDal :BaseDal<VideoFileInfo>,IVideoFileInfoDal
    {

    }
	
}