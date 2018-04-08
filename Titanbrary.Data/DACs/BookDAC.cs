﻿using System;
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
				try
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
                        Timestamp = DateTime.UtcNow,
                        BookID = Guid.NewGuid()
					});
					ctx.SaveChanges();
				}
				catch (Exception ex)
				{
					return false;
				}				
			}
			return true;
		}

		public virtual bool UpdateBook(BookModel book)
		{
			using (TitanbraryEntities ctx = new TitanbraryEntities())
			{
				try
				{
					var oldBook = ctx.Books.SingleOrDefault(b => b.BookID == book.BookID);
					oldBook.Active = book.Active;
					oldBook.Author = book.Author;
					oldBook.Description = book.Description;
					oldBook.Edition = book.Edition;
					oldBook.ISBN = book.ISBN;
					oldBook.Keywords = book.Keywords;
					oldBook.Language = book.Language;
					oldBook.Name = book.Name;
					oldBook.Picture = book.Picture;
					oldBook.Publisher = book.Publisher;
					oldBook.Quantity = book.Quantity;
					oldBook.Timestamp = book.Timestamp;
					oldBook.Year = book.Year;
					ctx.SaveChanges();
				}
				catch (Exception ex)
				{
					return false;
				}
			}
			return true;
		}

		public virtual List<BookModel> SearchBooks(string searchString)
		{
			List<BookModel> result = new List<BookModel>();
			using (TitanbraryEntities ctx = new TitanbraryEntities())
			{
				result = ctx.Books.Where(b => b.Author.Contains(searchString) ||
											  b.Publisher.Contains(searchString) ||
											  b.ISBN.Contains(searchString) ||
											  b.Language.Contains(searchString) ||
											  b.Keywords.Contains(searchString) ||
											  b.Name.Contains(searchString) ||
											  b.Description.Contains(searchString)).Select(b => new BookModel()
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

		public virtual bool AddBookToCart(Guid cartID, CartXBookModel cartXBook)
		{
			using (TitanbraryEntities ctx = new TitanbraryEntities())
			{
				try
				{
					ctx.CartXBooks.Add(new CartXBook
					{
						BookID = cartXBook.BookID,
						CartID = cartID,
						Quantity = cartXBook.Quantity
					});
					ctx.SaveChanges();
				}
				catch (Exception ex)
				{
					return false;
				}
			}
			return true;
		}

		public virtual bool DeleteBookFromCart(Guid cartID, Guid bookID)
		{
			using (TitanbraryEntities ctx = new TitanbraryEntities())
			{
				try
				{
					ctx.CartXBooks.Remove(new CartXBook
					{
						BookID = bookID,
						CartID = cartID
					});
					ctx.SaveChanges();
				}
				catch (Exception ex)
				{
					return false;
				}
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

		public virtual bool CreateGenre(GenreModel genre)
		{
			using (TitanbraryEntities ctx = new TitanbraryEntities())
			{
				try
				{
                    ctx.Genres.Add(new Genre
                    {
                        Title = genre.Title,
                        GenreID = Guid.NewGuid()
					});
					ctx.SaveChanges();
				}
				catch (Exception ex)
				{
					return false;
				}

			}
			return true;
		}

		public virtual bool UpdateGenre(GenreModel genre)
		{
			using (TitanbraryEntities ctx = new TitanbraryEntities())
			{
				try
				{
					var oldGenre = ctx.Genres.SingleOrDefault(g => g.GenreID == genre.GenreID);
					oldGenre.Title = genre.Title;
					ctx.SaveChanges();
				}
				catch (Exception ex)
				{
					return false;
				}				
			}
			return true;
		}

		#endregion
	}
}
