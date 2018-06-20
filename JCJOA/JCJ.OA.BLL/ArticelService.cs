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
    
    public partial class ArticelService:BaseService<Articel>,IArticelService
    {
        /// <summary>
        /// 多条件查询
        /// </summary>
        /// <param name="articelSearch"></param>
        /// <returns></returns>
        public IQueryable<Articel> LoadSearchEntities(ArticelSearch articelSearch)
        {
            var articelInfoList = this.DbSession.ArticelDal.LoadEntities(a => a.DelFlag == 0);
            if (!string.IsNullOrEmpty(articelSearch.ArticelClassId))
            {
                int cid = Convert.ToInt32(articelSearch.ArticelClassId);
                articelInfoList = from a in articelInfoList
                                  from b in a.ArticelClass
                                  where b.ID == cid
                                  select a;

            }
            if (!string.IsNullOrEmpty(articelSearch.ArticelTitle))
            {
                articelInfoList = articelInfoList.Where<Articel>(a => a.Title.Contains(articelSearch.ArticelTitle));
            }
            if (!string.IsNullOrEmpty(articelSearch.ArticelAuthor))
            {
                articelInfoList = articelInfoList.Where<Articel>(a => a.Author.Contains(articelSearch.ArticelAuthor));
            }
            //开始时间与结束时间，必须都要完整
            if (!string.IsNullOrEmpty(articelSearch.FormDatepicker) && !string.IsNullOrEmpty(articelSearch.ToDatepicker))
            {
                DateTime startDateTime = Convert.ToDateTime(articelSearch.FormDatepicker);
                DateTime endDateTime = Convert.ToDateTime(articelSearch.ToDatepicker);
                articelInfoList = articelInfoList.Where<Articel>(a => a.AddDate >= startDateTime && a.AddDate <= endDateTime);
            }
            articelSearch.TotalCount = articelInfoList.Count();
            return articelInfoList.OrderBy<Articel, int>(a => a.ID).Skip<Articel>((articelSearch.PageIndex - 1) * articelSearch.PageSize).Take<Articel>(articelSearch.PageSize);
        }
        /// <summary>
        /// 添加新闻文章.
        /// </summary>
        /// <param name="cid">类别编号</param>
        /// <param name="articelInfo"></param>
        /// <returns></returns>
        public bool AddEntity(int cid, Articel articelInfo)
        {
            var articelClass = this.DbSession.ArticelClassDal.LoadEntities(a => a.ID == cid).FirstOrDefault();
            if (articelClass != null)
            {
                this.DbSession.ArticelDal.AddEntity(articelInfo);
                articelInfo.ArticelClass.Add(articelClass);
                return this.DbSession.SaveChanges();
            }
            return false;

        }

    }

}
