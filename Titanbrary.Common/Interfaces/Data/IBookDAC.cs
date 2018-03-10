using System;
using System.Collections.Generic;
using Titanbrary.Common.Models;

namespace Titanbrary.Common.Interfaces.Data
{
	public interface IBookDAC
	{
		#region Book

		List<BookModel> GetAllBooks();

		List<BookModel> GetBooksByGenreID(Guid genreID);

		BookModel GetBookByBookID(Guid bookID);

		bool CreateBook(BookModel book);

		bool UpdateBook(BookModel book);

		List<BookModel> SearchBooks(string searchString);

		bool AddBookToCart(Guid bookID);

		#endregion

		#region Genre

		List<GenreModel> GetAllGenres();

		List<GenreModel> GetGenresByBookID(Guid bookID);

		GenreModel GetGenreByGenreID(Guid genreID);

		bool CreateGenre(GenreModel genre);

		bool UpdateGenre(GenreModel genre);

		#endregion
	}
}
