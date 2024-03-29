using CollectionManager.Authorization;
using CollectionManager.Data;
using CollectionManager.Data.Repositories;
using CollectionManager.Data.Interfaces;
using CollectionManager.Services.Interfaces;
using CollectionManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using CollectionManager.SignalR;
using AspNetCoreWebApp.CloudStorage;
using Microsoft.AspNetCore.ResponseCompression;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();
builder.Services.AddLocalization(options =>
{
    options.ResourcesPath = "Resources";
});
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("en-US"),
        new CultureInfo("pl-PL"),
    };

    options.SupportedUICultures = supportedCultures;
    options.SupportedCultures = supportedCultures;
    options.DefaultRequestCulture = new RequestCulture("en-US");
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
    {
        policy.AddRequirements(
            new UserUnlockedRequirement(),
            new AdminUserRequirement());
    });
    options.AddPolicy("UnlockedPolicy", policy =>
    {
        policy.AddRequirements(new UserUnlockedRequirement());
    });
});

builder.Services.AddTransient<IAuthorizationHandler, AdminUserRequirementHandler>();
builder.Services.AddTransient<IAuthorizationHandler, UserUnlockedRequirementHandler>();
builder.Services.AddScoped<IsOwnerAuthorizer>();

builder.Services.Configure<SecurityStampValidatorOptions>(options =>
{
    options.ValidationInterval = TimeSpan.Zero;
});

builder.Services.AddAuthentication().AddGoogle(googleOptions =>
{
    googleOptions.ClientId = builder.Configuration.GetSection("ClientId").Value;
    googleOptions.ClientSecret = builder.Configuration.GetSection("ClientSecret").Value;
});

builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);

builder.Services.AddScoped<ICollectionsRepository, CollectionsRepository>();
builder.Services.AddScoped<ICollectionService, CollectionService>();

builder.Services.AddScoped<ICollectionItemsRepository, CollectionItemsRepository>();
builder.Services.AddScoped<ICollectionItemService, CollectionItemService>();

builder.Services.AddScoped<ISearchService, SearchService>();

builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<ITagService, TagService>();

builder.Services.AddScoped<ICommentsRepository, CommentsRepository>();
builder.Services.AddScoped<ILikesRepository, LikesRepository>();
builder.Services.AddScoped<ISocialsService, SocialsService>();

builder.Services.AddSingleton<ICloudStorage, GoogleCloudRepository>();
builder.Services.AddSingleton<ICloudStorageService, CloudStorageService>();

builder.Services.AddServerSideBlazor();
builder.Services.AddSignalR();

builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream" });
});

builder.Services.AddSession();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.UseDeveloperExceptionPage();
app.MapBlazorHub();
app.MapHub<SocialsHub>("/socialHub");
app.UseSession();

app.Run();
