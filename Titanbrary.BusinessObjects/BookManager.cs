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

		public virtual BookModel GetBookByBookID(Guid bookID)
		{
			var book = _bookDAC.GetBookByBookID(bookID);
			return book;
		}

		public virtual bool CreateBook(BookModel book)
		{
			var result = _bookDAC.CreateBook(book);
			return result;
		}

		public virtual bool UpdateBook(BookModel book)
		{
			var result = _bookDAC.UpdateBook(book);
			return result;
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

		public virtual GenreModel GetGenreByGenreID(Guid genreID)
		{
			var genre = _bookDAC.GetGenreByGenreID(genreID);
			return genre;
		}

		public virtual bool CreateGenre(GenreModel genre)
		{
			var result = _bookDAC.CreateGenre(genre);
			return result;
		}

		public virtual bool UpdateGenre(GenreModel genre)
		{
			var result = _bookDAC.UpdateGenre(genre);
			return result;
		}

		#endregion
	}
}
