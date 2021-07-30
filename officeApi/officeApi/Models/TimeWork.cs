using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace officeApi.Models
{
    [Table("TimeWork")]
    public class TimeWork
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string UserName { get; set; }

        [StringLength(100)]
        public string Monday { get; set; }

        [StringLength(100)]
        public string Tuesday { get; set; }

        [StringLength(100)]
        public string Wednesday { get; set; }

        [StringLength(100)]
        public string Thursday { get; set; }

        [StringLength(100)]
        public string Friday { get; set; }

        [StringLength(100)]
        public string MondayDescription { get; set; }

        [StringLength(100)]
        public string TuesdayDescription { get; set; }

        [StringLength(100)]
        public string WednesdayDescription { get; set; }

        [StringLength(100)]
        public string ThursdayDescription { get; set; }

        [StringLength(100)]
        public string FridayDescription { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }
    }
}