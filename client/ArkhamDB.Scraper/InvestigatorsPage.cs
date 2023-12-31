namespace ArkhamDB.Scraper
{
    using ArkhamDB.Scraper.Models;
    using HtmlAgilityPack;
    using System.Text.RegularExpressions;
    using System.Web;

    internal class InvestigatorsPage
    {
        private static Regex IdRegex = new Regex(@"arkhamdb.com\/card\/(?<id>[0-9]+)*");

        public List<Investigator> Investigators { get; }

        public InvestigatorsPage(List<Investigator> investigators)
        {
            Investigators = investigators;
        }

        public static InvestigatorsPage Parse(string content)
        {
            HtmlDocument html = new HtmlDocument();
            html.LoadHtml(content);

            HtmlNode table = html.DocumentNode.SelectSingleNode("//*[@id=\"list\"]//table");

            HtmlNodeCollection rows = table.SelectNodes(".//tr[@class=\"even\" or @class=\"odd\"]");

            List<Investigator> investigators = new List<Investigator>();

            foreach (HtmlNode row in rows)
            {
                string innerhtml = row.InnerHtml;

                string id = IdRegex.Match(row.SelectSingleNode(".//td[@data-th=\"Name\"]/a")?.GetAttributeValue("href", null)).Groups["id"].Value;

                string full = HttpUtility.HtmlDecode(row.SelectSingleNode(".//td[@data-th=\"Name\"]/a")?.InnerText.Trim());
                string[] parts = full.Split(':');
                string name = parts[0].Trim().Replace("&quote;", "\"");
                string subName = parts[1].Trim();

                Faction? faction = Enum.Parse<Faction>(row.SelectSingleNode(".//td[@data-th=\"Faction\"]")?.InnerText);

                string set = row.SelectSingleNode(".//td[@data-th=\"Set\"]")?.InnerText;


                investigators.Add(
                    new Investigator(
                        id,
                        name,
                        subName,
                        faction ?? Faction.Neutral,
                        set
                    )
                );
            }

            return new InvestigatorsPage(investigators);
        }
    }
}
