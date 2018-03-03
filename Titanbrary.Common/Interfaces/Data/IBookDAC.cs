using System.Collections.Generic;
using Titanbrary.Common.Models;

namespace Titanbrary.Common.Interfaces.Data
{
	public interface IBookDAC
	{
		List<BookModel> GetAllBooksDAC();
	}
}
