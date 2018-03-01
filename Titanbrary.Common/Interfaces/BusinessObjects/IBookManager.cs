using System.Collections.Generic;
using Titanbrary.Common.Models;

namespace Titanbrary.Common.Interfaces.BusinessObjects
{
	public interface IBookManager
	{
		List<BookModel> GetAll();
	}
}
