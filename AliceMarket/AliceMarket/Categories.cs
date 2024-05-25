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
             this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            dgvCategories.CellClick += dgvCategories_CellClick;
        }

         AliceMarket1Entities1 market = new AliceMarket1Entities1();

        public void listCategories()
        {
            dgvCategories.DataSource = market.CategoriesTbls.ToList();
        }

        public void Clear()
        {
            txtCategoryID.Text = txtCtgName.Text = txtCtgDesc.Text = "";
        }

        private void Categories_Load(object sender, EventArgs e)
        {
            listCategories();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {


            // Create a new category object and populate it with values from input fields
            var category = new CategoriesTbl
            {
                Name = txtCtgName.Text,
                Description = txtCtgDesc.Text
            };

            market.CategoriesTbls.Add(category);
            market.SaveChanges();

            MessageBox.Show("New category is saved");
            listCategories();
            Clear();
        }

        private void dgvCategories_CellClick(object sender, DataGridViewCellEventArgs e)
        {
     
            if (e.RowIndex >= 0)
            {
                var row = dgvCategories.Rows[e.RowIndex];
                var cellValues = row.Cells.Cast<DataGridViewCell>().Select(cell => cell.Value?.ToString()).ToArray();

                System.Diagnostics.Debug.WriteLine($"Clicked Row Index: {e.RowIndex}, Values: {string.Join(", ", cellValues)}");

                txtCategoryID.Text = row.Cells[0].Value?.ToString() ?? string.Empty;
                txtCtgName.Text = row.Cells[1].Value?.ToString() ?? string.Empty;
                txtCtgDesc.Text = row.Cells[2].Value?.ToString() ?? string.Empty;
               
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
                    listCategories();
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
                    category.Name = txtCtgName.Text;
                    category.Description = txtCtgDesc.Text;

                    market.SaveChanges();

                    MessageBox.Show("Update is made successfully");
                    listCategories();
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

        private void txtFind_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtFind.Text))
            {
                var foundCategories = market.CategoriesTbls
                    .Where(c => c.Name.Contains(txtFind.Text))
                    .ToList();
                dgvCategories.DataSource = foundCategories;
            }
            else
            {
                listCategories();
            }
        }

    }
}

