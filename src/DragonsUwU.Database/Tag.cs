using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DragonsUwU.Database
{
    class Tag
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(48)]
        public string TagText { get; set; }

        public List<DragonTag> DragonTags { get; set; }
    }
}
