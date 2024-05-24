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

        AliceMarketEntities market = new AliceMarketEntities();

        public void ListProducts()
        {
            dgvProducts.DataSource = market.ProductsTbls.ToList();
        }

        public void Clear()
        {
            txtProductID.Text = txtProductName.Text = txtPrice.Text = txtQty.Text = txtDesc.Text = "";
        }

        private void Products_Load(object sender, EventArgs e)
        {
            ListProducts();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Create a new product object and populate it with values from input fields
            var product = new ProductsTbl
            {
                ProductName = txtProductName.Text,
                Price = decimal.TryParse(txtPrice.Text, out decimal price) ? price : (decimal?)null,
                Quantity = int.TryParse(txtQty.Text, out int qty) ? qty : (int?)null,
                Description = txtDesc.Text
            };

            market.ProductsTbls.Add(product);
            market.SaveChanges();

            MessageBox.Show("New product is saved");
            ListProducts();
            Clear();
        }

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Ensure a valid row is clicked
            {
                txtProductID.Text = dgvProducts.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtProductName.Text = dgvProducts.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtPrice.Text = dgvProducts.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtQty.Text = dgvProducts.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtDesc.Text = dgvProducts.Rows[e.RowIndex].Cells[4].Value.ToString();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtProductID.Text, out int productId))
            {
                var product = market.ProductsTbls.FirstOrDefault(p => p.ProductID == productId);

                if (product != null)
                {
                    market.ProductsTbls.Remove(product);
                    market.SaveChanges();

                    MessageBox.Show("Product is deleted.");
                    ListProducts();
                    Clear();
                }
                else
                {
                    MessageBox.Show("Product not found.");
                }
            }
            else
            {
                MessageBox.Show("Please select a valid product.");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtProductID.Text, out int productId))
            {
                var product = market.ProductsTbls.FirstOrDefault(p => p.ProductID == productId);

                if (product != null)
                {
                    product.ProductName = txtProductName.Text;
                    product.Price = decimal.TryParse(txtPrice.Text, out decimal price) ? price : (decimal?)null;
                    product.Quantity = int.TryParse(txtQty.Text, out int qty) ? qty : (int?)null;
                    product.Description = txtDesc.Text;

                    market.SaveChanges();

                    MessageBox.Show("Update is made successfully");
                    ListProducts();
                    Clear();
                }
                else
                {
                    MessageBox.Show("Product not found.");
                }
            }
            else
            {
                MessageBox.Show("Please select a valid product.");
            }
        }

        private void txtFindProduct_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtFind.Text))
            {
                var foundProducts = market.ProductsTbls
                    .Where(p => p.ProductName.Contains(txtFind.Text))
                    .ToList();
                dgvProducts.DataSource = foundProducts;
            }
            else
            {
                ListProducts();
            }
        }
    }
}
