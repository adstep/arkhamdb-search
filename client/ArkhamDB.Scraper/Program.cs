namespace ArkhamDB.Scraper
{
    using ArkhamDB.Scraper.Models;
    using System.Text.Json;

    internal class Program
    {
        static async Task Main(string[] args)
        {
            //await GenerateHotListPageCache();

            //await GenerateInvestigatorCache();

            //ProcessInvestigators();

            // ProcessHotListPages();
        }

        public static void ProcessHotListPages()
        {
            IEnumerable<HotListPage> pages = Enumerable.Range(0, 43).Select(i => HotListPage.Parse(Cache.LoadHotListPage(i)));

            IEnumerable<IGrouping<string, Deck>> deckMap = pages.SelectMany(p => p.Decks).GroupBy(p => p.InvestigatorId);

            foreach (IGrouping<string, Deck> group in deckMap)
            {
                string investigatorId = group.Key;
                List<Deck> decks = group.ToList();

                string json = JsonSerializer.Serialize(decks, Serialization.Options);
                File.WriteAllText($"{investigatorId}.json", json);
            }
        }

        public static void ProcessInvestigators()
        {
            string content = Cache.LoadInvestigatorsPage();

            InvestigatorsPage page = InvestigatorsPage.Parse(content);

            string json = JsonSerializer.Serialize(page.Investigators, Serialization.Options);
            Console.WriteLine(json);
        }

        public static async Task GenerateInvestigatorCache()
        {
            Console.WriteLine("Generating cache for Investigators page...");

            using HttpClient client = new HttpClient();
            // 01002
            HttpResponseMessage response = await client.GetAsync($"https://arkhamdb.com/find?q=t:investigator&decks=player");

            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();

            Cache.SaveInvestigatorsPage(content);
        }

        public static async Task GenerateHotListPageCache()
        {
            Console.WriteLine("Generating cache for HotList pages...");

            const int pages = 43;

            using HttpClient client = new HttpClient();

            for (int i = 0; i < pages; i++)
            {
                Console.WriteLine($"Caching page [{i + 1}/{pages}]");

                HttpResponseMessage response = await client.GetAsync($"https://arkhamdb.com/decklists/halloffame/{i}");

                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();
                Cache.SaveHotListPage(i, content);

                Console.WriteLine($"Cached page {i}");

                await Task.Delay(5000);
            }
        }
    }
}
