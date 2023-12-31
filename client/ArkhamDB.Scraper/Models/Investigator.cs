namespace ArkhamDB.Scraper.Models
{
    using System.Text.Json.Serialization;

    internal class Investigator
    {
        public string Id { get; }

        public string Name { get; }

        public string SubName { get; }

        public Faction Faction { get; }

        public string Set { get;  }

        public Investigator(string id, string name, string subName, Faction faction, string set)
        {
            Id = id;
            Name = name;
            SubName = subName;
            Faction = faction;
            Set = set;
        }
    }
}
