namespace PandaStroi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("u0597863_PandaStroi.Callback")]
    public partial class Callback
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Callback()
        {
            Log = new HashSet<Log>();
        }

        public int CallbackId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Dt { get; set; }

        [StringLength(150)]
        public string FIO { get; set; }

        [StringLength(150)]
        public string email { get; set; }

        [StringLength(150)]
        public string Phone { get; set; }

        public int? StatusId { get; set; }

        public bool isActive { get; set; }

        [StringLength(550)]
        public string Comment { get; set; }


        public virtual StatusList StatusList { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Log> Log { get; set; }
    }
}
