- In program.cs
* Add this code before (var app = builder.Build();) 
	\* 
	builder.Services.AddLocalization();

builder.Services.AddSingleton<IStringLocalizerFactory, jsonStringLocalizerFactory>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddMvc()
    .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix) \\this part is not required in api
    .AddDataAnnotationsLocalization(option =>
    {
        option.DataAnnotationLocalizerProvider = (type, factory) => factory.Create(typeof(jsonStringLocalizerFactory));
    });

var supportedLanguage = new[] { "en-US", "ar-EG", "de-DE" }; //this array of language

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = supportedLanguage.Select(c => new CultureInfo(c)).ToList();

    options.DefaultRequestCulture = new RequestCulture(culture: supportedCultures[0], uiCulture: supportedCultures[0]);
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});
*/

* add middelware after (app.UseRouting();)
/*
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedLanguage[0])
    .AddSupportedCultures(supportedLanguage)
    .AddSupportedUICultures(supportedLanguage); //this part is not required in api

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