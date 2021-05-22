namespace KBeerDiscordBot.Models
{
    public class Beer
    {
        public string Name { get; set; }
        public string ImageLink { get; set; }
        public float ABV { get; set; }
        public int IBU { get; set; }
        public string Description { get; set; }
        public string Style { get; set; }

        public Brewery Brewery { get; set; }
    }
}
