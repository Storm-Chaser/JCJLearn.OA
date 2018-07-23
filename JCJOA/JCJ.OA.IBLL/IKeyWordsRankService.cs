using JCJ.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCJ.OA.IBLL
{
    public partial interface IKeyWordsRankService:IBaseService<KeyWordsRank>
    {
        bool DeleteAll();
        bool InsertKeyWordRank();
        List<string> GetSearchWord(string term);
    }
}
