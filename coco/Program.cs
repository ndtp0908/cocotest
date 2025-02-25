var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
    
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
    );

    endpoints.MapControllerRoute(
        name: "about",
        pattern: "About",
        defaults: new { controller = "Home", action = "About" }
    );

    endpoints.MapControllerRoute(
        name: "product",
        pattern: "Product",
        defaults: new { controller = "Home", action = "Product" }
    );

    endpoints.MapControllerRoute(
        name: "shopping",
        pattern: "Shopping",
        defaults: new { controller = "Home", action = "Shopping" }
    );

    endpoints.MapControllerRoute(
        name: "user",
        pattern: "User",
        defaults: new { controller = "Home", action = "User" }
    );
});

app.Run();
