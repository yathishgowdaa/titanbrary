using System.Collections.Generic;
using Titanbrary.Common.Models;

namespace Titanbrary.Common.Interfaces.BusinessObjects
{
	public interface IBook
	{
		List<BookModel> GetAllBooks();
	}
}
