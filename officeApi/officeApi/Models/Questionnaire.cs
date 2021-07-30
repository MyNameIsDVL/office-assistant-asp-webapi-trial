using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace officeApi.Models
{
    [Table("Questionnaire")]
    public class Questionnaire
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string UserName { get; set; }

        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(100)]
        public string MothersName { get; set; }

        [StringLength(100)]
        public string FathersName { get; set; }

        public DateTime DateOfBirth { get; set; }

        [StringLength(100)]
        public string Street { get; set; }

        public int HomeNumber { get; set; }

        public int ApartmentNumber { get; set; }

        [StringLength(100)]
        public string Education { get; set; }

        [StringLength(100)]
        public string SchoolName { get; set; }

        [StringLength(100)]
        public string LastEmployer { get; set; }

        public string PhoneNumber { get; set; }

        public string PeselNumber { get; set; }
    }
}