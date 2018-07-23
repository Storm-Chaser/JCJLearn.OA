using JCJ.OA.IBLL;
using JCJ.OA.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCJ.OA.BLL
{
    public partial class keyWordsRankService: BaseService<KeyWordsRank>,IKeyWordsRankService
    {
        public bool DeleteAll()
        {
            string sql = "truncate table KeyWordsRank";
            return this.DbSession.ExecuteSql(sql) > 0;
        }

        public List<string> GetSearchWord(string term)
        {
            string sql = "select KeyWords from KeyWordsRank where KeyWords like @term";
            return this.DbSession.ExecuteQuery<string>(sql, new SqlParameter("@term", term + "%"));
        }

        public bool InsertKeyWordRank()
        {
            string sql = "insert into KeyWordsRank(Id,KeyWords,SearchCount) select newid(), SearchDetails.KeyWOrds,count(*) from SearchDetails where DateDiff(day,SearchDetails.SearchDateTime,getdate())<=7 group by SearchDetails.KeyWOrds";
            return this.DbSession.ExecuteSql(sql) > 0;
        }
    }
    
}
