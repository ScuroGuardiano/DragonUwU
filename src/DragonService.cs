using System.Collections.Generic;
using System.Linq;
using System;
using DragonsUwU.Database;

namespace DragonsUwU
{
    class DragonService
    {
        /// <summary>
        /// Tries to find dragons by tags, if no dragons is found it will return empty list
        /// </summary>
        public List<Dragon> FindDragons(List<string> stringTags)
        {
            using(var db = new DragonContext()) {
                var dragonsMatchTags = GetQueryableDragonThatMatchTags(db, stringTags);
                return dragonsMatchTags?.ToList() ?? new List<Dragon>();
            }
        }
        public Dragon FindRandomDragon(List<string> stringTags)
        {
            using(var db = new DragonContext()) {
                var dragonsMatchTags = GetQueryableDragonThatMatchTags(db, stringTags);
                
                if(dragonsMatchTags == null)
                    return null;

                int dragonsFound = dragonsMatchTags.Count();
                if(dragonsFound == 0)
                    return null;

                var rand = new Random();
                int toSkip = rand.Next(0, dragonsFound);

                return dragonsMatchTags.Skip(toSkip).Take(1).First();
            }
        }
        public void AddDragon(List<string> stringTags, string fileName)
        {
            using(var db = new DragonContext()) {
                List<Tag> tags = CreateTagListFromStringTags(db, stringTags);
                var dragon = new Dragon() { FileName = fileName };
                List<DragonTag> dragonTags = CreateDragonTags(tags, dragon);

                dragon.DragonTags = dragonTags;
                db.Add(dragon);
                db.SaveChanges();
            }
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

        private IQueryable<Dragon> GetQueryableDragonThatMatchTags(
            DragonContext db,
            List<string> stringTags
        ) {
            var query =
                from dragon in db.Dragons
                where (
                    (from dt in dragon.DragonTags
                     where stringTags.Contains(dt.Tag.TagText)
                     select dt).Count() == stringTags.Count()
                )
                select dragon;
            return query;
        }

        private List<DragonTag> CreateDragonTags(List<Tag> tags, Dragon dragon) {
            var dragonTags = new List<DragonTag>();
            tags.ForEach(tag => dragonTags.Add(new DragonTag() { Tag = tag, Dragon = dragon }));
            return dragonTags;
        }
    }
}