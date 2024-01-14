using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared;

public class LoginModel
{
    public Guid Id { get; set; }
    [Required(ErrorMessage = "Username is required")]
    public string? Username { get; set; }
    [Required(ErrorMessage = "Password is required")]
    [StringLength(255, ErrorMessage = "Must be between 8 and 255 characters", MinimumLength = 8)]
    [DataType(DataType.Password)]
    public string? HashedPassword { get; set; }
    public string? Token { get; set; }
    public string? Role { get; set; }

}
