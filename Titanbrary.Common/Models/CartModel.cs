using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanbrary.Common.Models
{
	public class CartModel
	{
		Guid CartID { get; set; }
		Guid UserID { get; set; }
		DateTime Date { get; set; }
		List<CartXBookModel> BookList { get; set; }
	}

	public class CartXBookModel
	{
		Guid BookID { get; set; }
		int Quantity { get; set; }
		int LineNumber { get; set; }
	}
}
