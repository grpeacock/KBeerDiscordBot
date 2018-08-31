using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace KBeerDiscordBot {
    
    public class UntappdAPI {

        private static readonly HttpClient client = new HttpClient();

        private string ID;
        private string secret;

        public UntappdAPI(string clientID, string clientSecret) {
            this.ID = clientID;
            this.secret = clientSecret;
        }

        public async Task<SimpleBeer> SearchBeer(string search) {
            StringBuilder sb = new StringBuilder();
            sb.Append("https://api.untappd.com/v4/search/beer?");
            sb.Append("client_id=" + Program.jConfig.ClientID);
            sb.Append("&client_secret=" + Program.jConfig.ClientSecret);
            sb.Append("&q=" + search);

            string response = await client.GetStringAsync(System.Uri.EscapeUriString(sb.ToString()));

            dynamic res = JsonConvert.DeserializeObject(response);
            if (res.response.beers.count > 0) {
                dynamic beerInfo = res.response.beers.items[0];
                SimpleBeer beer = new SimpleBeer();
                beer.Name = beerInfo.beer.beer_name;
                beer.BeerUntappdID = beerInfo.beer.bid;
                beer.ImageLink = beerInfo.beer.beer_label;
                beer.ABV = beerInfo.beer.beer_abv;
                beer.IBU = beerInfo.beer.beer_ibu;
                beer.Description = beerInfo.beer.beer_description;
                beer.Style = beerInfo.beer.beer_style;
                beer.Brewery = beerInfo.brewery.brewery_name;
                beer.BreweryUntappdID = beerInfo.brewery.brewery_id;
                beer.BreweryCity = beerInfo.brewery.location.brewery_city;
                beer.BreweryState = beerInfo.brewery.location.brewery_state;
                beer.BreweryCountry = beerInfo.brewery.country_name;
                beer.BreweryWebsite = beerInfo.brewery.contact.url;

                return beer;
            } else {
                return null;
            }
            

        }

    }

    public class SimpleBeer {
        public string Name { get; set; }
        public int BeerUntappdID { get; set; }
        public string ImageLink { get; set; }
        public float ABV { get; set; }
        public int IBU { get; set; }
        public string Description { get; set; }
        public string Style { get; set; }

        public string Brewery { get; set; }
        public int BreweryUntappdID { get; set; }
        public string BreweryCity { get; set; }
        public string BreweryState { get; set; }
        public string BreweryCountry { get; set; }
        public string BreweryWebsite { get; set; }

    }
}
