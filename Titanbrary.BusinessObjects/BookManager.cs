﻿using System;
using System.Collections.Generic;
using Titanbrary.Common.Models;
using Titanbrary.Common.Interfaces.BusinessObjects;
using Titanbrary.Common.Interfaces.Data;

namespace Titanbrary.BusinessObjects
{
	public class BookManager : IBook
	{
        private readonly IBookDAC _BookDAC;
		public BookManager(IBookDAC _bookDAC)
		{
			_BookDAC = _bookDAC;
		}

		#region Book

		public virtual List<BookModel> GetAllBooks()
		{
			var list = _BookDAC.GetAllBooks();
			return list;
		}

		public virtual List<BookModel> GetBooksByGenreID(Guid genreID)
		{
			var list = _BookDAC.GetBooksByGenreID(genreID);
			return list;
		}

		public virtual BookModel GetBookByBookID(Guid bookID)
		{
			var book = _BookDAC.GetBookByBookID(bookID);
			return book;
		}

		public virtual bool CreateBook(BookModel book)
		{
			var result = _BookDAC.CreateBook(book);
			return result;
		}

		public virtual bool UpdateBook(BookModel book)
		{
			var result = _BookDAC.UpdateBook(book);
			return result;
		}

		public virtual List<BookModel> SearchBooks(string searchString)
		{
			var list = _BookDAC.SearchBooks(searchString);
			return list;
		}

		public virtual bool AddBookToCart(Guid bookID)
		{
			var book = _BookDAC.AddBookToCart(bookID);
			return book;
		}

		#endregion

		#region Genre

		public virtual List<GenreModel> GetAllGenres()
		{
			var list = _BookDAC.GetAllGenres();
			return list;
		}

		public virtual List<GenreModel> GetGenresByBookID(Guid bookID)
		{
			var list = _BookDAC.GetGenresByBookID(bookID);
			return list;
		}

		public virtual GenreModel GetGenreByGenreID(Guid genreID)
		{
			var genre = _BookDAC.GetGenreByGenreID(genreID);
			return genre;
		}

		public virtual bool CreateGenre(GenreModel genre)
		{
			var result = _BookDAC.CreateGenre(genre);
			return result;
		}

		public virtual bool UpdateGenre(GenreModel genre)
		{
			var result = _BookDAC.UpdateGenre(genre);
			return result;
		}

		#endregion
	}
}
