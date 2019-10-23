using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Model.Enums.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.Models
{
    //Class Base 
    public class User : IdentityUser
    {

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        public Rol Rol { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
    }

}
