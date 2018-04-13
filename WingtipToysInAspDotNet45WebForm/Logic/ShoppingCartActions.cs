using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WingtipToysInAspDotNet45WebForm.Models;

namespace WingtipToysInAspDotNet45WebForm.Logic
{
    public class ShoppingCartActions : IDisposable
    {
        public string ShoppingCartUID { get; set; }

        private ProductContext _db = new ProductContext();

        public const string CartSessionKey = "CartUID";

        public void AddToCart(int id)
        {
            // Retrieve the product from the database.
            ShoppingCartUID = GetCartUID();

            var cartItem = _db.CartItems.SingleOrDefault(c => c.CartItemUID == ShoppingCartUID && c.ProductID == id);                        

            if (cartItem==null)
            {
                //Create a new CartItem if no CartItem exist already
                cartItem = new CartItem
                {
                    CartItemID = Guid.NewGuid().ToString(),
                    ProductID = id,
                    CartItemUID = ShoppingCartUID,
                    Product = _db.Products.SingleOrDefault(p => p.ProductID == id),
                    Quantity = 1,
                    DateCreated = DateTime.Now
                };

                _db.CartItems.Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart,                  
                // then add one to the quantity. 
                cartItem.Quantity++;
            }

            //Save the chages
            _db.SaveChanges();
        }

        public string GetCartUID()
        {
            if (HttpContext.Current.Session[CartSessionKey]==null)
            {
                if (! String.IsNullOrWhiteSpace(HttpContext.Current.User.Identity.Name))
                {
                    HttpContext.Current.Session[CartSessionKey] = HttpContext.Current.User.Identity.Name;
                }
                else
                {
                    // Generate a new random GUID using System.Guid class. 
                    Guid tempCartItemUID = Guid.NewGuid();
                    HttpContext.Current.Session[CartSessionKey] = tempCartItemUID.ToString();
                }
            }
            return HttpContext.Current.Session[CartSessionKey].ToString();
        }

        public List<CartItem> GetCartItems()
        {
            ShoppingCartUID = GetCartUID();
            return _db.CartItems.Where(c => c.CartItemUID == ShoppingCartUID).ToList();
        }

        public decimal GetTotal()
        {
            ShoppingCartUID = GetCartUID();
            // Multiply product price by quantity of that product to get        
            // the current price for each of those products in the cart.  
            // Sum all product price totals to get the cart total.   
            decimal? total = decimal.Zero;
            total = (decimal?)(from cartItems in _db.CartItems
                               where cartItems.CartItemUID == ShoppingCartUID
                               select (int?)cartItems.Quantity * cartItems.Product.UnitPrice).Sum();

            return total ?? decimal.Zero;
        }

        internal void UpdateShoppingCartDatabase(string cartUID, ShoppingCartUpdates[] cartUpdates)
        {
            using (var db = new ProductContext())
            {
                try
                {
                    int CartItemCount = cartUpdates.Count();
                    List<CartItem> myCart = GetCartItems();
                    foreach (var cartItem in myCart)
                    {
                        // Iterate through all rows within shopping cart list
                        for (int i = 0; i < CartItemCount; i++)
                        {
                            if (cartItem.Product.ProductID == cartUpdates[i].ProductId)
                            {
                                if (cartUpdates[i].RemoveItem == true || cartUpdates[i].PurchaseQuantity < 1)
                                {
                                    RemoveItem(cartUID, cartItem.ProductID);
                                }
                                else
                                {
                                    UpdateItem(cartUID, cartItem.ProductID, cartUpdates[i].PurchaseQuantity);
                                }
                            }
                        }
                    }
                }
                catch (Exception exp)
                {
                    throw new Exception("ERROR: Unable to Update Cart Database - " + exp.Message.ToString(), exp);
                }
            }
        }

        private void RemoveItem(string cartUID, int productId)
        {
            using (var _db=new ProductContext())
            {
                var myItem = _db.CartItems.Where(c => c.CartItemUID == cartUID && c.Product.ProductID == productId).SingleOrDefault();
                if (myItem != null)
                {
                    _db.CartItems.Remove(myItem);
                    _db.SaveChanges();
                }
            }
        }

        private void UpdateItem(string cartUID, int productID, int purchaseQuantity)
        {
            using (var _db = new ProductContext())
            {
                var myItem = _db.CartItems.Where(c => c.CartItemUID == cartUID && c.Product.ProductID == productID).SingleOrDefault();
                if (myItem != null)
                {
                    myItem.Quantity = purchaseQuantity;
                    _db.SaveChanges();
                }
            }
        }     

        public void EmptyCart()
        {
            using (var _db = new ProductContext())
            {
                ShoppingCartUID = GetCartUID();
                var myItems = _db.CartItems.Where(c => c.CartItemUID == ShoppingCartUID);
                foreach (var item in myItems)
                {
                    _db.CartItems.Remove(item);
                }

                _db.SaveChanges();
            }
        }

        public int GetCount()
        {
            ShoppingCartUID = GetCartUID();
            int? count = (from cartItems in _db.CartItems
                          where cartItems.CartItemUID == ShoppingCartUID
                          select (int?)cartItems.Quantity).Sum();

            // Return 0 if all entries are null         
            return count ?? 0;

        }
        public void Dispose()
        {
            if (_db!=null)
            {
                _db.Dispose();
                _db = null;
            }
        }
    }
}