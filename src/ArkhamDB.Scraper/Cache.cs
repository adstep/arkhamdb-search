namespace ArkhamDB.Scraper
{
    internal static class Cache
    {
        public static bool Exists(int page)
        {
            string path = GetPath(page);
            return File.Exists(path);
        }

        public static void Save(int page, string content)
        {
            File.WriteAllText(GetPath(page), content);
        }

        public static string Load(int page)
        {
            string path = GetPath(page);

            if (!Exists(page))
                throw new FileNotFoundException(path);

            return File.ReadAllText(path);
        }

        private static string GetPath(int page)
        {
            if (!Directory.Exists("cache"))
                Directory.CreateDirectory("cache");

            return Path.Join("cache", $"{page}.txt");
        }
    }
}
