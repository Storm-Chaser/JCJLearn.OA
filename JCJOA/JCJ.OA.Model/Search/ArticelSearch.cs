using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCJ.OA.Model.Search
{
    public class ArticelSearch:BaseSearch
    {
        public string ArticelTitle { get; set; }
        public string ArticelAuthor { get; set; }
        public string ArticelClassId { get; set; }
        public string FormDatepicker { get; set; }
        public string ToDatepicker { get; set; }
    }
}
