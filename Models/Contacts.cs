using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ContactBook.Models
{
    public class Contacts
    {
        public int Id { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string name { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Surname")]
        public string surname { get; set; }

        [Phone]
        [DataType(DataType.PhoneNumber)]
        [Display(Name="Telephone Number")]
        public string phonenumber { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string email { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public string DOB { get; set; }

        public Contacts()
        {

        }
    }
}
