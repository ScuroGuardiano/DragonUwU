using System.IO;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace DragonsUwU
{
    class DragonConfiguration
    {
        [Required]
        public string DiscordToken { get; set; }

        [Required]
        public string ConnectionString { get; set; }

        [Required]
        [MinLength(1)]
        public List<ulong> AdministratorsIds { get; set; }
        
        public string CommandPrefix { get; set; } = "$$";

        static public DragonConfiguration Config { get; set; }

        static public void LoadConfiguration() {
            Config = new DragonConfiguration();
            new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json", false)
                .Build()
                .Bind(Config);
            Validator.ValidateObject(Config, new ValidationContext(Config), true);
        }

    }
}