using JCJ.OA.DALFactory;
using JCJ.OA.IBLL;
using JCJ.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCJ.OA.BLL
{
    public partial class VideoFileInfoService:BaseService<VideoFileInfo>,IVideoFileInfoService
    {
        public bool AddVideoFileInfo(int classId, VideoFileInfo videoInfo)
        {
            var classInfo = this.DbSession.VideoClassDal.LoadEntities(c => c.ID == classId).FirstOrDefault();
            this.DbSession.VideoFileInfoDal.AddEntity(videoInfo);
            videoInfo.VideoClass.Add(classInfo);
            return this.DbSession.SaveChanges();
        }
        
    }
}
