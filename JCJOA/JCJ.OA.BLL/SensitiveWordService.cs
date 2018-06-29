using JCJ.OA.Model;
using JCJ.OA.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace JCJ.OA.BLL
{
    public partial class SensitiveWordService:BaseService<SensitiveWord>,ISensitiveWordService
    {
        /// <summary>
        /// 过滤禁用词
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool FilterFobidWord(string msg)
        {
            List<string> list = null;
            object obj = Common.MemcacheHelper.Get("fobid");
            if (obj == null)
            {
                this.DbSession.SensitiveWordDal.LoadEntities(s => s.IsForbid == true).Select(s=>s.WordPattern).ToList();
                string str = Common.SerializeHelper.SerializeToString(list);
                Common.MemcacheHelper.Set("fobid", str);
            }
            else
            {
                list = Common.SerializeHelper.DeserializeToObject<List<string>>(obj.ToString());

            }
            string regex = string.Join("|", list.ToArray());
            return Regex.IsMatch(msg, regex);
        }

        /// <summary>
        /// 过滤审查词
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool FilterModWord(string msg)
        {
            List<string> list = null;
            object obj = Common.MemcacheHelper.Get("mod");
            if (obj == null)
            {
                list = this.DbSession.SensitiveWordDal.LoadEntities(s => s.IsMod == true).Select(s => s.WordPattern).ToList();
                string str = Common.SerializeHelper.SerializeToString(list);
                Common.MemcacheHelper.Set("mod", str);
            }
            else
            {
                list = Common.SerializeHelper.DeserializeToObject<List<string>>(obj.ToString());
            }
            string regex = string.Join("|", list.ToArray());
            regex = regex.Replace(@"\", @"\\").Replace(@"{2}", @".{0,2}");
            return Regex.IsMatch(msg, regex);
        }

        /// <summary>
        /// 过滤替换词
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string FileterReplaceWord(string msg)
        {
            List<SensitiveWord> list = null;
            object obj = Common.MemcacheHelper.Get("replace");
            if (obj == null)
            {
                list = this.DbSession.SensitiveWordDal.LoadEntities(s => s.IsForbid == false && s.IsMod == false).ToList();
                string str = Common.SerializeHelper.SerializeToString(list);
                Common.MemcacheHelper.Set("replace", str);
            }
            else
            {
                list = Common.SerializeHelper.DeserializeToObject<List<SensitiveWord>>(obj.ToString());
            }
            foreach (SensitiveWord sensitiveWord in list)
            {
                msg = msg.Replace(sensitiveWord.WordPattern, sensitiveWord.ReplaceWord);
            }
            return msg;
        }

    }

}

