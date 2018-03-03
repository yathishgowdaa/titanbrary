using System;
using System.Collections.Generic;
using Titanbrary.Common.Models;

namespace Titanbrary.Common.Interfaces.BusinessObjects
{
	public interface IBook
	{
		#region Book

		List<BookModel> GetAllBooks();

		List<BookModel> GetBooksByGenreID(Guid bookID);

		#endregion

		#region Genre

		List<GenreModel> GetAllGenres();

		List<GenreModel> GetGenresByBookID(Guid bookID);

		#endregion
	}
}
