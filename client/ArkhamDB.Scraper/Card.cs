namespace ArkhamDB.Scraper
{
    internal class Card
    {
        public string Name { get; }
        public string SubName { get; }
        public string Faction { get; }
        public string Type { get; }
    
        public Card(string name, string subName, string faction, string type)
        {
            Name = name;
            SubName = subName;
            Faction = faction;
            Type = type;
        }
    }
}
