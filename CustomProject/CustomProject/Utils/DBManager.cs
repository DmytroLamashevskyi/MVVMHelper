using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Common;

namespace CustomProject.Utils
{
    //https://www.tektutorialshub.com/entity-framework/ef-code-first-example/

    public class DBManager : DbContext
    {
        public DBManager() : base("MyContext")
        {

        }

        // Constructor to use on a DbConnection that is already opened
        public DBManager(DbConnection existingConnection, bool contextOwnsConnection)
          : base(existingConnection, contextOwnsConnection)
        {

        }



    }
}
