﻿ 

using JCJ.OA.DLL;
using JCJ.OA.IDAL;
using JCJ.OA.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCJ.OA.DALFactory
{
	public partial class DBSession : IDBSession
    {
	
		private IActionInfoDal _ActionInfoDal;
        public IActionInfoDal ActionInfoDal
        {
            get
            {
                if(_ActionInfoDal == null)
                {
                    _ActionInfoDal = AbstractFactory.CreateActionInfoDal();
                }
                return _ActionInfoDal;
            }
            set { _ActionInfoDal = value; }
        }
	
		private IArticelDal _ArticelDal;
        public IArticelDal ArticelDal
        {
            get
            {
                if(_ArticelDal == null)
                {
                    _ArticelDal = AbstractFactory.CreateArticelDal();
                }
                return _ArticelDal;
            }
            set { _ArticelDal = value; }
        }
	
		private IArticelClassDal _ArticelClassDal;
        public IArticelClassDal ArticelClassDal
        {
            get
            {
                if(_ArticelClassDal == null)
                {
                    _ArticelClassDal = AbstractFactory.CreateArticelClassDal();
                }
                return _ArticelClassDal;
            }
            set { _ArticelClassDal = value; }
        }
	
		private IArticelCommentDal _ArticelCommentDal;
        public IArticelCommentDal ArticelCommentDal
        {
            get
            {
                if(_ArticelCommentDal == null)
                {
                    _ArticelCommentDal = AbstractFactory.CreateArticelCommentDal();
                }
                return _ArticelCommentDal;
            }
            set { _ArticelCommentDal = value; }
        }
	
		private IDepartmentDal _DepartmentDal;
        public IDepartmentDal DepartmentDal
        {
            get
            {
                if(_DepartmentDal == null)
                {
                    _DepartmentDal = AbstractFactory.CreateDepartmentDal();
                }
                return _DepartmentDal;
            }
            set { _DepartmentDal = value; }
        }
	
		private IKeyWordsRankDal _KeyWordsRankDal;
        public IKeyWordsRankDal KeyWordsRankDal
        {
            get
            {
                if(_KeyWordsRankDal == null)
                {
                    _KeyWordsRankDal = AbstractFactory.CreateKeyWordsRankDal();
                }
                return _KeyWordsRankDal;
            }
            set { _KeyWordsRankDal = value; }
        }
	
		private IPhotoClassDal _PhotoClassDal;
        public IPhotoClassDal PhotoClassDal
        {
            get
            {
                if(_PhotoClassDal == null)
                {
                    _PhotoClassDal = AbstractFactory.CreatePhotoClassDal();
                }
                return _PhotoClassDal;
            }
            set { _PhotoClassDal = value; }
        }
	
		private IPhotoInfoDal _PhotoInfoDal;
        public IPhotoInfoDal PhotoInfoDal
        {
            get
            {
                if(_PhotoInfoDal == null)
                {
                    _PhotoInfoDal = AbstractFactory.CreatePhotoInfoDal();
                }
                return _PhotoInfoDal;
            }
            set { _PhotoInfoDal = value; }
        }
	
		private IR_UserInfo_ActionInfoDal _R_UserInfo_ActionInfoDal;
        public IR_UserInfo_ActionInfoDal R_UserInfo_ActionInfoDal
        {
            get
            {
                if(_R_UserInfo_ActionInfoDal == null)
                {
                    _R_UserInfo_ActionInfoDal = AbstractFactory.CreateR_UserInfo_ActionInfoDal();
                }
                return _R_UserInfo_ActionInfoDal;
            }
            set { _R_UserInfo_ActionInfoDal = value; }
        }
	
		private IRoleInfoDal _RoleInfoDal;
        public IRoleInfoDal RoleInfoDal
        {
            get
            {
                if(_RoleInfoDal == null)
                {
                    _RoleInfoDal = AbstractFactory.CreateRoleInfoDal();
                }
                return _RoleInfoDal;
            }
            set { _RoleInfoDal = value; }
        }
	
		private ISearchDetailsDal _SearchDetailsDal;
        public ISearchDetailsDal SearchDetailsDal
        {
            get
            {
                if(_SearchDetailsDal == null)
                {
                    _SearchDetailsDal = AbstractFactory.CreateSearchDetailsDal();
                }
                return _SearchDetailsDal;
            }
            set { _SearchDetailsDal = value; }
        }
	
		private ISensitiveWordDal _SensitiveWordDal;
        public ISensitiveWordDal SensitiveWordDal
        {
            get
            {
                if(_SensitiveWordDal == null)
                {
                    _SensitiveWordDal = AbstractFactory.CreateSensitiveWordDal();
                }
                return _SensitiveWordDal;
            }
            set { _SensitiveWordDal = value; }
        }
	
		private IUserInfoDal _UserInfoDal;
        public IUserInfoDal UserInfoDal
        {
            get
            {
                if(_UserInfoDal == null)
                {
                    _UserInfoDal = AbstractFactory.CreateUserInfoDal();
                }
                return _UserInfoDal;
            }
            set { _UserInfoDal = value; }
        }
	
		private IVideoClassDal _VideoClassDal;
        public IVideoClassDal VideoClassDal
        {
            get
            {
                if(_VideoClassDal == null)
                {
                    _VideoClassDal = AbstractFactory.CreateVideoClassDal();
                }
                return _VideoClassDal;
            }
            set { _VideoClassDal = value; }
        }
	
		private IVideoFileInfoDal _VideoFileInfoDal;
        public IVideoFileInfoDal VideoFileInfoDal
        {
            get
            {
                if(_VideoFileInfoDal == null)
                {
                    _VideoFileInfoDal = AbstractFactory.CreateVideoFileInfoDal();
                }
                return _VideoFileInfoDal;
            }
            set { _VideoFileInfoDal = value; }
        }
	}	
}