using AliceMarket.Contexts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Entity;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using AliceMarket.Models;

namespace AliceMarket
{
    public partial class Products : Form
    {
        public Products()
        {
            InitializeComponent();
            dgvProducts.CellClick += dgvProducts_CellClick;
        }

        private void btnCreateDB_Click(object sender, EventArgs e)
        {
            using (AliceMarketDbContext context = new AliceMarketDbContext())
            {
                context.Database.Create();
                MessageBox.Show("Database is created successfully");
            }
        }

        AliceMarketDbContext dbcontext = new AliceMarketDbContext();

        public void ListProducts()
        {
            dgvProducts.DataSource = dbcontext.Products.ToList();
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
            
            var product = new ProductsModel
            {
                Name = txtProductName.Text,
                Price = int.Parse(txtPrice.Text),
                Quantity = int.Parse(txtQty.Text),
                Description = txtDesc.Text
            };

            dbcontext.Products.Add(product);
            dbcontext.SaveChanges();

            MessageBox.Show("New product is saved");
            ListProducts();
            Clear();
        }

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvProducts.Rows[e.RowIndex];
                var cellValues = row.Cells.Cast<DataGridViewCell>().Select(cell => cell.Value?.ToString()).ToArray();

                System.Diagnostics.Debug.WriteLine($"Clicked Row Index: {e.RowIndex}, Values: {string.Join(", ", cellValues)}");

                txtProductID.Text = row.Cells[0].Value?.ToString() ?? string.Empty;
                txtProductName.Text = row.Cells[1].Value?.ToString() ?? string.Empty;
                txtPrice.Text = row.Cells[2].Value?.ToString() ?? string.Empty;
                txtQty.Text = row.Cells[3].Value?.ToString() ?? string.Empty;
                txtDesc.Text = row.Cells[4].Value?.ToString() ?? string.Empty;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtProductID.Text, out int productId))
            {
                var product = dbcontext.Products.FirstOrDefault(p => p.ProductId == productId);

                if (product != null)
                {
                    dbcontext.Products.Remove(product);
                    dbcontext.SaveChanges();

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
            /*if (int.TryParse(txtProductID.Text, out int productId))
            {
                var product = dbcontext.Products.FirstOrDefault(p => dbcontext.Products == productId);

                if (product != null)
                {
                    product.ProductName = txtProductName.Text;
                    product.Price = int.TryParse(txtPrice.Text, out int price) ? price : (decimal?)null;
                    product.Quantity = int.TryParse(txtQty.Text, out int qty) ? qty : (int?)null;
                    product.Description = txtDesc.Text;

                    dbcontext.SaveChanges();

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
            }*/
            if (int.TryParse(txtProductID.Text, out int productId))
            {
                // Corrected comparison: compare the Id property of the product
                var product = dbcontext.Products.FirstOrDefault(p => p.ProductId == productId);

                if (product != null)
                {
                    product.Name = txtProductName.Text;
                    product.Price = int.Parse(txtPrice.Text);
                    product.Quantity = int.Parse(txtQty.Text);
                    product.Description = txtDesc.Text;

                    dbcontext.SaveChanges();

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
                var foundProducts = dbcontext.Products
                    .Where(p => p.Name.Contains(txtFind.Text))
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