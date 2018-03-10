using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanbrary.Common.Models;

namespace Titanbrary.Common.Interfaces.BusinessObjects
{
	public interface ICart
	{
		bool CreateCart(CartModel cart);

		CartModel GetCart(Guid cartID);
	}
}
