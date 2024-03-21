using System.ComponentModel.DataAnnotations;

namespace StudentRegistration.Models
{
    public class usersModel
    {
        [Key]
        public int user_id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
     
    }
}
