using JCJ.OA.Model;
using JCJ.OA.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCJ.OA.BLL
{
    public partial class SensitiveWordService:BaseService<SensitiveWord>,ISensitiveWordService
    {
        /// <summary>
        /// 过滤禁用词
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool FilterForbidWord(string msg)
        {
            List<string> list = null;
            object obj = Common.MemcacheHelper.Get("fobid");
            if (obj == null)
            {
                this.DbSession.SensitiveWordDal.LoadEntities(s => s.IsForbid == true).Select(s=>s.WordPattern).ToList();
                Common.SerializeHelper.SerializeToString(list);
            }
            else
            {

            }
            return false;
        }
    }

}

