namespace ArkhamDB.Scraper
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            string content = Cache.Load(0);

            IEnumerable<HotListPage> pages = Enumerable.Range(0, 43).Select(i => HotListPage.Parse(Cache.Load(i)));

            var temp = pages.ToList();

            IEnumerable<string> images = pages.SelectMany(p => p.Decks)
                .Select(d => d.CardId)
                .Distinct();

            foreach (string image in images)
            {
                Console.WriteLine(image);
            }
            Console.WriteLine("DONE");


            Console.ReadKey();
        }

        public static async Task LoadCard()
        {
            using HttpClient client = new HttpClient();
            // 01002
            HttpResponseMessage response = await client.GetAsync($"https://arkhamdb.com/card/01002");

            client.GetAsync()
        }

        public static async Task GenerateCache()
        {
            const int pages = 43;

            using HttpClient client = new HttpClient();

            for (int i = 0; i < pages; i++)
            {
                HttpResponseMessage response = await client.GetAsync($"https://arkhamdb.com/decklists/halloffame/{i}");

                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();
                Cache.Save(i, content);

                Console.WriteLine($"Cached page {i}");

                await Task.Delay(5000);
            }
        }
    }
}
