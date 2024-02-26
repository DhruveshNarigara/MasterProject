using MasterProject.Repositories;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<IDepartmentRepositories , DepartmentRepositories>();
builder.Services.AddScoped<IEmployeeRepositories , EmployeeRepositories>();

builder.Services.AddScoped<IUserRepositories,UserRepositories>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set a timeout for the session
    options.Cookie.HttpOnly = true; // Make the session cookie inaccessible to client-side scripts
    options.Cookie.IsEssential = true; // Make the session cookie essential
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
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
