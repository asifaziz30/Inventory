using Inventory.Service.Contracts.Data;
using Inventory.Service.Data.DataManager;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddNewtonsoftJson(options => { options.SerializerSettings.ContractResolver = new DefaultContractResolver(); });


builder.Services.AddKendo();
builder.Services.AddAutoMapper(typeof(Inventory.Service.Infrastructure.Config.MappingProfileConfiguration));
builder.Services.AddTransient<IItemService, ItemService>();
builder.Services.AddTransient<IItemManager, ItemManager>();
builder.Services.AddTransient<IPurchaseOrderService, PurchaseOrderService>();
builder.Services.AddTransient<IPurchaseOrderManager, PurchaseOrderManager>();
builder.Services.AddTransient<ISaleOrderService, SaleOrderService>();
builder.Services.AddTransient<ISaleOrderManager, SaleOrderManager>();

builder.Services.AddLocalization();
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
    pattern: "{controller=Item}/{action=Index}/{id?}");

app.Run();
