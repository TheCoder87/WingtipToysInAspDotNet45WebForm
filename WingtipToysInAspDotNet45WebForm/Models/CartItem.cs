using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WingtipToysInAspDotNet45WebForm.Models
{
    public class CartItem
    {
        public string CartItemID { get; set; }
        public string CartItemUID { get; set; }
        public int Quantity { get; set; }
        public DateTime DateCreated { get; set; }
        public int ProductID { get; set; }
        public virtual Product Product { get; set; }


    }
}