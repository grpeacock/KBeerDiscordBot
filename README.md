# KBeerDiscordBot
A simple search and lookup Discord bot for finding information on beer. Provides a name, logo, ABV, IBU, description, and brewery in the current chatroom.

The bot taps into the Discord plugin API using the DSharpPlus (available on Nuget and https://dsharpplus.emzi0767.com/index.html) reference and the Untappd API (https://untappd.com/api/dashboard; while a wrapper is available on Nuget, I found it easier to use direct HTTPS requests).

This project was made as a bit of a "portfolio project" since I realized I didn't have any shareable C# experience. The goal was to show basic knowledge of C# and the ability to work with async programming and 3rd party APIs.

License: MIT 2021 Gregory R. Peacock (https://github.com/grpeacock/KBeerDiscordBot/blob/master/KBeerDiscordBot/LICENSE)
