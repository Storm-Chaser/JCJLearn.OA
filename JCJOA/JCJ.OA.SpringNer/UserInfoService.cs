using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCJ.OA.SpringNer
{
    public class UserInfoService : IUserInfoService
    {
        public string UserName { get; set; }
        public Person person { get; set; }
        public string ShowMsg()
        {
            return "Hello World"+UserName+person.Age;
        }
    }
}
