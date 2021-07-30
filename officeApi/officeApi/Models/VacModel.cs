using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace officeApi.Models
{
    public partial class VacModel : DbContext
    {
        public VacModel()
            : base("name=VacModel")
        {
        }

        public virtual DbSet<Vacation> Vacation { get; set; }
        public virtual DbSet<Questionnaire> Questionnaire { get; set; }
        public virtual DbSet<TimeWork> TimeWork { get; set; }
        public virtual DbSet<UserSettings> UserSettings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}
