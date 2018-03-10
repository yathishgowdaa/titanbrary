using System;
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

  //      // GET api/<controller>
  //      public IEnumerable<string> Get()
		//{
		//	return new string[] { "value1", "value2" };
		//}

		//// GET api/<controller>/5
		//public string Get(int id)
		//{
		//	return "value";
		//}

        // POST api/<controller>
        [Route("GetAllBooks")]
        [HttpPost]
		public IHttpActionResult GetAllBooks()
		{
            var list = _Book.GetAllBooks();
			return Ok(list);
		}

		// POST api/<controller>
		[Route("GetAllGenres")]
		[HttpPost]
		public IHttpActionResult GetAllGenres()
		{
			var list = _Book.GetAllGenres();
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
		[Route("GetGenresByBookID/{bookID}")]
		[HttpPost]
		public IHttpActionResult GetGenresByBookID(Guid bookID)
		{
			var list = _Book.GetGenresByBookID(bookID);
			return Ok(list);
		}

		//// PUT api/<controller>/5
		//public void Put(int id, [FromBody]string value)
		//{
		//}

		//// DELETE api/<controller>/5
		//public void Delete(int id)
		//{
		//}
	}
}