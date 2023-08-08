using Append.Blazor.Printing;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using MudBlazor;
using MudBlazor.Services;

namespace Hust_Medical.Extensions
{
    public static class WebAppBuilderExt
    {
        public static void AddRepositories(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<RepoInitialize>();
            builder.Services.AddScoped<IMedicineRepo, MedicineRepo>();
            builder.Services.AddScoped<IMedicineGroupRepo, MedicineRepo>();
            builder.Services.AddScoped<IDiseaseRepo, DiseaseRepo>();
            builder.Services.AddScoped<IDiseaseGroupRepo, DiseaseRepo>();
            builder.Services.AddScoped<IPrescriptionRepo, PrescriptionRepo>();
            builder.Services.AddScoped<IMedicalExaminationRepo, MedicalExaminationRepo>();
            builder.Services.AddScoped<IPatientRepo, PatientRepo>();
            builder.Services.AddScoped<IBillingRepo, BillingRepo>();
            builder.Services.AddScoped<IUserRepo, UserRepo>();
        }

        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IMedicineService, MedicineService>();
            builder.Services.AddScoped<IDiseaseService, DiseaseService>();
            builder.Services.AddScoped<IPrescriptionService, PrescriptionService>();
            builder.Services.AddScoped<IMedicalExaminationService, MedicalExaminationService>();
            builder.Services.AddScoped<IPatientService, PatientService>();
            builder.Services.AddScoped<IBillingService, BillingService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddSingleton<IKeyVaultService, KeyVaultService>();
            builder.Services.AddScoped<IPrintingService, PrintingService>();
            //builder.Services.AddSingleton(typeof(ILogger), builder.Services.BuildServiceProvider().GetService<ILogger<Medicine>>());
        }

        public static void AddAuthentication(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}
).AddCookie(option =>
{
    option.LoginPath = "/auth/login";
})
 .AddOpenIdConnect("Auth0", options =>
 {
     options.Authority = $"https://{builder.Configuration["auth0Domain"]}";

     options.ClientId = builder.Configuration["auth0ClientId"];
     options.ClientSecret = builder.Configuration["auth0ClientSecret"];

     options.ResponseType = OpenIdConnectResponseType.Code;

     options.Scope.Clear();
     options.Scope.Add("openid");
     options.Scope.Add("profile"); // <- Optional extra
     options.Scope.Add("email");   // <- Optional extra

     options.CallbackPath = new PathString("/callback");
     options.ClaimsIssuer = "Auth0";
     options.SaveTokens = true;
     options.TokenValidationParameters = new TokenValidationParameters
     {
         NameClaimType = "name",
         RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
     };

     options.Events = new OpenIdConnectEvents
     {
         OnRedirectToIdentityProviderForSignOut = (context) =>
         {
             var logoutUri = $"https://{builder.Configuration["auth0Domain"]}/v2/logout?client_id={builder.Configuration["auth0ClientId"]}";

             var postLogoutUri = context.Properties.RedirectUri;
             if (!string.IsNullOrEmpty(postLogoutUri))
             {
                 if (postLogoutUri.StartsWith("/"))
                 {
                     var request = context.Request;
                     postLogoutUri = request.Scheme + "://" + request.Host + request.PathBase + postLogoutUri;
                 }
                 logoutUri += $"&returnTo={Uri.EscapeDataString(postLogoutUri)}";
             }

             context.Response.Redirect(logoutUri);
             context.HandleResponse();

             return Task.CompletedTask;
         }
     };
 });

        }

        public static void AddMudSnackBarConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddMudServices(config =>
            {
                config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopCenter;
                config.SnackbarConfiguration.PreventDuplicates = false;
                config.SnackbarConfiguration.NewestOnTop = false;
                config.SnackbarConfiguration.ShowCloseIcon = true;
                config.SnackbarConfiguration.VisibleStateDuration = 100;
                config.SnackbarConfiguration.HideTransitionDuration = 50;
                config.SnackbarConfiguration.ShowTransitionDuration = 50;
                config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
            });
        }
    }
}