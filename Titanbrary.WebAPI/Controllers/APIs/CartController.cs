using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Titanbrary.Common.Interfaces.BusinessObjects;

namespace Titanbrary.WebAPI.Controllers
{
    public class CartController : ApiController
    {
		private readonly ICart _Cart;
		private readonly IBook _Book;

		public CartController(ICart _cart, IBook _book)
		{
			_Cart = _cart;
			_Book = _book;
		}

		// POST api/<controller>
		[Route("GetCart/{cartID}")]
		[HttpPost]
		public IHttpActionResult GetCart(Guid cartID)
		{
			var list = _Cart.GetCart(cartID);
			return Ok(list);
		}

		// POST api/<controller>
		[Route("GetBook/{bookID}")]
		[HttpPost]
		public IHttpActionResult GetBook(Guid bookID)
		{
			var list = _Book.GetBookByBookID(bookID);
			return Ok(list);
		}
	}
}
