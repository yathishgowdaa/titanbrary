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

		bool UpdateBook(BookModel book, ref bool isQuantityChanged);

		List<BookModel> SearchBooks(string searchString);

		bool AddBookToCart(Guid cartID, CartXBookModel cartXBook);

		bool DeleteBookFromCart(Guid cartID, Guid bookID);

        bool AddBookToWaitlist(Guid bookID, Guid userID);

        bool IsBookInWaitlist(Guid bookID);

        Guid GetUserInWaitlist(Guid bookID);

        List<BookModel> FeaturedBooks();

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
