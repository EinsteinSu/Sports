namespace Sports.Common
{
    public static class TypeConvert
    {
        public static int ToInt(this string str)
        {
            int.TryParse(str, out int i);
            return i;
        }
    }
}