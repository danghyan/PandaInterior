namespace PandaStroi.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class PandaContext : DbContext
    {
        public PandaContext()
            : base("name=PandaStroi")
        {
        }

        public virtual DbSet<Callback> Callback { get; set; }
        public virtual DbSet<Log> Log { get; set; }
        public virtual DbSet<StatusList> StatusList { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
   

        }
    }
}
