using System;
using System.Collections.Generic;
using System.Text;

namespace CeloTest.Service.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public string PhoneNumber { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
