

using System.Net;

using MiddlewareExample.Web.Middlewares;

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

#region Map ve Run Kullan�m�
//app.Use(async (context, next) => //1.middleware
//{
//    await context.Response.WriteAsync("Before 1. Middleware\n");

//    await next();

//    await context.Response.WriteAsync("Before 1. Middleware\n");

//});

//app.Use(async (context, next) =>  //2.middleware
//{
//    await context.Response.WriteAsync("Before 2. Middleware\n");

//    await next();

//    await context.Response.WriteAsync("Before 2. Middleware\n");

//});

//app.Run(async context =>  //sonlandiri middleware ile request a�ag�daki middlewarelara u�ramayacak
//{
//    await context.Response.WriteAsync("Terminal 3. Middleware\n");
//}); 
#endregion

//orne�in "/ornek" dedi�imizde benim middleware�m cal��s�n istiyorsak

#region Map Metodu Kullan�m�
//app.Map("/ornek", app =>

//{
//    //app.Run(async context =>
//    //{
//    //    await context.Response.WriteAsync("Ornek Url'i icin middleware");
//    //});
//}); 
#endregion

//requestin querystringinde bir de�er ge�ti�inde �al��am middleware

#region MapWhen Kullan�m�
//app.MapWhen(context => context.Request.Query.ContainsKey("name"), app =>
//{
//    app.Use(async (context, next) =>
//    {
//        await context.Response.WriteAsync("Before 1. Middleware\n");

//        await next();

//        await context.Response.WriteAsync("After 1. Middleware\n");

//    });

//    app.Run(async context =>
//    {
//    await context.Response.WriteAsync("terminal 3. Middleware\n");
//    });
//}); 
#endregion



app.UseMiddleware<WhiteIpAddressControlMiddleware>();




app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
