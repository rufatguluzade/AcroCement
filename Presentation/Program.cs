using DataAccess.Context;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
using DataAccess.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Presentation.Middlewares;
using Business.MappingProfiles;
using Business.Services.Abstraction;
using Business.Services.Concered;
using Business.Extensions;

var builder = WebApplication.CreateBuilder(args);






/*Logger log = new LoggerConfiguration()
      .WriteTo.Seq("http://localhost:5341")
      .WriteTo.File("C:\\Users\\ROG\\Desktop\\Loge\\ApiLog-.log", rollingInterval: RollingInterval.Day)
      .Enrich.FromLogContext()
      .MinimumLevel.Information()
      .CreateLogger();

builder.Host.UseSerilog(log);*/


// Add services to the container.





builder.Services.AddControllers();




// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Summarylerin dusmesi ucun swaggere
builder.Services.AddSwaggerGen(options =>
{
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Presentation.xml"));
});

builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("Default"), x => x.MigrationsAssembly("DataAccess")));







// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.Configure<RouteOptions>(x =>
{
    x.LowercaseUrls = true;
});

builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("Default"), x => x.MigrationsAssembly("DataAccess")));



builder.Services.AddCors(p => p.AddDefaultPolicy(builder =>
{
    builder.WithOrigins("https://localhost:44395/");
    builder.AllowAnyMethod();
    builder.AllowAnyHeader();

}));








#region Repositories
builder.Services.AddScoped<IAboutUsRepository, AboutUsRepository>();
builder.Services.AddScoped<IBlogRepository, BlogRepository>();
builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<ICustomerReactionRepository, CustomerReactionRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ISliderRepository, SliderRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<IWhyUsRepository, WhyUsRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

#endregion



#region UnitOfWork


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

#endregion



#region Services


builder.Services.AddScoped<IFileService, FileService>();





builder.Services.AddScoped<IAboutUsService, AboutUsService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<ICustomerReactionService, CustomerReactionService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<ISliderService , SliderService>();
builder.Services.AddScoped<IWhyUsService, WhyUsService>();
builder.Services.AddScoped<IBlogService , BlogService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

#endregion




#region Mapping
builder.Services.AddAutoMapper(x =>
{

    x.AddProfile(new AboutUsMappingProfile());
    x.AddProfile(new ContactMappingProfile());
    x.AddProfile(new CustomerReactionMappingProfile());
    x.AddProfile(new TagMappingProfile());
    x.AddProfile(new SliderMappingProfile());
    x.AddProfile(new WhyUsMappingProfile());
    x.AddProfile(new BlogMappingProfile());
    x.AddProfile(new ProductMappingProfile());
    x.AddProfile(new CategoryMappingProfile());
 
});


#endregion



builder.Services.AddCors(p => p.AddPolicy("policy1", builder =>
{
    builder.WithOrigins("http://127.0.0.1:5500/" , "http://localhost:5500");
   builder.AllowAnyMethod();
    builder.AllowAnyHeader();

}));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}





app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseHttpsRedirection();
app.UseCors("policy1");
/*app.UseAuthentication();
app.UseAuthorization();*/
app.MapControllers();
app.UseMiddleware<CustomExceptionMiddleware>();

app.Run();



