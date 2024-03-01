using HalloDocApp.Entities.DataContext;
using HalloDocApp.Repositories.Repository.Implementation;
using HalloDocApp.Repositories.Repository.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDBContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IPatientLoginRepository,PatientLoginRepository>();
builder.Services.AddScoped<IPatientRepository,PatientRepository>();


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
    pattern: "{controller=Home}/{action=PatientLogin}/{id?}");

app.Run();
