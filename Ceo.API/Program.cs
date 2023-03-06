using Ceo.API.Filters;
using Ceo.API.Middlewares;
using Ceo.API.Modules;
using Ceo.Core.MongoDbSettings;
using Ceo.Repository;
using Ceo.Service.Mapping;
using Ceo.Service.Validations;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NLog.Web;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers(options => options.Filters.Add(new ValidateFilterAttribute())).AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<ProductDtoValidator>());

builder.Services.AddControllers(options => options.Filters.Add(new ValidateFilterAttribute())).AddNewtonsoftJson();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<ProductDtoValidator>();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;


});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();
builder.Services.AddScoped(typeof(NotFoundFilter<>));

//RepoServiceModule de AutoFac ile eklendi.
//builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
//builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));
//builder.Services.AddScoped<IProductRepository, ProductRepository>();
//builder.Services.AddScoped<IProductService, ProductService>();
//builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
//builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddAutoMapper(typeof(MapProfile));


builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), options =>
    {
        options.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
    });


});


//appsettings'i okuyarak "DatabaseSettings" class�ndaki propertyleri set ediyoruz.
//�ncelikle appsettings deki datalar�m�z� "DatabaseSettings" e ba�lamak i�in. Bunu yazd�ktan sonra herhangi bir class�n constructor�nda IOptions<DatabaseSettings> options diyerek bu de�erleri okuyabiliriz. Biz bunun yerine direkt olarak bir interface �zerinden almak i�in iki a�a��daki kodu yazd�k.
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));
//==> Interface �zerinden almak i�in a�a��daki kodu yazd�k. A�a��daki kodda IOptions ile DatabaseSettingsi tan�mlad�k. Herhangi bir class�n contructor�nda IDatabaseSettings'i �a��rd���mda bana DatabaseSettings appsettingsdeki ayarlar ile doldurulmu� �ekilde gelecektir.
builder.Services.AddSingleton<IDatabaseSettings>(sp =>
{
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
});


//builder.Services.AddScoped(typeof(IGenericRepositoryMongo<>), typeof(GenericRepositoryMongo<>));

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new RepoServiceModule()));

builder.Logging.ClearProviders();
builder.Host.UseNLog();

var app = builder.Build();


//app.Logger.LogInformation("Adding Routes");
//app.Logger.LogInformation("Starting the app");
//app.Logger.IsEnabled(LogLevel.Debug);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

//Ba�larda olmas� daha iyi olur.
app.UseCustomException();

app.UseAuthorization();

app.MapControllers();

app.Run();
