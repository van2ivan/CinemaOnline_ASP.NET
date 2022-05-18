using System.ComponentModel.DataAnnotations;

namespace CinemaOnline.Data.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Email is reqired")]
        public string EmailAdress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
