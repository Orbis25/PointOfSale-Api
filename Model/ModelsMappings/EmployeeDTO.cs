using Model.Enums.Shared;
using Model.Enums.User;
using System;


namespace Model.ModelsMappings
{
    public class EmployeeDTO
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Avatar { get; set; }
        public string EmployeeCode { get; set; }
        public string Dni { get; set; }
        public State State { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Rol Rol { get; set; }
        public string UserId { get; set; }
    }
}
