using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WingtipToysInAspDotNet45WebForm.Logic;

namespace WingtipToysInAspDotNet45WebForm
{
    public partial class AddToCart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string rawID = Request.QueryString["ProductID"];
            int productID;
            if (! String.IsNullOrEmpty(rawID) && int.TryParse(rawID, out productID))
            {
                using (ShoppingCartActions userShoppingCart=new ShoppingCartActions())
                {
                    userShoppingCart.AddToCart(productID);
                }
            }
            else
            {
                Debug.Fail("ERROR : We should never get to AddToCart.aspx without a ProductId.");
                throw new Exception("ERROR : It is illegal to load AddToCart.aspx without setting a ProductId.");
            }

            Response.Redirect("ShoppingCart.aspx");
        }
    }
}