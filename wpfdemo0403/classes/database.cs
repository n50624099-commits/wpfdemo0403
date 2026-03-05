using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfdemo0403.database;
using wpfdemo0403.classes;

namespace wpfdemo0403.classes
{
    public class database
    {
        public static dostEntities Context = new dostEntities();
        public database() { }
    }
}
