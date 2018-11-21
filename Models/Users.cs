namespace PandaStroi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("u0597863_PandaStroi.Users")]
    public partial class Users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Users()
        {
            Log = new HashSet<Log>();
        }

        [Key]
        public int UserId { get; set; }

        [StringLength(50)]
        public string login { get; set; }

        [StringLength(50)]
        public string pass { get; set; }

        public DateTime? enterdt { get; set; }

        [StringLength(70)]
        public string email { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Log> Log { get; set; }

    }
}
