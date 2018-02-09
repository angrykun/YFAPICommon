using EntityFrameworkCore.UserModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class MyDBContext : DbContext
    {
        public MyDBContext()
            :base("name=Default")
        {

        }
        public DbSet<User> User { set; get; }
    }
}
