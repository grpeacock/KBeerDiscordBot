using KBeerDiscordBot.Models;
using System.Threading.Tasks;

namespace KBeerDiscordBot.Abstractions
{
    public interface IBeerService
    {
        Task<Beer> SearchSingleBeerAsync(string search);
    }
}
