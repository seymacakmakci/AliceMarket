using AliceMarket.Contexts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AliceMarket
{
    public partial class Products : Form
    {
        public Products()
        {
            InitializeComponent();
        }

        private void btnCreateDB_Click(object sender, EventArgs e)
        {
            using (AliceMarketDbContext Products = new AliceMarketDbContext())
            {
                Products.Database.Create();
                MessageBox.Show("Database is created successfully");
            }
        }
    }
}
