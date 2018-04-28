using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanbrary.Common.Interfaces.BusinessObjects;
using Titanbrary.Common.Interfaces.Data;
using Titanbrary.Common.Models;

namespace Titanbrary.BusinessObjects
{
	public class CartManager : ICart
	{
		private readonly ICartDAC _CartDAC;
		public CartManager(ICartDAC _cartDAC)
		{
			_CartDAC = _cartDAC;
		}

		public virtual bool CreateCart(CartModel cart)
		{
			var result = _CartDAC.CreateCart(cart);
			return result;
		}

		public virtual CartModel GetCart(Guid cartID)
		{
			var cart = _CartDAC.GetCart(cartID);
           
			return cart;
		}

        public virtual CartModel GetCartByUserID(Guid userID)
        {
            var cart = _CartDAC.GetCartByUserID(userID);
            return cart;
        }

        public virtual bool Checkout(Guid cartID)
        {
            var result = _CartDAC.Checkout(cartID);
            return result;
        }
    }
}
