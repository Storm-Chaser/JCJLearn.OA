using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCJ.OA.WebUI.Models
{
    public class SearchContent
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime AddDate { get; set; }
        public int Flag { get; set; }
        public LuceneActionType LuceneActionType { get; set; }
    }
}
