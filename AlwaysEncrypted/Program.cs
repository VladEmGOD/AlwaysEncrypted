using AlwaysEncrypted.DataAccess;
using AlwaysEncrypted.DataAccess.Connection;
using AlwaysEncrypted.DataAccess.Providers;
using Microsoft.Data.SqlClient;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

var customProvider = new SettingsSqlColumnEncryptionKeyStoreProvider();
var providers = new Dictionary<string, SqlColumnEncryptionKeyStoreProvider>
    {
        { "APP_SETTINGS_KEY_VAULT", customProvider},
    };

SqlConnection.RegisterColumnEncryptionKeyStoreProviders(providers);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IDbConnection>(_ =>
{
    SqlConnectionStringBuilder strbldr = new SqlConnectionStringBuilder();
    strbldr.DataSource = "DESKTOP-5EAGGSU";
    strbldr.InitialCatalog = "AlwaysEncrypted";
    strbldr.IntegratedSecurity = true;
    strbldr.ColumnEncryptionSetting = SqlConnectionColumnEncryptionSetting.Enabled;
    var conn = new SqlConnection(strbldr.ConnectionString);

    return conn;
});

builder.Services.AddTransient<IUserProvider, UserProvider>();

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

app.Run();
