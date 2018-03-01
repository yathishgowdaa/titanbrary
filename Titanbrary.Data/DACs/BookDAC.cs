using System.Collections.Generic;
using System.Linq;
using System.Data;
using Titanbrary.Common.Models;

namespace Titanbrary.Data.DACs
{
	public class BookDAC
	{

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
					Languague = b.Language,
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





    }
}
