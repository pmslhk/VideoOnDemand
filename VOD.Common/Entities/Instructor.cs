using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VOD.Common.Entities
{
    class Instructor
    {   [Key]
        public int Id { get; set; }

        [MaxLength(80), Required]
        public string Name { get; set; }

        [MaxLength(1024)]
        public string Description { get; set; }

        [MaxLength(1024)]
        public string Thumbnail { get; set; }

    }
}
