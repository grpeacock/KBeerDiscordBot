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

            string search = cc.RawArgumentString.Trim();
            await cc.RespondAsync("Searching for: " + search);
            await cc.TriggerTypingAsync();

            SimpleBeer beer = await Program.BeerAPI.SearchBeer(search);
            if (beer != null) {

                await cc.RespondAsync("", embed: new DSharpPlus.Entities.DiscordEmbedBuilder { ImageUrl = beer.ImageLink });

                StringBuilder sb = new StringBuilder();
                sb.AppendLine();
                sb.AppendLine("**" + beer.Name);
                sb.AppendLine();
                sb.AppendLine("ABV:** " + beer.ABV + "   **IBU:** " + beer.IBU);
                sb.AppendLine("**Style:** " + beer.Style);
                sb.AppendLine("**Description:** " + beer.Description);
                sb.AppendLine();
                sb.AppendLine("**Brewed by:** " + beer.Brewery + " (" + beer.BreweryCity + ", " + beer.BreweryState + ", " + beer.BreweryCountry +")");
                sb.AppendLine(beer.BreweryWebsite);

                await cc.RespondAsync(sb.ToString());
            } else {
                await cc.RespondAsync("\r\nNo results could be found.");
            }
        }

    }
}
