using JCJ.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCJ.OA.IBLL
{
    public partial interface ISensitiveWordService:IBaseService<SensitiveWord>
    {
        bool FilterFobidWord(string msg);
        bool FilterModWord(string msg);
        string FileterReplaceWord(string msg);
    }
}
