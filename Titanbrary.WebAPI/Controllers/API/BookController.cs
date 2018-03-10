﻿using System;
using System.Collections.Generic;
using System.Web.Http;
using Titanbrary.Common.Interfaces.BusinessObjects;
using Titanbrary.Common.Models;

namespace Titanbrary.WebAPI.Controllers
{
    [RoutePrefix("api/Book")]
    public class BookController : ApiController
	{
        
        private readonly IBook _Book;
      
        public BookController(IBook _book)
        {
            _Book = _book;
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
			var list = _Book.UpdateBook(book);
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