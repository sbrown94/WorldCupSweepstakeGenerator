using System.Text.Json.Serialization;

namespace WorldCupSweepstakes.Models.OddsApi
{
    public class OddsApiResponse
    {
        [JsonPropertyName("bookmakers")]
        public Bookmaker[] Bookmakers { get; set; }
    }

    public class Bookmaker
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("last_update")]
        public string LastUpdate { get; set; }

        [JsonPropertyName("markets")]
        public Market[] Markets { get; set; }
    }

    public class Market
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("outcomes")]
        public Outcome[] Outcomes { get; set; }
    }

    public class Outcome
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("price")]
        public string Price { get; set; }
    }
}
