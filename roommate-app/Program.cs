using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using roommate_app.Controllers.Authentication;
using roommate_app.Data;
using roommate_app.Exceptions;
using roommate_app.Other.FileCreator;
using roommate_app.Services;
using roommate_app.Interceptors;
using roommate_app.Other.Logger;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IFileCreator, FileCreator>();
builder.Services.AddScoped<IListingCompreterFactory, ListingComparerFactory>();
builder.Services.AddScoped<IErrorLogging, ErrorLogging>();
builder.Services.AddScoped<IDurationLogger, DurationLogger>();

builder.Services.AddInterceptedService<IGenericService, GenericService, DurationInterceptor>();
builder.Services.AddInterceptedService<IListingService, ListingService, DurationInterceptor>();
builder.Services.AddInterceptedService<IUserService, UserService, DurationInterceptor>();

builder.Services.AddCors((options) =>
{
    options.AddPolicy("CORSPolicy",
        builder =>
        {
            builder
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithOrigins("http://localhost:3000");
        });
});

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultDatabase");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IListingService, ListingService>();
builder.Services.AddScoped<IGenericService, GenericService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();

    app.UseMiddleware<JwtMiddleware>();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors("CORSPolicy");

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();


app.Run();