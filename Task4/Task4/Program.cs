namespace Task4
{
    // Інтерфейс стратегії
    interface IImageLoadingStrategy
    {
        void LoadImage(string href);
    }

    // Конкретна стратегія для завантаження з мережі
    class NetworkImageLoadingStrategy : IImageLoadingStrategy
    {
        public void LoadImage(string href)
        {
            Console.WriteLine($"Loading image from network: {href}");
        }
    }

    // Конкретна стратегія для завантаження з файлової системи
    class FilesystemImageLoadingStrategy : IImageLoadingStrategy
    {
        public void LoadImage(string href)
        {
            Console.WriteLine($"Loading image from filesystem: {href}");
        }
    }

    // Контекст, який використовує стратегію
    class Image
    {
        private IImageLoadingStrategy loadingStrategy;

        public Image(IImageLoadingStrategy loadingStrategy)
        {
            this.loadingStrategy = loadingStrategy;
        }

        public void SetLoadingStrategy(IImageLoadingStrategy loadingStrategy)
        {
            this.loadingStrategy = loadingStrategy;
        }

        public void Load(string href)
        {
            loadingStrategy.LoadImage(href);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Default;

            // Створення зображення зі стратегією завантаження з мережі
            Image networkImage = new Image(new NetworkImageLoadingStrategy());
            networkImage.Load("https://images.rawpixel.com/image_png_800/czNmcy1wcml2YXRlL3Jhd3BpeGVsX2ltYWdlcy93ZWJzaXRlX2NvbnRlbnQvcHUyMzMxNjM2LWltYWdlLTAxLXJtNTAzXzMtbDBqOXFrNnEucG5n.png");

            // Зміна стратегії на завантаження з файлової системи
            networkImage.SetLoadingStrategy(new FilesystemImageLoadingStrategy());
            networkImage.Load(@"C:\Users\Maxim\Desktop\ЛАБЫ 2 СЕМЕСТР\2\КПЗ\Lab_4_KPZ_Maksym_Shyriaiev\Task4\pexels-chevon-rossouw-2558605.jpg");
        }
    }
}