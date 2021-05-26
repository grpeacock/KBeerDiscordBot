using KBeerDiscordBot.Untappd;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KBeerDiscordBot.IntegrationTests.Untappd
{
    [TestClass]
    public class UntappdBeerServiceIntegrationTests
    {
        [TestMethod]
        public async Task GivenASearchString_WhenCallingSearchSingleBeerAsync_ResultIsReturned()
        {
            var beerService = new UntappdBeerService(
                new UntappdApiCredentials
                {
                    ClientId = TestConfig.UntappdClientId,
                    ClientSecret = TestConfig.UntappdClientSecret
                });

            var beer = await beerService.SearchSingleBeerAsync("Old Rasputin");

            Assert.IsNotNull(beer);
            Assert.AreNotEqual(0.0f, beer.ABV); // This may be inaccurate depending on the search. How to better assert?
            Assert.AreNotEqual("", beer.Description);
            Assert.AreNotEqual(0, beer.IBU);
            Assert.AreNotEqual("", beer.ImageLink);
            Assert.AreNotEqual("", beer.Name);
            Assert.AreNotEqual("", beer.Style);

            Assert.IsNotNull(beer.Brewery);
            Assert.AreNotEqual("", beer.Brewery.City);
            Assert.AreNotEqual("", beer.Brewery.Country);
            Assert.AreNotEqual("", beer.Brewery.Name);
            Assert.AreNotEqual("", beer.Brewery.State);
            Assert.AreNotEqual("", beer.Brewery.Website);
        }

        [TestMethod]
        public async Task GivenAnEmptySearchString_WhenCallingSearchSingleBeerAsync_NoResultIsReturned()
        {
            var beerService = new UntappdBeerService(
                new UntappdApiCredentials
                {
                    ClientId = TestConfig.UntappdClientId,
                    ClientSecret = TestConfig.UntappdClientSecret
                });

            var beer = await beerService.SearchSingleBeerAsync("");

            Assert.IsNull(beer);
        }
    }
}
