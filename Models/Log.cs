namespace PandaStroi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("u0597863_PandaStroi.Log")]
    public partial class Log
    {
        public int LogId { get; set; }

        public int? UserId { get; set; }

        public int? CallbackId { get; set; }

        [StringLength(250)]
        public string Operation { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime dt { get; set; }

        public virtual Callback Callback { get; set; }

        public virtual Users Users { get; set; }
    }
}
