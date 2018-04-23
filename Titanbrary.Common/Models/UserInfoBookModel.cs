using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanbrary.Common.Models
{
    public class UserInfoBookModel
    {
        public UserModel user { get; set; }
        public BookModel book { get; set; }
        public List<BookModel> books { get; set; }
        public GenreModel genre { get; set; }
        public List<GenreModel> genres { get; set; }
        public Guid genreId { get; set; }
        public string bookId { get; set; }
        public CartModel cart { get; set; }
    }
}
