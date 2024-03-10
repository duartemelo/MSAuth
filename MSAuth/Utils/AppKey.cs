namespace MSAuth.API.Utils
{
    public class AppKey
    {
        public static string GetAppKey(HttpContext context)
        {
            return context.Items["AppKey"]!.ToString()!;
        }
    }
}
