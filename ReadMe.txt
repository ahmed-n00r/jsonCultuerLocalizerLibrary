- In program.cs
* Add this code before (var app = builder.Build();) 
	\* 
	builder.Services.AddLocalization();

builder.Services.AddSingleton<IStringLocalizerFactory, jsonStringLocalizerFactory>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddMvc()
    .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization(option =>
    {
        option.DataAnnotationLocalizerProvider = (type, factory) => factory.Create(typeof(jsonStringLocalizerFactory));
    });

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("en-US"),
        new CultureInfo("ar-EG"),
        new CultureInfo("de-DE")
    }; //this array of language

    //options.DefaultRequestCulture = new RequestCulture(culture: supportedCultures[0], uiCulture: supportedCultures[0]);
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
}); */

* add middelware after (app.UseRouting();)
/*
var supportedCultures = new[] { "en-US", "ar-EG", "de-DE" }; //this array of language
var localizationOptions = new RequestLocalizationOptions()
    //.SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);
*/

- In _ViewImports.cshtml
* add the folling using and injection
/*
@using Microsoft.AspNetCore.Mvc.Localization
@using Microsoft.AspNetCore.Builder;
@using Microsoft.AspNetCore.Localization;
@using Microsoft.AspNetCore.Http.Features;
@using Microsoft.Extensions.Options;

@inject IOptions<RequestLocalizationOptions> options
@inject IViewLocalizer localizer
*/

- In ViewResource you well find partial view for all language that you use

- For create cookies you can use this code 
* you can use this post mithod in Home controller
/*
[HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );

            return LocalRedirect(returnUrl);
        }
*/