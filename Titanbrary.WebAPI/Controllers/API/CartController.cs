using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Net.Mail;
using Titanbrary.Common.Interfaces.BusinessObjects;
using Titanbrary.Common.Models;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;

namespace Titanbrary.WebAPI.Controllers
{
    public class CartController : ApiController
    {
		private readonly ICart _Cart;
		private readonly IBook _Book;
        private readonly IAccount _Account;
        private ApplicationUserManager _UserManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _UserManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _UserManager = value;
            }
        }

        public CartController(ICart _cart, IBook _book, IAccount _account)
		{
			_Cart = _cart;
			_Book = _book;
            _Account = _account;
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

		// POST api/<controller>
		[Route("CreateCart")]
		[HttpPost]
		public IHttpActionResult CreateCart([FromBody] CartModel cart)
		{
			var list = _Cart.CreateCart(cart);
            if (list)
                return Ok();
			return BadRequest();
		}

        // POST api/<controller>
        [Route("Checkout/{cartID}")]
        [HttpPost]
        public async Task<IHttpActionResult> Checkout(Guid cartID)
        {
            var list = _Cart.Checkout(cartID);

            var cart = _Cart.GetCart(cartID);
            var userID = cart.UserID;
            var cartXBookList = cart.BookList;
            List<BookModel> bookList = new List<BookModel>();
            foreach (var book in cartXBookList)
            {
                bookList.Add(_Book.GetBookByBookID(book.BookID));
            }
            
            var currentUser = await _UserManager.FindByIdAsync(userID.ToString());
            var user = _Account.GetUserInfo(currentUser);
            CheckoutEmail(user, bookList, cartXBookList);

            if (list)
                return Ok();
            return BadRequest();
        }

        public async void CheckoutEmail(UserModel model, List<BookModel> bookList, List<CartXBookModel> cartXBookList)
        {
            string booksHTML = "<table><tr><th>Title</th><th>Author</th><th>Quantity</th></tr>";
            foreach (var book in bookList)
            {
                var quantity = cartXBookList.Where(b => b.BookID == book.BookID).Select(b => b.Quantity).FirstOrDefault();
                booksHTML += "<tr><td>" + book.Name + "</td>" + "<td>" + book.Author + "</td>" + "<td>" + quantity + "</td></tr>";
            }
            booksHTML += "</table>";

            SmtpClient mailClient = new SmtpClient("smtp.gmail.com", 587);
            mailClient.Credentials = new NetworkCredential("titanbrary.reminders@gmail.com", "titanbraryreminders");

            MailMessage email = new MailMessage();
            email.From = new MailAddress("Titanbrary@gmail.com");
            email.To.Add(model.Email);
            email.Subject = "Checkout!";
            email.Body = string.Format("<p>Hello {0} {1}</p><p>Thank you for checking out!</p><p>Here is a list of what you got:</p>{2}<p>Thanks,</p><p>Titanbrary Team</p>", model.FirstName, model.LastName, booksHTML);

            await mailClient.SendMailAsync(email);
        }
    }
}
