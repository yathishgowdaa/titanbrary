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
		public virtual CartModel GetCart(Guid cartID)
		{
			List<CartModel> result = new List<CartModel>();
			using (TitanbraryEntities ctx = new TitanbraryEntities())
			{
				//result = ctx.Cart.Where(c => c.cartID == cartID).Select(b => new CartModel()
				//{
					
				//}).ToList();
			}
			return result[0];
		}
	}
}
