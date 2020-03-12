using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DragonsUwU.Database.Models;

namespace DragonsUwU.Database.Services
{
    class DragonService
    {
        /// <summary>
        /// Tries to find dragons by tags, if no dragons is found it will return empty list
        /// </summary>
        public async Task<List<Dragon>> FindDragonsAsync(List<string> stringTags)
        {
            using(var db = new DragonContext()) {
                var dragonsMatchTags = GetQueryableDragonThatMatchTags(db, stringTags);
                return await dragonsMatchTags?.ToListAsync() ?? new List<Dragon>();
            }
        }
        public async Task<Dragon> FindRandomDragonAsync(List<string> stringTags)
        {
            using(var db = new DragonContext()) {
                var dragonsMatchTags = GetQueryableDragonThatMatchTags(db, stringTags);
                
                if(dragonsMatchTags == null)
                    return null;

                int dragonsFound = await dragonsMatchTags.CountAsync();
                if(dragonsFound == 0)
                    return null;

                var rand = new Random();
                int toSkip = rand.Next(0, dragonsFound);

                return await dragonsMatchTags.Skip(toSkip).Take(1).FirstAsync();
            }
        }
        public async Task AddDragonAsync(List<string> stringTags, string fileName)
        {
            using(var db = new DragonContext()) {
                List<Tag> tags = await CreateTagListFromStringTagsAsync(db, stringTags);
                var dragon = new Dragon() { Filename = fileName };
                List<DragonTag> dragonTags = CreateDragonTags(tags, dragon);

                dragon.DragonTags = dragonTags;
                await db.AddAsync(dragon);
                await db.SaveChangesAsync();
            }
        }
        /// <summary>
        /// If Tags are in Database it will load them, if one does not exists, it will create it  
        /// This one boii can modify Database, but doesn't save changes
        /// </summary>
        private async Task<List<Tag>> CreateTagListFromStringTagsAsync(DragonContext db, List<string> stringTags)
        {
            var lowerStringTags = stringTags.ConvertAll(st => st.ToLower());
            // Here we will have tags that exists in database
            List<Tag> tagsFound = await 
            (
                from t in db.Tags
                where lowerStringTags.Contains(t.TagText)
                select t
            ).ToListAsync();

            if(tagsFound.Count == stringTags.Count) 
                return tagsFound; // If we found all tags, we don't need to create missing
            
            // Okey, we need to add some tags, let's go with this shit
            List<string> tagsTextFound = tagsFound.ConvertAll(t => t.TagText);
            List<string> missingTags = 
            (
                from st in lowerStringTags
                where tagsTextFound.Contains(st) == false
                select st
            ).ToList();

            List<Tag> tagsToAdd = missingTags.ConvertAll(mt => new Tag(mt));
            await db.AddRangeAsync(tagsToAdd);

            return tagsFound.Concat(tagsToAdd).ToList();
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