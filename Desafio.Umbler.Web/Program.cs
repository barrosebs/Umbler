using Desafio.Umbler.Web.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

//builder.Services.AddScoped<HttpClient>(s =>
//{
//    return new HttpClient { BaseAddress = new Uri(@"http://localhost:65453/") };
//});
builder.Services.AddHttpClient<IDomainService, DomainService>(client =>
{
    client.BaseAddress = new Uri(@"http://localhost:65453/");
    client.DefaultRequestHeaders.Add("Accept", "application/+json");
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
