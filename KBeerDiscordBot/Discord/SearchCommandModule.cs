using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using KBeerDiscordBot.Abstractions;
using KBeerDiscordBot.Models;
using System.Text;
using System.Threading.Tasks;

namespace KBeerDiscordBot.Discord
{

    public class SearchCommandModule : BaseCommandModule
    {

        private IBeerService _beerService;

        public SearchCommandModule(IBeerService beerService)
        {
            _beerService = beerService;
        }

        [Command("search"), Description("Returns info on the first result of a beer search.\r\nUsage: search [beername]")]
        public async Task SearchCommand(CommandContext context, [RemainingText]string searchString)
        {
            string search = context.RawArgumentString.Trim();
            await context.RespondAsync("Searching for: " + search);
            await context.TriggerTypingAsync();

            var beer = await _beerService.SearchSingleBeerAsync(search);
            if (beer != null)
            {

                await context.RespondAsync("", embed: new DSharpPlus.Entities.DiscordEmbedBuilder { ImageUrl = beer.ImageLink });

                StringBuilder sb = new StringBuilder();
                sb.AppendLine();
                sb.AppendLine("**" + beer.Name);
                sb.AppendLine();
                sb.AppendLine("ABV:** " + beer.ABV + "   **IBU:** " + beer.IBU);
                sb.AppendLine("**Style:** " + beer.Style);
                sb.AppendLine("**Description:** " + beer.Description);
                sb.AppendLine();
                sb.AppendLine("**Brewed by:** " + beer.Brewery.Name + " (" + beer.Brewery.City + ", " + beer.Brewery.State + ", " + beer.Brewery.Country + ")");
                sb.AppendLine(beer.Brewery.Website);

                await context.RespondAsync(sb.ToString());
            }
            else
            {
                await context.RespondAsync("\r\nNo results could be found.");
            }
        }

    }
}
