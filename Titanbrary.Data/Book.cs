//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Titanbrary.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Book
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Book()
        {
            this.Genres = new HashSet<Genre>();
            this.CartXBooks = new HashSet<CartXBook>();
        }
    
        public string Name { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string ISBN { get; set; }
        public Nullable<int> Edition { get; set; }
        public Nullable<System.DateTime> Year { get; set; }
        public int Quantity { get; set; }
        public string Language { get; set; }
        public byte[] Picture { get; set; }
        public string Keywords { get; set; }
        public bool Active { get; set; }
        public string Description { get; set; }
        public System.DateTime Timestamp { get; set; }
        public System.Guid BookID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Genre> Genres { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CartXBook> CartXBooks { get; set; }
    }
}
