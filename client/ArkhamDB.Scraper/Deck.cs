namespace ArkhamDB.Scraper
{
    internal class Deck
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string InvestigatorId { get; set; }
        public DateTime Published { get; set; }
        public int Favorites { get; set; }
        public int Likes { get; set; }
        public int Comments { get; set; }
        public string Version { get; set; }

        public Deck(int id, string name, string investigatorId, DateTime published, int likes, int favorite, int comments, string version)
        {
            Id = id;
            Name = name;
            InvestigatorId = investigatorId;
            Published = published;
            Favorites = favorite;
            Likes = likes;
            Comments = comments;
            Version = version;
        }
    }
}
