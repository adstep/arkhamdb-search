namespace ArkhamDB.Scraper
{
    using HtmlAgilityPack;
    using System.Text.RegularExpressions;

    internal class HotListPage
    {
        private static Regex IdRegex = new Regex("decklist\\/view\\/(?<id>[0-9]+)\\/.*");
        private static Regex CardIdRegex = new Regex(@"/bundles/cards/(?<cardId>[0-9]+)\.png");

        public List<Deck> Decks { get; }

        public HotListPage(List<Deck> decks) 
        {
            Decks = decks;
        }

        public static HotListPage Parse(string content)
        {
            HtmlDocument html = new HtmlDocument();
            html.LoadHtml(content);

            HtmlNode table = html.DocumentNode.SelectSingleNode("//*[@id=\"wrapper\"]//table");

            HtmlNodeCollection rows = table.SelectNodes(".//tbody/tr");

            List<Deck> decks = new List<Deck>();

            foreach (HtmlNode row in rows) 
            {
                string innerhtml = row.InnerHtml;

                HtmlNode img = row.SelectSingleNode(".//td[1]/img");
                string image = img.GetAttributeValue("src", null);
                string cardId = CardIdRegex.Match(image).Groups["cardId"].Value;

                HtmlNode a = row.SelectSingleNode(".//td[2]/article/h4/a");
                string deckLink = a.GetAttributeValue("href", null);
                int id = int.Parse(IdRegex.Match(deckLink).Groups["id"].Value);
                string? name = a.InnerText;

                HtmlNode time = row.SelectSingleNode(".//td[2]/article/h5/time");
                DateTime createdAt = DateTime.Parse(time.GetAttributeValue("datetime", null));


                HtmlNode span0 = row.SelectSingleNode(".//*[@id=\"social-icon-like\"]/span[2]");
                int likes = int.Parse(span0.InnerText);

                HtmlNode span1 = row.SelectSingleNode(".//*[@id=\"social-icon-favorite\"]/span[2]");
                int favorite = int.Parse(span1.InnerText);

                HtmlNode span2 = row.SelectSingleNode(".//*[@id=\"social-icon-comment\"]/span[2]");
                int comments = int.Parse(span2.InnerText);

                HtmlNode span3 = row.SelectSingleNode(".//*[@class=\"social-icon-version\"]/span[2]");
                string version = span3.InnerText;

                decks.Add(
                    new Deck(
                        id,
                        name,
                        cardId,
                        createdAt,
                        likes,
                        favorite,
                        comments,
                        version
                    )
                );
            }

            return new HotListPage(decks);
        }
    }
}
