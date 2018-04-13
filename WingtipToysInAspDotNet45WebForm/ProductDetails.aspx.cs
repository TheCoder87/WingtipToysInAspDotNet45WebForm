using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using WingtipToysInAspDotNet45WebForm.Models;

namespace WingtipToysInAspDotNet45WebForm
{
    public partial class ProductDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public IQueryable<Product> GetProduct([QueryString("productID")]int? productId)
        {
            var _db = new ProductContext();
            IQueryable<Product> query = _db.Products;
            if (productId.HasValue && productId>0)
            {
                query = query.Where(p => p.ProductID == productId);
            }
            return query;
        }
    }
}