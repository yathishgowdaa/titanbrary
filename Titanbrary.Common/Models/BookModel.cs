using System;

namespace Titanbrary.Common.Models
{
	public class BookModel
	{
		public string Name { get; set; }
		public string Author { get; set; }
		public string Publisher { get; set; }
		public string ISBN { get; set; }
		public int? Edition { get; set; }
		public DateTime? Year { get; set; }
		public int Quantity { get; set; }
		public string Languague { get; set; }
		public byte[] Picture { get; set; }
		public string Keywords { get; set; }
		public bool Active { get; set; }
		public string Description { get; set; }
		public DateTime Timestamp { get; set; }
		public Guid BookID { get; set; }
	}
}
