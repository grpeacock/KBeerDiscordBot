using KBeerDiscordBot.Abstractions;
using KBeerDiscordBot.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KBeerDiscordBot.Untappd
{
    public class UntappdBeerService : IBeerService
    {

        private readonly HttpClient _httpClient;

        private string _clientId;
        private string _clientSecret;

        public UntappdBeerService(UntappdApiCredentials untappdApiCredentials)
        {
            this._clientId = untappdApiCredentials.ClientId;
            this._clientSecret = untappdApiCredentials.ClientSecret;
            _httpClient = new HttpClient();
        }

        public async Task<Beer> SearchSingleBeerAsync(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return null;
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("https://api.untappd.com/v4/search/beer?");
            sb.Append("client_id=" + _clientId);
            sb.Append("&client_secret=" + _clientSecret);
            sb.Append("&q=" + search);

            string response = await _httpClient.GetStringAsync(System.Uri.EscapeUriString(sb.ToString()));

            dynamic res = JsonConvert.DeserializeObject(response);
            if (res.response.beers.count > 0)
            {
                dynamic beerInfo = res.response.beers.items[0];
                Beer beer = new Beer();
                beer.Name = beerInfo.beer.beer_name;
                //beer.BeerUntappdID = beerInfo.beer.bid;
                beer.ImageLink = beerInfo.beer.beer_label;
                beer.ABV = beerInfo.beer.beer_abv;
                beer.IBU = beerInfo.beer.beer_ibu;
                beer.Description = beerInfo.beer.beer_description;
                beer.Style = beerInfo.beer.beer_style;

                beer.Brewery = new Brewery
                {
                    Name = beerInfo.brewery.brewery_name,
                    City = beerInfo.brewery.location.brewery_city,
                    State = beerInfo.brewery.location.brewery_state,
                    Country = beerInfo.brewery.country_name,
                    Website = beerInfo.brewery.contact.url
                    //UntappdID = beerInfo.brewery.brewery_id;
                };

                return beer;
            }
            else
            {
                return null;
            }
        }
    }
}
