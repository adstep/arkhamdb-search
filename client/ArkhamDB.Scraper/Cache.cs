namespace ArkhamDB.Scraper
{
    internal static class Cache
    {
        public static bool ExistsHotListPage(int page)
        {
            string path = GetPathForHotListPage(page);
            return File.Exists(path);
        }

        public static void SaveInvestigatorsPage(string content)
        {
            File.WriteAllText(GetPathForInvestigatorsPage(), content);
        }

        public static string LoadInvestigatorsPage()
        {
            string path = GetPathForInvestigatorsPage();

            return File.ReadAllText(path);
        }

        public static void SaveHotListPage(int page, string content)
        {
            File.WriteAllText(GetPathForHotListPage(page), content);
        }

        public static string LoadHotListPage(int page)
        {
            string path = GetPathForHotListPage(page);

            if (!ExistsHotListPage(page))
                throw new FileNotFoundException(path);

            return File.ReadAllText(path);
        }

        private static string GetPathForHotListPage(int page)
        {
            string path = Path.Join("cache", "hot-list-pages");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return Path.Join(path, $"{page}.html");
        }

        private static string GetPathForInvestigatorsPage()
        {
            string path = Path.Join("cache", "investigator-pages");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return Path.Join(path, $"0.html");
        }
    }
}
