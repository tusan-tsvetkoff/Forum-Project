using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Contracts.Post
{
    public record PostRequest(
        [Required(AllowEmptyStrings = false, ErrorMessage = "The {0} field is required and must not be an empty string.")]
        [MaxLength(64, ErrorMessage = "The {0} field must be less than {1} characters.")]
        [MinLength(16, ErrorMessage = "The {0} field must be at least {1} character.")]
        string Title,

       [Required(AllowEmptyStrings = false, ErrorMessage = "The {0} field is required and must not be an empty string.")]
       [MaxLength(8192, ErrorMessage = "The {0} field must be less than {1} characters.")]
       [MinLength(32, ErrorMessage = "The {0} field must be at least {1} character.")]
       string Content,

       Guid UserId);

}
