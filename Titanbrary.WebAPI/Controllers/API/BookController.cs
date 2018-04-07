using System;
using System.Collections.Generic;
using System.Web.Http;
using Titanbrary.Common.Interfaces.BusinessObjects;
using Titanbrary.Common.Models;
using System.Net;
using System.Net.Mail;
using Hangfire;
using Microsoft.AspNet.Identity.Owin;
using System.Net.Http;

namespace Titanbrary.WebAPI.Controllers
{
	[RoutePrefix("api/Book")]
	public class BookController : ApiController
	{

		private readonly IBook _Book;
        private readonly IAccount _Account;
        private readonly ICart _Cart;
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

        public BookController(IBook _book, IAccount _account, ICart _cart)
		{
			_Book = _book;
            _Account = _account;
            _Cart = _cart;
        }

		#region Book

		// POST api/<controller>
		[Route("GetAllBooks")]
		[HttpPost]
		public IHttpActionResult GetAllBooks()
		{
			var list = _Book.GetAllBooks();
			return Ok(list);
		}

		// POST api/<controller>
		[Route("GetBooksByGenreID/{genreID}")]
		[HttpPost]
		public IHttpActionResult GetBooksByGenreID(Guid genreID)
		{
			var list = _Book.GetBooksByGenreID(genreID);
			return Ok(list);
		}

		// POST api/<controller>
		[Route("GetBookByBookID/{bookID}")]
		[HttpPost]
		public IHttpActionResult GetBookByBookID(Guid bookID)
		{
			var list = _Book.GetBookByBookID(bookID);
			return Ok(list);
		}

		// POST api/<controller>
		[Route("CreateBook")]
		[HttpPost]
		public IHttpActionResult CreateBook([FromBody] BookModel book)
		{
			var list = _Book.CreateBook(book);
			if (list)
				return Ok();
			return BadRequest();
		}

		// POST api/<controller>
		[Route("UpdateBook")]
		[HttpPost]
		public IHttpActionResult UpdateBook([FromBody] BookModel book)
		{
            bool isQuantityChanged = false;
			var list = _Book.UpdateBook(book, ref isQuantityChanged);

            if (isQuantityChanged && _Book.IsBookInWaitlist(book.BookID))
            {
                var userID = _Book.GetUserInWaitlist(book.BookID);

                CartXBookModel cartXBookModel = new CartXBookModel();
                cartXBookModel.BookID = book.BookID;
                cartXBookModel.Quantity = 1;              

                CartModel cart = _Cart.GetCartByUserID(userID);
                if (cart != null)
                    _Book.AddBookToCart(cart.CartID, cartXBookModel);
                else
                {
                    List<CartXBookModel> cartXBookModelList = new List<CartXBookModel>();
                    cartXBookModelList.Add(cartXBookModel);
                    _Cart.CreateCart(new CartModel
                    {
                        UserID = userID,
                        CartID = new Guid(),
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        BookList = cartXBookModelList
                    });
                }                

                var currentUser = _UserManager.FindByIdAsync(userID.ToString());
                var user = _Account.GetUserInfo(currentUser);
                OutWaitlistEmail(user, book);
            }

			if (list)
				return Ok();
			return BadRequest();
		}

		// POST api/<controller>
		[Route("SearchBooks/{searchString?}")]
		[HttpPost]
		public IHttpActionResult SearchBooks(string searchString)
		{
			var list = _Book.SearchBooks(searchString);
			return Ok(list);
		}

		// POST api/<controller>
		[Route("AddBookToCart/{cartID}")]
		[HttpPost]
		public IHttpActionResult AddBookToCart(Guid cartID, [FromBody] CartXBookModel cartXBook)
		{
			var list = _Book.AddBookToCart(cartID, cartXBook);
			if (list)
				return Ok();
			return BadRequest();
		}

		// POST api/<controller>
		[Route("DeleteBookFromCart/{cartID}/{bookID}")]
		[HttpPost]
		public IHttpActionResult DeleteBookFromCart(Guid cartID, Guid bookID)
		{
			var list = _Book.DeleteBookFromCart(cartID, bookID);
			if (list)
				return Ok();
			return BadRequest();
		}

