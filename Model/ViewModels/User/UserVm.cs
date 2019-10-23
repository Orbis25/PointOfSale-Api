using Model.Enums.Shared;
using Model.Enums.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.ViewModels.User
{
    public class UserVm
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public State State { get; set; }
    }
}
