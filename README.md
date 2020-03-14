# Dragons UwU
## Taggable image bot for Discord

> Important note: 'Dragon' in code means 'Image' :3

---
### Some sh*t that nobody care of

Okey bot is now workable, ~~but yet so much to do ^^~~  
Hmm I guess I am done with this bot here :p I just wanted to build something with C# and I think it's finished.  
If you are experienced C# dev and your eyes starts to bleed when you read code feel free to open issue and tell me what I could do better!

---

### Get random image ^^
Just use ```$$tag``` or multiple tags ```$$tag1 tag2 tag3``` on text channel on Discord. If you DM to bot ommit command prefix.

### Adding Images
If your discord client id is in AdministratorIds in config.json you can add images. To do it you just go to Direct Message with bot, send image and in message you type tags OR type tags and at the end type url to image with http:// or https:// prefix, example  
```meme cats https://<rest-of-url>```
> Unfortunately there's one issue... Discord won't allow you to send some nsfw images to bot, so... You can't add them that way... BUT! You can send just url to an image ^^

---

### How to run this shit?
1. Install dotnet core
2. Install ef tools: ```dotnet tool install --global dotnet-ef```
3. Clone this repo ;)
4. Restore all needed packages: ```nuget restore```
5. Update database: ```dotnet ef database update```
6. Change config-pattern.json to config.json and update your settings
7. Run this shit: ```dotnet run```

---

# LICENSE
MIT