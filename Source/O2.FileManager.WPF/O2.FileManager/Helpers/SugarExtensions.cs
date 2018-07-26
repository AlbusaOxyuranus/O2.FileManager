namespace O2.FileManager.Helpers
{
    public static class SugarExtensions

    {
        public static bool Is<T>(this object o)

        {
            return o is T;
        }


        public static T As<T>(this object o) where T : class

        {
            return o as T;
        }


        public static T Of<T>(this object o)

        {
            return (T) o;
        }
    }
}