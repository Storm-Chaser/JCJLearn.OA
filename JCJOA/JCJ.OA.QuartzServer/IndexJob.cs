using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCJ.OA.QuartzServer
{
    public class IndexJob : IJob
    {
        IBLL.IKeyWordsRankService bll = new BLL.keyWordsRankService();
        public void Execute(JobExecutionContext context)
        {
            bll.DeleteAll();
            bll.InsertKeyWordRank();
        }
    }
}
