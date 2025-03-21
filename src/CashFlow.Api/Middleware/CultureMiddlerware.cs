using System.Globalization;

namespace CashFlow.Api.Middleware
{
    public class CultureMiddlerware
    {
        private readonly RequestDelegate _next;
        private const string CULTURE_INFO_DEFAULT = "en";

        public CultureMiddlerware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke (HttpContext context)
        {
            var suportedLanguages = CultureInfo.GetCultures(CultureTypes.AllCultures).ToList();

            var requestedCulture = context.Request.Headers.AcceptLanguage.FirstOrDefault();

            var culture = new CultureInfo(CULTURE_INFO_DEFAULT);

            if(string.IsNullOrWhiteSpace(requestedCulture)
                && suportedLanguages.Exists(language => language.Name.Equals(requestedCulture))
            )
                culture = new CultureInfo(requestedCulture);

            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;

            await _next(context);
        }

    }
}
