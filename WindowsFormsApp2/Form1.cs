using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var mycriteria = new MyCriteria()
            {
                DateCol = DateTime.Now
            };

            var list = new List<MyCriteria>() { mycriteria };

            var criteria = list.ToDataTable();

            var context = new DataContext();
            var results = context.Database.Connection.Query<DateTime>(new CommandDefinition("TestSP", new { criteria }, commandType: CommandType.StoredProcedure));
        }
    }
}
