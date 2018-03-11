using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanbrary.Common.Models
{
	public class CartModel
	{
		public Guid CartID { get; set; }
		public Guid UserID { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime ModifiedDate { get; set; }
		public List<CartXBookModel> BookList { get; set; }
	}

	public class CartXBookModel
	{
		public Guid BookID { get; set; }
		public int Quantity { get; set; }
	}
}
