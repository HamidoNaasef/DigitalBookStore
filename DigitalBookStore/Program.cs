using DigitalBookStore.Models;
using DigitalBookStore.Models.Repositroies;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddMvc();
builder.Services.AddScoped<IRepository<Book>, BookDBRepository>();
builder.Services.AddScoped<IRepository<Author>, AuthorDBRepository>();
//what is the wrong here??
builder.Services.AddDbContext<BookStoredDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon"));
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//RunMigration(app);


app.Run();

void RunMigration(WebApplication webHost)
{
    using (var scope = webHost.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<BookStoredDbContext>();
        db.Database.Migrate();
    } ;
}



