﻿using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddServerSideBlazor();
builder.Services.AddRazorPages();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Patient_Health_Management_System", Version = "v1" });
});
builder.Services.AddMudServices();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddMvcCore().AddApiExplorer();
builder.Services.AddHttpClient();

// Add Repositories
builder.AddRepositories();
// Add Services
builder.AddServices();
// Add Authentication
builder.AddAuthentication();

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

