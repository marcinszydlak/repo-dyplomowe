//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class Uczniowie
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Uczniowie()
        {
            this.Rozwiązania = new HashSet<Rozwiązania>();
        }
    
        public int IdUcznia { get; set; }
        public string Imię { get; set; }
        public string Nazwisko { get; set; }
        public string Login { get; set; }
        public string Hasło { get; set; }
        public int IdKlasy { get; set; }
    
        public virtual Klasy Klasy { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rozwiązania> Rozwiązania { get; set; }
    }
}
