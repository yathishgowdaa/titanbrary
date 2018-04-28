using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanbrary.Common.Interfaces.Data;
using Titanbrary.Common.Models;

namespace Titanbrary.Data.DACs
{
	public class CartDAC : ICartDAC
	{
		public virtual bool CreateCart(CartModel cart)
		{
			using (TitanbraryContainer ctx = new TitanbraryContainer())
			{
				try
				{
                    var cartId = Guid.NewGuid();

                    var target = new Cart();
                    target.CartID = Guid.NewGuid();
                    target.UserID = cart.UserID;
                    target.Completed = false;
                    target.CreatedDate = DateTime.UtcNow;
                    target.ModifiedDate = DateTime.UtcNow;                    
                    //List<CartXBook> bookList = new List<CartXBook>();
                                       
                    //target.CartXBooks = bookList;
                    //               ctx.Carts.Add(new Cart
                    //               {
                    //                   CartID = cartId,
                    //	CreatedDate = cart.CreatedDate,
                    //	ModifiedDate = cart.ModifiedDate,
                    //	UserID = cart.UserID,
                    //                   Completed = false
                    //});
                    ctx.Carts.Add(target);
                    ctx.SaveChanges();

                    foreach (var book in cart.BookList)
                    {
                        var t = new CartXBook();
                        t.CartID = target.CartID;
                        t.BookID = book.BookID;
                        t.Quantity = 1;
                        ctx.CartXBooks.Add(t);
                        //ctx.CartXBooks.Add(new CartXBook
                        //{
                        //    CartID = cartId,
                        //    BookID = book.BookID,
                        //    Quantity = 1
                        //});
                        ctx.SaveChanges();
                    }
                    
				}
				catch (Exception ex)
				{
					return false;
				}
			}
			return true;
		}

		public virtual CartModel GetCart(Guid cartId)
		{
			List<CartModel> result = new List<CartModel>();
           
			using (TitanbraryContainer ctx = new TitanbraryContainer())
			{
				result = ctx.Carts.Where(c => c.CartID == cartId && c.Completed == false).Select(c => new CartModel
				{                  
					CartID = c.CartID,
					UserID = c.UserID,
					CreatedDate = c.CreatedDate,
					ModifiedDate = c.ModifiedDate,
					BookList = c.CartXBooks.Where(cb => cb.CartID == c.CartID).Select(cb => new CartXBookModel
					{
						BookID = cb.BookID,
						Quantity = cb.Quantity
					}).ToList()                    
				}).ToList();
                if(result.Count() == 0)
                {
                    return null;
                }
                foreach(var books in result)
                {
                    books.Books = new List<BookModel>();
                    foreach(var book in books.BookList)
                    {
                        var detailedBook = new BookModel();
                        var b = ctx.Books.Where(bk => bk.BookID == book.BookID).FirstOrDefault();
                        detailedBook.BookID = b.BookID;
                        detailedBook.Author = b.Author;
                        detailedBook.Publisher = b.Publisher;
                        detailedBook.Edition = b.Edition;
                        detailedBook.Active = b.Active;
                        detailedBook.Description = b.Description;
                        detailedBook.ISBN = b.ISBN;
                        detailedBook.Keywords = b.Keywords;
                        detailedBook.Language = b.Language;
                        detailedBook.Name = b.Name;
                        detailedBook.Quantity = b.Quantity;
                        detailedBook.Year = b.Year;
                        detailedBook.Picture = b.Picture;
                        
                        foreach(var genre in b.Genres)
                        {
                            var g = ctx.Genres.Where(gr => gr.GenreID == genre.GenreID).FirstOrDefault();
                            detailedBook.GenreID = g.GenreID;
                        }
                       
                        books.Books.Add(detailedBook);
                    }
                }
                
			}
			return result[0];
		}

        public virtual CartModel GetCartByUserID(Guid userID)
        {
            var result = new CartModel();
            result.BookList = new List<CartXBookModel>();
            result.Books = new List<BookModel>();
            
            using (TitanbraryContainer ctx = new TitanbraryContainer())
            {
                var cart = ctx.Carts.Where(c => c.UserID == userID && c.Completed == false).FirstOrDefault();

                if(cart == null)
                {
                    return null;
                }

                result.CartID = cart.CartID;
                result.UserID = cart.UserID;
                result.CreatedDate = cart.CreatedDate;
                result.ModifiedDate = cart.ModifiedDate;
                
                foreach(var book in cart.CartXBooks)
                {
                    var books = new CartXBookModel();
                    var booksDetail = new BookModel();
                    books.BookID = book.BookID;
                    books.Quantity = book.Quantity;
                    result.BookList.Add(books);
                    var getBook = ctx.Books.Where(b => b.BookID == book.BookID).FirstOrDefault();
                    booksDetail.BookID = getBook.BookID;
                    booksDetail.Author = getBook.Author;
                    booksDetail.Publisher = getBook.Publisher;
                    booksDetail.Edition = getBook.Edition;
                    booksDetail.Active = getBook.Active;
                    booksDetail.Description = getBook.Description;
                    booksDetail.ISBN = getBook.ISBN;
                    booksDetail.Keywords = getBook.Keywords;
                    booksDetail.Language = getBook.Language;
                    booksDetail.Name = getBook.Name;
                    booksDetail.Quantity = getBook.Quantity;
                    booksDetail.Year = getBook.Year;
                    booksDetail.Picture = getBook.Picture;

                    foreach (var genre in getBook.Genres)
                    {
                        var g = ctx.Genres.Where(gr => gr.GenreID == genre.GenreID).FirstOrDefault();
                        booksDetail.GenreID = g.GenreID;
                    }
                    result.Books.Add(booksDetail);
                }

            }
            return result;
        }

        public virtual bool Checkout(Guid cartID)
        {
            using (TitanbraryContainer ctx = new TitanbraryContainer())
            {
                try
                {
                    var oldCart = ctx.Carts.SingleOrDefault(c => c.CartID == cartID);
                    oldCart.Completed = true;
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return true;
        }
	}
}
