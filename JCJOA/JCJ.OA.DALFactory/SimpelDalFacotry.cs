 

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JCJ.OA.DALFactory
{
    public partial class AbstractFactory
    {
      
   
		
	    public static IDAL.IActionInfoDal CreateActionInfoDal()
        {

            //string classFulleName = ConfigurationManager.AppSettings["DALNameSpace"] + ".ActionInfoDAL";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
			string fullClassName = NameSpace + ".ActionInfoDal";
            var obj  = CreateInstance(fullClassName);


            return obj as IDAL.IActionInfoDal;
        }
		
	    public static IDAL.IArticelDal CreateArticelDal()
        {

            //string classFulleName = ConfigurationManager.AppSettings["DALNameSpace"] + ".ArticelDAL";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
			string fullClassName = NameSpace + ".ArticelDal";
            var obj  = CreateInstance(fullClassName);


            return obj as IDAL.IArticelDal;
        }
		
	    public static IDAL.IArticelClassDal CreateArticelClassDal()
        {

            //string classFulleName = ConfigurationManager.AppSettings["DALNameSpace"] + ".ArticelClassDAL";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
			string fullClassName = NameSpace + ".ArticelClassDal";
            var obj  = CreateInstance(fullClassName);


            return obj as IDAL.IArticelClassDal;
        }
		
	    public static IDAL.IArticelCommentDal CreateArticelCommentDal()
        {

            //string classFulleName = ConfigurationManager.AppSettings["DALNameSpace"] + ".ArticelCommentDAL";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
			string fullClassName = NameSpace + ".ArticelCommentDal";
            var obj  = CreateInstance(fullClassName);


            return obj as IDAL.IArticelCommentDal;
        }
		
	    public static IDAL.IDepartmentDal CreateDepartmentDal()
        {

            //string classFulleName = ConfigurationManager.AppSettings["DALNameSpace"] + ".DepartmentDAL";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
			string fullClassName = NameSpace + ".DepartmentDal";
            var obj  = CreateInstance(fullClassName);


            return obj as IDAL.IDepartmentDal;
        }
		
	    public static IDAL.IKeyWordsRankDal CreateKeyWordsRankDal()
        {

            //string classFulleName = ConfigurationManager.AppSettings["DALNameSpace"] + ".KeyWordsRankDAL";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
			string fullClassName = NameSpace + ".KeyWordsRankDal";
            var obj  = CreateInstance(fullClassName);


            return obj as IDAL.IKeyWordsRankDal;
        }
		
	    public static IDAL.IPhotoClassDal CreatePhotoClassDal()
        {

            //string classFulleName = ConfigurationManager.AppSettings["DALNameSpace"] + ".PhotoClassDAL";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
			string fullClassName = NameSpace + ".PhotoClassDal";
            var obj  = CreateInstance(fullClassName);


            return obj as IDAL.IPhotoClassDal;
        }
		
	    public static IDAL.IPhotoInfoDal CreatePhotoInfoDal()
        {

            //string classFulleName = ConfigurationManager.AppSettings["DALNameSpace"] + ".PhotoInfoDAL";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
			string fullClassName = NameSpace + ".PhotoInfoDal";
            var obj  = CreateInstance(fullClassName);


            return obj as IDAL.IPhotoInfoDal;
        }
		
	    public static IDAL.IR_UserInfo_ActionInfoDal CreateR_UserInfo_ActionInfoDal()
        {

            //string classFulleName = ConfigurationManager.AppSettings["DALNameSpace"] + ".R_UserInfo_ActionInfoDAL";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
			string fullClassName = NameSpace + ".R_UserInfo_ActionInfoDal";
            var obj  = CreateInstance(fullClassName);


            return obj as IDAL.IR_UserInfo_ActionInfoDal;
        }
		
	    public static IDAL.IRoleInfoDal CreateRoleInfoDal()
        {

            //string classFulleName = ConfigurationManager.AppSettings["DALNameSpace"] + ".RoleInfoDAL";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
			string fullClassName = NameSpace + ".RoleInfoDal";
            var obj  = CreateInstance(fullClassName);


            return obj as IDAL.IRoleInfoDal;
        }
		
	    public static IDAL.ISearchDetailsDal CreateSearchDetailsDal()
        {

            //string classFulleName = ConfigurationManager.AppSettings["DALNameSpace"] + ".SearchDetailsDAL";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
			string fullClassName = NameSpace + ".SearchDetailsDal";
            var obj  = CreateInstance(fullClassName);


            return obj as IDAL.ISearchDetailsDal;
        }
		
	    public static IDAL.ISensitiveWordDal CreateSensitiveWordDal()
        {

            //string classFulleName = ConfigurationManager.AppSettings["DALNameSpace"] + ".SensitiveWordDAL";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
			string fullClassName = NameSpace + ".SensitiveWordDal";
            var obj  = CreateInstance(fullClassName);


            return obj as IDAL.ISensitiveWordDal;
        }
		
	    public static IDAL.IUserInfoDal CreateUserInfoDal()
        {

            //string classFulleName = ConfigurationManager.AppSettings["DALNameSpace"] + ".UserInfoDAL";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
			string fullClassName = NameSpace + ".UserInfoDal";
            var obj  = CreateInstance(fullClassName);


            return obj as IDAL.IUserInfoDal;
        }
		
	    public static IDAL.IVideoClassDal CreateVideoClassDal()
        {

            //string classFulleName = ConfigurationManager.AppSettings["DALNameSpace"] + ".VideoClassDAL";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
			string fullClassName = NameSpace + ".VideoClassDal";
            var obj  = CreateInstance(fullClassName);


            return obj as IDAL.IVideoClassDal;
        }
		
	    public static IDAL.IVideoFileInfoDal CreateVideoFileInfoDal()
        {

            //string classFulleName = ConfigurationManager.AppSettings["DALNameSpace"] + ".VideoFileInfoDAL";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
			string fullClassName = NameSpace + ".VideoFileInfoDal";
            var obj  = CreateInstance(fullClassName);


            return obj as IDAL.IVideoFileInfoDal;
        }
	}
	
}