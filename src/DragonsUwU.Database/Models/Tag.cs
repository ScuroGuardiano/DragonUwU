using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DragonsUwU.Database.Models
{
    class Tag
    {
        public Tag() {}
        public Tag(string stringTag) {
            TagText = stringTag.ToLower();
        }
        public int Id { get; set; }

        [Required]
        [MaxLength(48)]
        public string TagText { get; set; }

        public List<DragonTag> DragonTags { get; set; }
    }
}
