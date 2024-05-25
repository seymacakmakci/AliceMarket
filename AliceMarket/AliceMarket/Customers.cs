using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace AliceMarket
{
    public partial class Customers : Form
    {
        public Customers()
        {
            InitializeComponent();
        }
        
        AliceMarketEntities1 market = new AliceMarketEntities1();

        public void listCustomers()
        {
              dgvCustomers.DataSource = market.CustomersTbls.ToList();

        }

        public void Clear()
        {
            txtCustomerID.Text = txtCstName.Text = txtPhone.Text = txtCity.Text = txtBirthdate.Text = "";
        }
        private void Customers_Load(object sender, EventArgs e)
        {
            listCustomers();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Create a new customer object and populate it with values from input fields
            var customer = new CustomersTbl
            {
                Name = txtCstName.Text,
                Phone = txtPhone.Text,
                City = txtCity.Text,
                BirthDate = ParseDateTime(txtBirthdate.Text)

            };

            market.CustomersTbls.Add(customer);
            market.SaveChanges();

            MessageBox.Show("New customer is saved");
            listCustomers();
            Clear();
        }
        private DateTime? ParseDateTime(string dateString)
        {
            if (DateTime.TryParse(dateString, out DateTime parsedDate))
            {
                return parsedDate;
            }
            return null;
        }

        private void dgvCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Ensure a valid row is clicked
            {
                txtCustomerID.Text = dgvCustomers.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtCstName.Text = dgvCustomers.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtPhone.Text = dgvCustomers.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtCity.Text = dgvCustomers.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtBirthdate.Text = dgvCustomers.Rows[e.RowIndex].Cells[4].Value.ToString();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtCustomerID.Text, out int customerId))
            {
                var customer = market.CustomersTbls.FirstOrDefault(c => c.CustomerID == customerId);

                if (customer != null)
                {
                    market.CustomersTbls.Remove(customer);
                    market.SaveChanges();

                    MessageBox.Show("Customer is deleted.");
                    listCustomers();
                    Clear();
                }
                else
                {
                    MessageBox.Show("Customer not found.");
                }
            }
            else
            {
                MessageBox.Show("Please select a valid customer.");
            }
        }



        /* private void txtFind_TextChanged(object sender, EventArgs e)
         {
             if (!string.IsNullOrWhiteSpace(txtFind.Text))
             {
                 var foundCustomers = market.CustomersTbls
                     .Where(c => c.Name.Contains(txtFind.Text))
                     .ToList();
                 dgvCustomers.DataSource = foundCustomers;
             }
             else
             {
                 listCustomers();
             }
         } */

        private void txtFind_TextChanged(object sender, EventArgs e)
        {
            if (txtFind.Text != string.Empty)
            {
                var find_member = market.CustomersTbls.Where(find => find.Name.Contains(txtFind.Text));
                dgvCustomers.DataSource = find_member.ToList();
            }
            else
            {
                dgvCustomers.DataSource = market.CustomersTbls.ToList();
            }
        }


        /* private void btnEdit_Click(object sender, EventArgs e)
         {
             if (int.TryParse(txtCustomerID.Text, out int customerId))
             {
                 var customer = market.CustomersTbls.FirstOrDefault(c => c.CustomerID == customerId);

                 if (customer != null)
                 {
                     customer.Name = txtCstName.Text;
                     customer.Phone = txtPhone.Text;
                     customer.City = txtCity.Text;
                     customer.BirthDate = ParseDateTime(txtBirthdate.Text);

                     market.SaveChanges();

                     MessageBox.Show("Update is made successfully");
                     listCustomers();
                     Clear();
                 }
                 else
                 {
                     MessageBox.Show("Customer not found.");
                 }
             }
             else
             {
                 MessageBox.Show("Please select a valid customer.");
             }
         }*/

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int customer_id = int.Parse(txtCustomerID.Text);   //convert the text value to int by int.Parse
            var upd_customer = market.CustomersTbls.First(upd => upd.CustomerID == customer_id);

            upd_customer.Name = txtCstName.Text;
            upd_customer.City = txtCity.Text;
            upd_customer.Phone = txtPhone.Text;
            upd_customer.BirthDate = upd_customer.BirthDate;

            market.SaveChanges();
            MessageBox.Show("Update is made successfully");
            listCustomers();
            Clear();
        }

        
    }
}