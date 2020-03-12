# Dragons UwU
## Taggable image bot for Discord

> Important note: 'Dragon' in code means 'Image' :3

Okey bot is now workable, but yet so much to do ^^

### Get random image ^^
Just use ```$$tag``` or multiple tags ```$$tag1 tag2 tag3``` on text channel on Discord. If you DM to bot ommit command prefix.

### Adding Images
If your discord client id is in AdministratorIds in config.json you can add images. To do it you just go to Direct Message with bot, send image and in message you type tags  
> Unfortunately there's one issue... Discord won't allow you to send some nsfw images to bot, so... You can't add them that way... BUT! Don't you worry, I am currently working on better method of adding images!

### How to run this shit?
1. Install dotnet core
2. Install ef tools: ```dotnet tool install --global dotnet-ef```
3. Clone this repo ;)
4. Restore all needed packages: ```nuget restore```
5. Update database: ```dotnet ef database update```
6. Change config-pattern.json to config.json and update your settings
7. Run this shit: ```dotnet run```


# LICENSE
MIT