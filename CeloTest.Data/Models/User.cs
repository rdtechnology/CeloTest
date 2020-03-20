using System;
using System.Collections.Generic;

namespace CeloTest.Data.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Image { get; set; }
        public bool? IsActive { get; set; }
    }
}
