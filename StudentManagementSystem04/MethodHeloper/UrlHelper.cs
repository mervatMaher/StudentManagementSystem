namespace studentmanagementsystem04.methodheloper
{
    public class UrlHelper
    {
        public static string GetAbsoluteUrl(HttpRequest request, string relativeUrl)
        {
            var absoluteUri = string.Concat(
                request.Scheme,
                "://",
                request.Host.ToUriComponent(),
                request.PathBase.ToUriComponent(),
                relativeUrl
            );
            return absoluteUri;
        }
    }
}