        // POST api/<controller>
        [Route("AddBookToWaitlist/{bookID}/{userID}")]
        [HttpPost]
        public IHttpActionResult AddBookToWaitlist(Guid bookID, Guid userID)
        {
            var list = _Book.AddBookToWaitlist(bookID, userID);

            var book = _Book.GetBookByBookID(bookID);

            var currentUser = _UserManager.FindByIdAsync(userID.ToString());
            var user = _Account.GetUserInfo(currentUser);
            InWaitlistEmail(user, book);

            if (list)
                return Ok();
            return BadRequest();
        }

        // POST api/<controller>
        [Route("FeaturedBooks")]
		[HttpPost]
		public IHttpActionResult FeaturedBooks()
		{
			var list = _Book.FeaturedBooks();
			return Ok(list);
		}

        public async void InWaitlistEmail(UserModel model, BookModel book)
        {
            string booksHTML = "<table><tr><th>Title</th><th>Author</th></tr>";
            booksHTML += "<tr><td>" + book.Name + "</td>" + "<td>" + book.Author + "</td></tr>";
            booksHTML += "</table>";

            SmtpClient mailClient = new SmtpClient("smtp.gmail.com", 587);
            mailClient.Credentials = new NetworkCredential("titanbrary.reminders@gmail.com", "titanbraryreminders");

            MailMessage email = new MailMessage();
            email.From = new MailAddress("Titanbrary@gmail.com");
            email.To.Add(model.Email);
            email.Subject = "Waitlisted!";
            email.Body = string.Format("<p>Hello {0} {1}</p><p>Thank you for waitlisting! You will be notified as soon as it is available.</p><p>Here is a list of what you got:</p>{2}<p>Thanks,</p><p>Titanbrary Team</p>", model.FirstName, model.LastName, booksHTML);

            await mailClient.SendMailAsync(email);
        }

        public async void OutWaitlistEmail(UserModel model, BookModel book)
        {
            string booksHTML = "<table><tr><th>Title</th><th>Author</th></tr>";
            booksHTML += "<tr><td>" + book.Name + "</td>" + "<td>" + book.Author + "</td></tr>";
            booksHTML += "</table>";

            SmtpClient mailClient = new SmtpClient("smtp.gmail.com", 587);
            mailClient.Credentials = new NetworkCredential("titanbrary.reminders@gmail.com", "titanbraryreminders");

            MailMessage email = new MailMessage();
            email.From = new MailAddress("Titanbrary@gmail.com");
            email.To.Add(model.Email);
            email.Subject = "Book Available!";
            email.Body = string.Format("<p>Hello {0} {1}</p><p>Thank you for waiting! Your book is now available to checkout and has already been placed in your cart. Please login to checkout the book.</p><p>Here is a list of what you got:</p>{2}<p>Thanks,</p><p>Titanbrary Team</p>", model.FirstName, model.LastName, booksHTML);

            await mailClient.SendMailAsync(email);
        }

        #endregion

        #region Genre

        // POST api/<controller>
        [Route("GetAllGenres")]
		[HttpPost]
		public IHttpActionResult GetAllGenres()
		{
			var list = _Book.GetAllGenres();
			return Ok(list);
		}

		// POST api/<controller>
		[Route("GetGenresByBookID/{bookID}")]
		[HttpPost]
		public IHttpActionResult GetGenresByBookID(Guid bookID)
		{
			var list = _Book.GetGenresByBookID(bookID);
			return Ok(list);
		}

		// POST api/<controller>
		[Route("GetGenreByGenreID/{genreID}")]
		[HttpPost]
		public IHttpActionResult GetGenreByGenreID(Guid genreID)
		{
			var list = _Book.GetGenreByGenreID(genreID);
			return Ok(list);
		}

		// POST api/<controller>
		[Route("CreateGenre")]
		[HttpPost]
		public IHttpActionResult CreateGenre([FromBody] GenreModel genre)
		{
			var list = _Book.CreateGenre(genre);
			if (list)
				return Ok();
			return BadRequest();
		}

		// POST api/<controller>
		[Route("UpdateGenre")]
		[HttpPost]
		public IHttpActionResult UpdateGenre([FromBody] GenreModel genre)
		{
			var list = _Book.UpdateGenre(genre);
			if (list)
				return Ok();
			return BadRequest();
		}

		#endregion
	}
}