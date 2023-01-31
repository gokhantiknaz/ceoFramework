using AltYapi.API.Filters;
using AltYapi.API.Middlewares;
using AltYapi.API.Modules;
using AltYapi.Core.MongoDbSettings;
using AltYapi.Repository;
using AltYapi.RepositoryMongo.Persistence;
using AltYapi.Service.Mapping;
using AltYapi.Service.Validations;
using AltYapi.ServiceMongo.Mapping;
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
MongoDbPersistence.Configure();

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
builder.Services.AddAutoMapper(typeof(MapProfileMongo));

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), options =>
    {
        options.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
    });


});


//appsettings'i okuyarak "DatabaseSettings" classýndaki propertyleri set ediyoruz.
//Öncelikle appsettings deki datalarýmýzý "DatabaseSettings" e baðlamak için. Bunu yazdýktan sonra herhangi bir classýn constructorýnda IOptions<DatabaseSettings> options diyerek bu deðerleri okuyabiliriz. Biz bunun yerine direkt olarak bir interface üzerinden almak için iki aþaðýdaki kodu yazdýk.
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));
//==> Interface üzerinden almak için aþaðýdaki kodu yazdýk. Aþaðýdaki kodda IOptions ile DatabaseSettingsi tanýmladýk. Herhangi bir classýn contructorýnda IDatabaseSettings'i çaðýrdýðýmda bana DatabaseSettings appsettingsdeki ayarlar ile doldurulmuþ þekilde gelecektir.
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

//Baþlarda olmasý daha iyi olur.
app.UseCustomException();

app.UseAuthorization();

app.MapControllers();

app.Run();
