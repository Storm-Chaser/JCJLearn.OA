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
    public partial class DBSession :IDBSession
    {
        //Model.ItcastCmsEntities Db = new ItcastCmsEntities();
        public DbContext Db
        {
            get { return DBContexFactory.CreateDbContext(); }
        }
        //private IUserInfoDal _userInfoDal;
        //public IUserInfoDal userInfoDal
        //{
        //    get {
        //        if(_userInfoDal == null)
        //        {
        //            //_userInfoDal = new UserInfoDal();  //解耦和

        //            _userInfoDal = AbstractFactory.CreateUserInfoDal();
        //        }
        //        return _userInfoDal;
        //    }
        //    set { _userInfoDal = value; }
        //}

        //public IUserInfoDal userInfoDal { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        //一个业务中可能涉及到对多张表的操作，但是希望链接一次数据库，完成对多张表的操作。
        //工作单元模式
        public bool SaveChanges()
        {
            return Db.SaveChanges() > 0;
        }

    }
}
