namespace ArkhamDB.Scraper
{
    using HtmlAgilityPack;

    internal class CardPage
    {
        public Card Card { get; }

        public CardPage(Card card)
        {
            Card = card;
        }

        public static CardPage Parse(string content)
        {
            HtmlDocument html = new HtmlDocument();
            html.LoadHtml(content);

            HtmlNode panelTitle = html.DocumentNode.SelectSingleNode("//*[@id=\"list\"]/h3[@class=\"panel-title\"]");
            string? name = panelTitle.SelectSingleNode(".//*[class=\"card-name\"]")?.InnerText;
            string? subName = panelTitle.SelectSingleNode(".//*[class=\"card-name\"]")?.InnerText;


            HtmlNode panelBody = html.DocumentNode.SelectSingleNode("//*[@id=\"list\"]/h3[@class=\"panel-body\"]");
            string? faction = panelBody.SelectSingleNode(".//*[class=\"card-faction\"")?.InnerText;
            string? type = panelBody.SelectSingleNode(".//*[class=\"card-type\"")?.InnerText;

            return new CardPage(new Card(name, subName, faction, type));
        }
    }
}
