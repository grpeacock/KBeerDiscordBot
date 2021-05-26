using KBeerDiscordBot.Abstractions;
using KBeerDiscordBot.Discord;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KBeerDiscordBot.UnitTests.Discord.Commands
{
    /*
        Note: It's apparently well known that DSharpPlus is not unit testable due to the CommandContext class.
        Well known except to me when choosing it originally.

        Tests could be written if all command logic were extracted to a separate service, but the abstraction may not be worth it here.
    */
    [TestClass]
    public class SearchCommandModuleUnitTests
    {
        [TestMethod]
        public async Task GivenAValidSearch_WhenSearchCommandIsIssued_BeerServiceSearchIsInitiated()
        {
        }

        [TestMethod]
        public async Task GivenAValidSearch_WhenSearchCommandIsIssued_ReplyHasAllInformation()
        {

        }

        [TestMethod]
        public async Task GivenSearchFindsNoResults_WhenSearchCommandIsIssued_NoResultReplyReturned()
        {
            var searchCommand = new SearchCommandModule(Mock.Of<IBeerService>());
        }
    }
}
