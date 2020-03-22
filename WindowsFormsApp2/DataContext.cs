using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    class DataContext : DbContext
    {
        public DataContext()
            : base(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=db.mdf;Integrated Security=True;Connect Timeout=30")
        { }
    }
}
