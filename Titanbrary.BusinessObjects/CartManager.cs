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

		public virtual CartModel GetCart(Guid cartID)
		{
			var cart = _CartDAC.GetCart(cartID);
			return cart;
		}
	}
}
