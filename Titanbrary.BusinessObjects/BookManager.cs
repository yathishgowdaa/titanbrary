using System;
using System.Collections.Generic;
using Titanbrary.Common.Models;
using Titanbrary.Common.Interfaces.BusinessObjects;
using Titanbrary.Common.Interfaces.Data;

namespace Titanbrary.BusinessObjects
{
	public class BookManager : IBook
	{
        private readonly IBookDAC _bookDAC;
		public BookManager(IBookDAC bookDAC)
		{
			_bookDAC = bookDAC;
		}

		#region Book

		public virtual List<BookModel> GetAllBooks()
		{
			var list = _bookDAC.GetAllBooks();
			return list;
		}

		public virtual List<BookModel> GetBooksByGenreID(Guid genreID)
		{
			var list = _bookDAC.GetBooksByGenreID(genreID);
			return list;
		}

		#endregion

		#region Genre

		public virtual List<GenreModel> GetAllGenres()
		{
			var list = _bookDAC.GetAllGenres();
			return list;
		}

		public virtual List<GenreModel> GetGenresByBookID(Guid bookID)
		{
			var list = _bookDAC.GetGenresByBookID(bookID);
			return list;
		}

		#endregion
	}
}
