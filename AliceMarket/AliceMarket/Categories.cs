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
    public partial class Categories : Form
    {
        public Categories()
        {
            InitializeComponent();
        }

        AliceMarketEntities market = new AliceMarketEntities();

        public void ListCategories()
        {
            dgvCategories.DataSource = market.CategoriesTbls.ToList();
        }

        public void Clear()
        {
            txtCategoryID.Text = txtCtgName.Text = tctCtgDesc.Text = "";
        }

        private void Categories_Load(object sender, EventArgs e)
        {
            ListCategories();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Create a new category object and populate it with values from input fields
            var category = new CategoriesTbl
            {
                CategoryName = txtCtgName.Text,
                Description = tctCtgDesc.Text
            };

            market.CategoriesTbls.Add(category);
            market.SaveChanges();

            MessageBox.Show("New category is saved");
            ListCategories();
            Clear();
        }

        private void dgvCategories_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Ensure a valid row is clicked
            {
                txtCategoryID.Text = dgvCategories.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtCtgName.Text = dgvCategories.Rows[e.RowIndex].Cells[1].Value.ToString();
                tctCtgDesc.Text = dgvCategories.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtCategoryID.Text, out int categoryId))
            {
                var category = market.CategoriesTbls.FirstOrDefault(c => c.CategoryID == categoryId);

                if (category != null)
                {
                    market.CategoriesTbls.Remove(category);
                    market.SaveChanges();

                    MessageBox.Show("Category is deleted.");
                    ListCategories();
                    Clear();
                }
                else
                {
                    MessageBox.Show("Category not found.");
                }
            }
            else
            {
                MessageBox.Show("Please select a valid category.");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtCategoryID.Text, out int categoryId))
            {
                var category = market.CategoriesTbls.FirstOrDefault(c => c.CategoryID == categoryId);

                if (category != null)
                {
                    category.CategoryName = txtCtgName.Text;
                    category.Description = tctCtgDesc.Text;

                    market.SaveChanges();

                    MessageBox.Show("Update is made successfully");
                    ListCategories();
                    Clear();
                }
                else
                {
                    MessageBox.Show("Category not found.");
                }
            }
            else
            {
                MessageBox.Show("Please select a valid category.");
            }
        }

        private void txtFindCategory_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtFind.Text))
            {
                var foundCategories = market.CategoriesTbls
                    .Where(c => c.CategoryName.Contains(txtFind.Text))
                    .ToList();
                dgvCategories.DataSource = foundCategories;
            }
            else
            {
                ListCategories();
            }
        }

        private void txtFind_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

