using System.Collections.Generic;
using Titanbrary.Common.Models;
using Titanbrary.Common.Interfaces.BusinessObjects;
using Titanbrary.Common.Interfaces.Data;

namespace Titanbrary.BusinessObjects
{
	public class BookManager : IBook
	{
        //Change how to initialize the interface
        private readonly IBookDAC _bookDAC;
		public BookManager(IBookDAC bookDAC)
		{
			_bookDAC = bookDAC;
		}

		public virtual List<BookModel> GetAllBooks()
		{
			var list = _bookDAC.GetAllBooks();
			return list;
		}
	}
}
