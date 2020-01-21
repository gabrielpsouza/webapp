using System;

namespace WebApp.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public int Age { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
        public DateTime DateCreate { get; set; }
        public string Address { get; set; }
        public string Obs { get; set; }
    }
}