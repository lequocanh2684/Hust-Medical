using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using MudBlazor.Services;
using Patient_Health_Management_System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddServerSideBlazor();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Patient_Health_Management_System", Version = "v1" });
});
builder.Services.AddMudServices();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddSingleton<MongoDbSetup>();
builder.Services.AddScoped<IMedicineRepo, MedicineRepo>();
builder.Services.AddScoped<IMedicineService, MedicineService>();
builder.Services.AddScoped<IDiseaseRepo, DiseaseRepo>();
builder.Services.AddScoped<IPrescriptionRepo, PrescriptionRepo>();
builder.Services.AddScoped<IMedicineExaminationRepo, MedicalExaminationRepo>();
builder.Services.AddScoped<IPatientRepo, PatientRepo>();
//builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IDiseaseService, DiseaseService>();


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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.Use((context, next) =>
{
    context.Request.Scheme = "https";
    return next(context);
});
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Patient_Health_Management_System v1"));

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

