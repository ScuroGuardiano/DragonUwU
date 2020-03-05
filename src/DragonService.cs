using System.Collections.Generic;
using System.Linq;
using System;
using DragonsUwU.Database;

namespace DragonsUwU
{
    class DragonService
    {
        public void AddDragon(List<string> stringTags, string filePath)
        {
            using(var db = new DragonContext()) {
                List<Tag> tags = CreateTagListFromStringTags(db, stringTags);
                var dragon = new Dragon() { FileName = filePath };
                List<DragonTag> dragonTags = CreateDragonTags(tags, dragon);

                dragon.DragonTags = dragonTags;
                db.Add(dragon);
                db.SaveChanges();
            }
        }
        /// <summary>
        /// Tries to find dragons by tags, if no dragons is found it will return empty list
        /// </summary>
        public List<Dragon> FindDragons(List<string> stringTags) {
            using(var db = new DragonContext()) {
                var tags = FindTags(db, stringTags);
                var tagIds = tags.Select(t => t.Id);

                var query = 
                    from dragon in db.Dragons
                    where (
                        (from dt in dragon.DragonTags
                        where tagIds.Contains(dt.TagId)
                        select dt).Count() == tagIds.Count()
                    )
                    select dragon;
                return query.ToList();
            }
        }

        /// <summary>
        /// This one nice guy does not modify Database, will return Tags he will be able to find
        /// </summary>
        private List<Tag> FindTags(DragonContext db, List<string> stringTags) {
            var tags = new List<Tag>();
            stringTags.ForEach(t => {
                try 
                {
                    tags.Add(db.Tags.Single(tag => tag.TagText == t.ToLower()));
                }
                catch {}
            });
            return tags;
        }
        /// <summary>
        /// If Tags are in Database it will load them, if one does not exists, it will create it  
        /// This one boii can modify Database
        /// </summary>
        private List<Tag> CreateTagListFromStringTags(DragonContext db, List<string> stringTags)
        {
            var tags = new List<Tag>();

            stringTags.ForEach(el => {
                try
                {
                    var tag = db.Tags.Single(t => t.TagText == el.ToLower());
                    tags.Add(tag);
                }
                catch(System.InvalidOperationException) {
                    //Tag not exist, we must create it and add to Database
                    var tag = new Tag(el);
                    db.Add(tag);
                    db.SaveChanges();
                    tags.Add(tag);
                }
            });

            return tags;
        }

        private List<DragonTag> CreateDragonTags(List<Tag> tags, Dragon dragon) {
            var dragonTags = new List<DragonTag>();
            tags.ForEach(tag => dragonTags.Add(new DragonTag() { Tag = tag, Dragon = dragon }));
            return dragonTags;
        }
    }
}