using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVRML.Classes
{
    class Admincls
    {
        public int AdminId { set; get; }
        public string Username { set; get; }
        public string Password { set; get; }


        public int Login(string username, string password)
        {
            using (var data= new DataClasses1DataContext())
            {
                var admin = data.Administrators.FirstOrDefault(f => f.Username == username && f.AdminPw== password);
                if (admin!=null)
                {
                    return admin.AdminId;
                }
            }

            return 0;
        }

    }

}
