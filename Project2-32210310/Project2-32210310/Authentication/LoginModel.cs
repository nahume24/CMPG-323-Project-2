using System.ComponentModel.DataAnnotations;

namespace Project2_32210310.Authentication
{
    public class LoginModel
    {
        [Required(ErrorMessage = "User Name is required")] 

        public string Username { get; set; }



        [Required(ErrorMessage = "Password is required")]

        public string Password { get; set; }
    }
}
