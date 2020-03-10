namespace DragonsUwU.Database.Models
{
    class DragonTag
    {
        public int DragonId { get; set; }
        public Dragon Dragon { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
