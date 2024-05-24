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
        }

        private void txtCustomerID_TextChanged(object sender, EventArgs e)
        {

        }
        AliceMarketEntities market = new AliceMarketEntities();

        public void ListOrders()
        {
            dgvOrders.DataSource = market.OrdersTbls.ToList();
        }

        public void Clear()
        {
            txtOrderID.Text = txtCustomerID.Text = txtProductID.Text = txtOrderDate.Text = txtAmount.Text = "";
        }

        private void Orders_Load(object sender, EventArgs e)
        {
            ListOrders();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var order = new OrdersTbl
            {
                CustomerID = int.TryParse(txtCustomerID.Text, out int customerId) ? customerId : (int?)null,
                ProductID = int.TryParse(txtProductID.Text, out int productId) ? productId : (int?)null,
                OrderDate = DateTime.TryParse(txtOrderDate.Text, out DateTime orderDate) ? orderDate : (DateTime?)null,
                Amount = decimal.TryParse(txtAmount.Text, out decimal amount) ? amount : (decimal?)null
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
                txtOrderID.Text = dgvOrders.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtCustomerID.Text = dgvOrders.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtProductID.Text = dgvOrders.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtOrderDate.Text = dgvOrders.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtAmount.Text = dgvOrders.Rows[e.RowIndex].Cells[4].Value.ToString();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
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

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtOrderID.Text, out int orderId))
            {
                var order = market.OrdersTbls.FirstOrDefault(o => o.OrderID == orderId);

                if (order != null)
                {
                    order.CustomerID = int.TryParse(txtCustomerID.Text, out int customerId) ? customerId : (int?)null;
                    order.ProductID = int.TryParse(txtProductID.Text, out int productId) ? productId : (int?)null;
                    order.OrderDate = DateTime.TryParse(txtOrderDate.Text, out DateTime orderDate) ? orderDate : (DateTime?)null;
                    order.Amount = decimal.TryParse(txtAmount.Text, out decimal amount) ? amount : (decimal?)null;

                    market.SaveChanges();

                    MessageBox.Show("Update is made successfully");
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

    }
}
