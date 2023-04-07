using System.ComponentModel.DataAnnotations;

namespace AvtotestMVC.Models;

public class CreateUserModel
{
    [Required]
    [StringLength(40, MinimumLength =3)]
    public string Username { get; set; }
    [Required]
    [StringLength(40, MinimumLength = 3)]
    public string Name { get; set; }
    [Required]
    [RegularExpression("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$",
        ErrorMessage = "password must contain minimum eight characters, at least one letter and one number") ]
    public string Password { get; set; }
    [Required]
    public IFormFile Photo { get; set; }
}
