using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace officeApi.Models
{
    [Table("UserSettings")]
    public class UserSettings
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string UserName { get; set; }

        [StringLength(100)]
        public string AvatarName { get; set; }

        public int StyleMode { get; set; }
        
        [StringLength(100)]
        public string AvatarCaption { get; set; }
    }
}