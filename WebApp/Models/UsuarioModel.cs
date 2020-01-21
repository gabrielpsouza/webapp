using System;

namespace WebApp.Models
{
    public class UsuarioModel
    {
        public int UserId { get; set; }
        public string User { get; set; }
        public int Age { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
        public DateTime DateCreate { get; set; }
        public string Endereco { get; set; }
        public string Obs { get; set; }
    }
}