using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Titanbrary.Common.Models;
using Titanbrary.Common.Interfaces.Data;

namespace Titanbrary.Data.DACs
{
	public class BookDAC: IBookDAC
	{
		#region Book

		public virtual List<BookModel> GetAllBooks()
		{
			List<BookModel> result = new List<BookModel>();
			using (TitanbraryEntities ctx = new TitanbraryEntities())
			{
                result = ctx.Books.Select(b => new BookModel()
                {
                    Name = b.Name,
                    Author = b.Author,
                    Publisher = b.Publisher,
                    ISBN = b.ISBN,
                    Edition = b.Edition,
                    Year = b.Year,
                    Quantity = b.Quantity,
					Language = b.Language,
                    Picture = b.Picture,
                    Keywords = b.Keywords,
                    Active = b.Active,
                    Description = b.Description,
                    Timestamp = b.Timestamp,
                    BookID = b.BookID
                }).ToList();
            }
			return result;
		}

		public virtual List<BookModel> GetBooksByGenreID(Guid genreID)
		{
			List<BookModel> result = new List<BookModel>();
			using (TitanbraryEntities ctx = new TitanbraryEntities())
			{
				result = ctx.Books.Where(b => b.Genres.Any(g => g.GenreID == genreID)).Select(b => new BookModel()
				{
					Name = b.Name,
					Author = b.Author,
					Publisher = b.Publisher,
					ISBN = b.ISBN,
					Edition = b.Edition,
					Year = b.Year,
					Quantity = b.Quantity,
					Language = b.Language,
					Picture = b.Picture,
					Keywords = b.Keywords,
					Active = b.Active,
					Description = b.Description,
					Timestamp = b.Timestamp,
					BookID = b.BookID
				}).ToList();
			}
			return result;
		}

		public virtual BookModel GetBookByBookID(Guid bookID)
		{
			List<BookModel> result = new List<BookModel>();
			using (TitanbraryEntities ctx = new TitanbraryEntities())
			{
				result = ctx.Books.Where(b => b.BookID == bookID).Select(b => new BookModel()
				{
					Name = b.Name,
					Author = b.Author,
					Publisher = b.Publisher,
					ISBN = b.ISBN,
					Edition = b.Edition,
					Year = b.Year,
					Quantity = b.Quantity,
					Language = b.Language,
					Picture = b.Picture,
					Keywords = b.Keywords,
					Active = b.Active,
					Description = b.Description,
					Timestamp = b.Timestamp,
					BookID = b.BookID
				}).ToList();
			}
			return result[0];
		}

		public virtual bool CreateBook(BookModel book)
		{
			using (TitanbraryEntities ctx = new TitanbraryEntities())
			{
				ctx.Books.Add(new Book
				{
					Name = book.Name,
					Author = book.Author,
					Publisher = book.Publisher,
					ISBN = book.ISBN,
					Edition = book.Edition,
					Year = book.Year,
					Quantity = book.Quantity,
					Language = book.Language,
					Picture = book.Picture,
					Keywords = book.Keywords,
					Active = book.Active,
					Description = book.Description,
					Timestamp = book.Timestamp,
					BookID = book.BookID
				});
				ctx.SaveChanges();
			}
			return true;
		}

		#endregion

		#region Genre

		public virtual List<GenreModel> GetAllGenres()
		{
			List<GenreModel> result = new List<GenreModel>();
			using (TitanbraryEntities ctx = new TitanbraryEntities())
			{
				result = ctx.Genres.Select(g => new GenreModel()
				{
					Title = g.Title,
					GenreID = g.GenreID
				}).ToList();
			}
			return result;
		}

		public virtual List<GenreModel> GetGenresByBookID(Guid bookID)
		{
			List<GenreModel> result = new List<GenreModel>();
			using (TitanbraryEntities ctx = new TitanbraryEntities())
			{
				result = ctx.Genres.Where(g => g.Books.Any(b => b.BookID == bookID)).Select(g => new GenreModel()
				{
					Title = g.Title,
					GenreID = g.GenreID
				}).ToList();
			}
			return result;
		}

		public virtual GenreModel GetGenreByGenreID(Guid genreID)
		{
			List<GenreModel> result = new List<GenreModel>();
			using (TitanbraryEntities ctx = new TitanbraryEntities())
			{
				result = ctx.Genres.Where(g => g.GenreID == genreID).Select(g => new GenreModel()
				{
					Title = g.Title,
					GenreID = g.GenreID
				}).ToList();
			}
			return result[0];
		}

		#endregion
	}
}
