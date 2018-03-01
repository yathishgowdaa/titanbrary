using System.Collections.Generic;
using Titanbrary.Common.Models;
using Titanbrary.Common.Interfaces.BusinessObjects;
using Titanbrary.Common.Interfaces.Data;

namespace Titanbrary.BusinessObjects
{
	public class BookManager : IBookManager
	{
		//Change how to initialize the interface
		protected IBook _book = null;
		public BookManager(IBook book)
		{
			_book = book;
		}

		public virtual List<BookModel> GetAll()
		{
			var list = _book.GetAll();
			return list;
		}
	}
}
