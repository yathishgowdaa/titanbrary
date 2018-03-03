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
        [Route("GetAll")]
        [HttpPost]
		public IHttpActionResult GetAll()
		{
            //Update to BookManager Interface
            //var list = _bookManager.GetAll();
            var list = _Book.GetAllBooks();
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