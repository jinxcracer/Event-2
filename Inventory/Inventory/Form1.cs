using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory
{
    public partial class frmAddProduct : Form
    {
        private string _ProductName, _Category, _MfgDate, _ExpDate, _Description;
        private int _Quantity;
        private double _SellPrice;
        private BindingSource showProductList = new BindingSource();

        public frmAddProduct()
        {
            InitializeComponent();
        }
        private void frmAddProduct_Load(object sender, EventArgs e)
        {
            string[] ListOfProductCategory = {
                "Beverages",
                "Bread/Bakery",
                "Canned/Jarred Goods",
                "Dairy",
                "Frozen Goods",
                "Meat",
                "Personal Care",
                "Other"
            };

            foreach (string category in ListOfProductCategory)
            {
                cbCategory.Items.Add(category);
            }
        }
        public string Product_Name(string name)
        {
            if (!Regex.IsMatch(name, @"^[a-zA-Z\s]+$"))
            {
                throw new StringFormatException("Product Name must only contain letters.");
            }
            return name;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        public int Quantity(string qty)
        {
            if (!Regex.IsMatch(qty, @"^\d+$"))
            {
                throw new NumberFormatException("Quantity must be a valid number.");
            }
            return Convert.ToInt32(qty);
        }


        public double SellingPrice(string price)
        {
            if (!Regex.IsMatch(price, @"^(\d*\.)?\d+$"))
            {
                throw new CurrencyFormatException("Selling Price must be a valid currency value.");
            }
            return Convert.ToDouble(price);
        }

        
        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                
                _ProductName = Product_Name(txtProductName.Text);
                _Category = cbCategory.Text;
                _MfgDate = dtPickerMfgDate.Value.ToString("yyyy-MM-dd");
                _ExpDate = dtPickerMfgDate.Value.ToString("yyyy-MM-dd");
                _Description = richTxtDescription.Text;
                _Quantity = Quantity(txtQuantity.Text);
                _SellPrice = SellingPrice(txtSellPrice.Text);

                
                showProductList.Add(new ProductClass(_ProductName, _Category, _MfgDate, _ExpDate, _SellPrice, _Quantity, _Description));

                
                gridViewProductList.DataSource = showProductList;
            }
            catch (StringFormatException ex)
            {
                MessageBox.Show(ex.Message, "String Format Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (NumberFormatException ex)
            {
                MessageBox.Show(ex.Message, "Number Format Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (CurrencyFormatException ex)
            {
                MessageBox.Show(ex.Message, "Currency Format Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

   
    public class StringFormatException : Exception
    {
        public StringFormatException(string message) : base(message) { }
    }

    public class NumberFormatException : Exception
    {
        public NumberFormatException(string message) : base(message) { }
    }

    public class CurrencyFormatException : Exception
    {
        public CurrencyFormatException(string message) : base(message) { }
    }
}

