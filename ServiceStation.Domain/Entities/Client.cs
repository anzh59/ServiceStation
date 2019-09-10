using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ServiceStation.Domain.Entities
{
    public class Client
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Description = "First Name")]
        [Required(ErrorMessage = "Please enter first name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter birth date")]
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter phone number")]
        public string Phone { get; set; }

        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", 
            ErrorMessage = "Email should be in the following format: email@domain.com")]
        public string Email { get; set; }
    }
}
