namespace officeApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Vacation")]
    public partial class Vacation
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string UserName { get; set; }

        [StringLength(40)]
        public string Type { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public int? VacDays { get; set; }

        public DateTime? DateIn { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
