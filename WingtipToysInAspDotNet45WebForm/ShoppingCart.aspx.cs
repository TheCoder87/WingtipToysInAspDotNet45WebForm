using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WingtipToysInAspDotNet45WebForm.Logic;
using WingtipToysInAspDotNet45WebForm.Models;

namespace WingtipToysInAspDotNet45WebForm
{
    public partial class ShoppingCart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (ShoppingCartActions action=new ShoppingCartActions())
            {
                decimal cartTotal = 0;
                cartTotal = action.GetTotal();
                if (cartTotal>0)
                {
                    //display Total
                    lblTotal.Text = String.Format("{0:c}", cartTotal);
                }
                else
                {
                    lblToTotal.Text = "";
                    lblTotal.Text = "";
                    ShoppingCartTitle.InnerText = "Shopping Cart is Empty!";
                    btnUpdate.Visible = false;
                }
            }

        }

        // The return type can be changed to IEnumerable, however to support
        // paging and sorting, the following parameters must be added:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public List<CartItem> GetShoppingCartItems()
        {
            ShoppingCartActions action = new ShoppingCartActions();            

            return action.GetCartItems(); ;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateCartItems();
        }

        private List<CartItem> UpdateCartItems()
        {
            using (ShoppingCartActions userShoppingCart =new ShoppingCartActions())
            {
                string cartUID = userShoppingCart.GetCartUID();

                ShoppingCartUpdates[] cartUpdates = new ShoppingCartUpdates[CartList.Rows.Count];
                for (int i = 0; i < CartList.Rows.Count; i++)
                {
                    IOrderedDictionary rowValues = new OrderedDictionary();
                    rowValues = GetValues(CartList.Rows[i]);

                    cartUpdates[i].ProductId = Convert.ToInt32(rowValues["ProductID"]);

                    CheckBox cbRemove = new CheckBox();
                    cbRemove = (CheckBox)CartList.Rows[i].FindControl("Remove");
                    cartUpdates[i].RemoveItem = cbRemove.Checked;

                    TextBox quantityTextBox = new TextBox();
                    quantityTextBox = (TextBox)CartList.Rows[i].FindControl("PurchaseQuantity");
                    cartUpdates[i].PurchaseQuantity = Convert.ToInt32(quantityTextBox.Text.ToString());
                }
                userShoppingCart.UpdateShoppingCartDatabase(cartUID, cartUpdates);
                CartList.DataBind();
                lblTotal.Text = String.Format("{0:c}", userShoppingCart.GetTotal());

                return userShoppingCart.GetCartItems();
            }
        }

        public static IOrderedDictionary GetValues(GridViewRow gridViewRow)
        {
            IOrderedDictionary values = new OrderedDictionary();
            foreach (DataControlFieldCell cell in gridViewRow.Cells)
            {
                if (cell.Visible)
                {
                    // Extract values from the cell.
                    cell.ContainingField.ExtractValuesFromCell(values, cell, gridViewRow.RowState, true); 
                }
            }
            return values;
        }
    }
}