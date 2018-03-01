using System.Collections.Generic;
using Titanbrary.Common.Models;
using Titanbrary.Common.Interfaces.BusinessObjects;
using Titanbrary.Common.Interfaces.Data;

namespace Titanbrary.BusinessObjects
{
	public class BookManager : IBook
	{
		//Change how to initialize the interface
		protected IBookDAC _book = null;
		public BookManager(IBookDAC book)
		{
			_book = book;
		}

		public virtual List<BookModel> GetAllBooks()
		{
			var list = _book.GetAllBooks();
			return list;
		}
	}
}
