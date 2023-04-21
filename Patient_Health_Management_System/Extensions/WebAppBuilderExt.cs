using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace Patient_Health_Management_System.Extensions
{
    public static class WebAppBuilderExt
    {
        public static void AddRepositories(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<MongoDbSetup>();
            builder.Services.AddScoped<IMedicineRepo, MedicineRepo>();
            builder.Services.AddScoped<IDiseaseRepo, DiseaseRepo>();
            builder.Services.AddScoped<IPrescriptionRepo, PrescriptionRepo>();
            builder.Services.AddScoped<IMedicalExaminationRepo, MedicalExaminationRepo>();
            builder.Services.AddScoped<IPatientRepo, PatientRepo>();
            builder.Services.AddScoped<IBillingRepo, BillingRepo>();
        }

        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IMedicineService, MedicineService>();
            builder.Services.AddScoped<IDiseaseService, DiseaseService>();
            builder.Services.AddScoped<IPrescriptionService, PrescriptionService>();
            builder.Services.AddScoped<IMedicalExaminationService, MedicalExaminationService>();
            builder.Services.AddScoped<IPatientService, PatientService>();
            builder.Services.AddScoped<IBillingService, BillingService>();
        }

        public static void AddAuthentication(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}
).AddCookie()
 .AddOpenIdConnect("Auth0", options =>
 {
     options.Authority = $"https://{builder.Configuration["Auth0:Domain"]}";

     options.ClientId = builder.Configuration["Auth0:ClientId"];
     options.ClientSecret = builder.Configuration["Auth0:ClientSecret"];

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
     };

     options.Events = new OpenIdConnectEvents
     {
         OnRedirectToIdentityProviderForSignOut = (context) =>
         {
             var logoutUri = $"https://{builder.Configuration["Auth0:Domain"]}/v2/logout?client_id={builder.Configuration["Auth0:ClientId"]}";

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
    }
}