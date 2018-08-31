using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace KBeerDiscordBot {

    class CommandManager {

        [Command("search"), Description("Returns info on the first result of an Untappd search.\r\nUsage: search [beername]\r\n       search beer [beername]\r\n       search brewery [breweryname]")]
        public async Task Search(CommandContext cc) {

        }

    }
}
