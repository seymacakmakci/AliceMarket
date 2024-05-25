using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AliceMarket
{
    public partial class Orders : Form
    {
        public Orders()
        {
            InitializeComponent();
            dgvOrders.CellClick += dgvOrders_CellClick;
        }

        readonly AliceMarket2Entities market = new AliceMarket2Entities();

        public void ListOrders()
        {
            dgvOrders.DataSource = market.OrdersTbls.ToList();
        }

        public void Clear()
        {
            txtOrderID.Text = txtOrderID.Text = txtProductID.Text = txtOrderDate.Text = txtAmount.Text =txtCustomerID.Text= "";
        }

        private void Orders_Load(object sender, EventArgs e)
        {
            ListOrders();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var order = new OrdersTbl
            {
                CustomerID= int.Parse(txtOrderID.Text),
                ProductID= int.Parse(txtProductID.Text),
                OrderDate= DateTime.Now,
                OrderAmount= int.Parse(txtAmount.Text)

               
            };

            market.OrdersTbls.Add(order);
            market.SaveChanges();

            MessageBox.Show("New order is saved");
            ListOrders();
            Clear();
        }

        private void dgvOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
              if (e.RowIndex >= 0)
              {
                var row = dgvOrders.Rows[e.RowIndex];
                var cellValues = row.Cells.Cast<DataGridViewCell>().Select(cell => cell.Value?.ToString()).ToArray();

                System.Diagnostics.Debug.WriteLine($"Clicked Row Index: {e.RowIndex}, Values: {string.Join(", ", cellValues)}");

                txtOrderID.Text = row.Cells[0].Value?.ToString() ?? string.Empty;
                txtCustomerID.Text = row.Cells[1].Value?.ToString() ?? string.Empty;
                txtProductID.Text = row.Cells[2].Value?.ToString() ?? string.Empty;
                txtOrderDate.Text = row.Cells[3].Value?.ToString() ?? string.Empty;
                txtAmount.Text = row.Cells[4].Value?.ToString() ?? string.Empty;
            }
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
           

            try
            {
                if (int.TryParse(txtOrderID.Text, out int orderId))
                {
                    var order = market.OrdersTbls.FirstOrDefault(o => o.OrderID == orderId);

                    if (order != null)
                    {
                        market.OrdersTbls.Remove(order);
                        market.SaveChanges();

                        MessageBox.Show("Order is deleted.");
                        ListOrders();
                        Clear();
                    }
                    else
                    {
                        MessageBox.Show("Order not found.");
                    }
                }
                else
                {
                    MessageBox.Show("Please select a valid order.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Exception: {ex}");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            /* if (int.TryParse(txtOrderID.Text, out int orderId))
             {
                 var order = market.OrdersTbls.FirstOrDefault(o => o.OrderID == orderId);

                 if (order != null)
                 {
                     order.CustomerID = int.TryParse(txtOrderID.Text, out int customerId) ? customerId : (int?)null;
                     order.ProductID = int.TryParse(txtProductID.Text, out int productId) ? productId : (int?)null;
                     order.OrderDate = DateTime.TryParse(txtOrderDate.Text, out DateTime orderDate) ? orderDate : (DateTime?)null;
                     order.OrderAmount = int.TryParse(txtAmount.Text, out int amount) ? amount : (int?)null;

                     market.SaveChanges();

                     MessageBox.Show("Update is made successfully");
                     listOrders();
                     Clear();
                 }
                 else
                 {
                     MessageBox.Show("Order not found.");
                 }
             }
             else
             {
                 MessageBox.Show("Please select a valid order.");
             }
         }*/

            int order_id = int.Parse(txtOrderID.Text);   //convert the text value to int by int.Parse
            var upd_order = market.OrdersTbls.First(upd => upd.OrderID == order_id);

            upd_order.ProductID = int.Parse(txtProductID.Text);
            upd_order.CustomerID = int.Parse(txtCustomerID.Text);
            upd_order.OrderDate = upd_order.OrderDate;
            upd_order.OrderAmount = int.Parse(txtAmount.Text);

            market.SaveChanges();
            MessageBox.Show("Update is made successfully");
            ListOrders();
            Clear();

        }
    }
}
