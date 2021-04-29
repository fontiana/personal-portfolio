namespace PersonalPortfolio.Helper
{
    public static class HelperExtensions
    {
        public static string RemoveDash(this string dashedId)
        {
            return dashedId.Replace("-", " ");
        }
    }
}
