
using System.ComponentModel.DataAnnotations;

namespace Auth.Models
{
    public class User
    {
        public int Id { get; set; }
        
        public string Login { get; set; }
      
        public string Password { get; set; }

        public string Date { get; set; }
    }
}
