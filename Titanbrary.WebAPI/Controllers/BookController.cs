using System.Collections.Generic;
using System.Web.Http;
using Titanbrary.Common.Interfaces.BusinessObjects;

namespace Titanbrary.WebAPI.Controllers
{
	public class BookController : ApiController
	{
        protected IBook _book = null;
        public BookController()
        {
            _book = new BookManager();
        }


        // GET api/<controller>
        public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		// GET api/<controller>/5
		public string Get(int id)
		{
			return "value";
		}

		// POST api/<controller>
		[HttpPost]
		[Route("Book/GetAll")]
		public IHttpActionResult GetAll()
		{
			//Update to BookManager Interface
			var list = _bookManager.GetAll();
			return Ok(list);
		}

		// PUT api/<controller>/5
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE api/<controller>/5
		public void Delete(int id)
		{
		}
	}
}