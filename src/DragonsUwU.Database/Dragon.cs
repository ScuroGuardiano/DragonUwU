using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DragonsUwU.Database
{
    class Dragon
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string FileName { get; set; }

        public List<DragonTag> DragonTags { get; set; }
    }
}
