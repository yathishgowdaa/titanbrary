using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanbrary.Common.Interfaces.Data;
using Titanbrary.Common.Models;

namespace Titanbrary.Data.DACs
{
	public class CartDAC : ICartDAC
	{
		public virtual bool CreateCart(CartModel cart)
		{
			using (TitanbraryEntities ctx = new TitanbraryEntities())
			{
				try
				{
					ctx.Carts.Add(new Cart
					{
						CartID = cart.CartID,
						CreatedDate = cart.CreatedDate,
						ModifiedDate = cart.ModifiedDate,
						UserID = cart.UserID
					});
					ctx.SaveChanges();
				}
				catch (Exception ex)
				{
					return false;
				}
			}
			return true;
		}

		public virtual CartModel GetCart(Guid cartID)
		{
			List<CartModel> result = new List<CartModel>();
			using (TitanbraryEntities ctx = new TitanbraryEntities())
			{
				result = ctx.Carts.Where(c => c.CartID == cartID).Select(c => new CartModel
				{
					CartID = c.CartID,
					UserID = c.UserID,
					CreatedDate = c.CreatedDate,
					ModifiedDate = c.ModifiedDate,
					BookList = c.CartXBooks.Where(cb => cb.CartID == c.CartID).Select(cb => new CartXBookModel
					{
						BookID = cb.BookID,
						Quantity = cb.Quantity
					}).ToList()
				}).ToList();
			}
			return result[0];
		}
	}
}
