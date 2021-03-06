﻿ 

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCJ.OA.IDAL
{
	public partial interface IDBSession
    {

	
		IActionInfoDal ActionInfoDal{get;set;}
	
		IArticelDal ArticelDal{get;set;}
	
		IArticelClassDal ArticelClassDal{get;set;}
	
		IArticelCommentDal ArticelCommentDal{get;set;}
	
		IDepartmentDal DepartmentDal{get;set;}
	
		IKeyWordsRankDal KeyWordsRankDal{get;set;}
	
		IPhotoClassDal PhotoClassDal{get;set;}
	
		IPhotoInfoDal PhotoInfoDal{get;set;}
	
		IR_UserInfo_ActionInfoDal R_UserInfo_ActionInfoDal{get;set;}
	
		IRoleInfoDal RoleInfoDal{get;set;}
	
		ISearchDetailsDal SearchDetailsDal{get;set;}
	
		ISensitiveWordDal SensitiveWordDal{get;set;}
	
		IUserInfoDal UserInfoDal{get;set;}
	
		IVideoClassDal VideoClassDal{get;set;}
	
		IVideoFileInfoDal VideoFileInfoDal{get;set;}
	}	
}