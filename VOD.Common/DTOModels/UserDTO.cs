using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VOD.Common.DTOModels.Admin
{
    public class UserDTO
    {
        [Required]
        [Display(Name = "User Id")]
        public string Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Is Admin")]
        public bool IsAdmin { get; set; }
        public ButtonDTO ButtonDTO { get { return new ButtonDTO(Id); } }

        public TokenDTO Token { get; set; }

    }
}
