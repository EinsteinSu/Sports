namespace Sports.Common
{
    public class FontSettings
    {
        public string FontName { get; set; }

        public int FontSize { get; set; }

        public string Color { get; set; }
    }

    public class DisplayData<T>
    {
        public T Data { get; set; }

        public FontSettings FontSettings { get; set; }
    }
}