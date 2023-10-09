using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// configuraci�n de la autenticaci�n de usuarios
// cookies son archivos 
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie((options) =>
{
    options.LoginPath = new PathString("/Usuario/Login");
    options.ExpireTimeSpan = TimeSpan.FromHours(8); // ExpireTimeSpan: cuanto va a durar la sesi�n del usuario en la aplicaci�n
    options.SlidingExpiration = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//Habilitarla autenticaci�n en la aplicaci�n web
app.UseAuthorization(); // es obligatorio para poder ponerlo en nuestro proyecto

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
