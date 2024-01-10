using EmployeeWeb.Services;
using EmployeeWeb.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//  Burasý için yazýlan service moduluru register etmek gerekiyor
builder.Services.AddHttpClient<IEmployeeService, EmployeeService>(s => s.BaseAddress = new Uri("https://localhost:7087")); // ???? Multiple constructor verdi...dikkat yani IEmployeeService interfacini yanlýþlýkla tekrar kendi üzerine yerleþtirmeye çalýþmýsýz.

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
