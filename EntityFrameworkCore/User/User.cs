using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.UserModel
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Pass { set; get; }
        public string Email { get; set; }
    }
}
