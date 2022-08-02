using HotListing.API.Configurations;
using HotListing.API.Contracts;
using HotListing.API.Data;
using HotListing.API.Repository;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//I'm letting the API know about the entity framework in order to use it to connect to the database. 1st, i'll define the connection string
var ConnectionString = builder.Configuration.GetConnectionString("HotelListingDBConnectionString");
builder.Services.AddDbContext<HotelListingDbContext>(options=>
{
    options.UseSqlServer(ConnectionString); //configuring the server i'm using  
    
    });
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", b => b.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
});

//this is a configuration for the serilog to know it should start working when the app starts running
//the configuration here represents the configuration files that is in the project starting with the jason files so eventually
//i'm going to need to do some configuration in the json file
builder.Host.UseSerilog((ctx,lc)=>lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));


builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>)); //register for generic repo
builder.Services.AddScoped<ICountriesRepository,CountriesRepository>(); //register for specific repo
builder.Services.AddScoped<IHotelsRepository, HotelsRepository>();
builder.Services.AddAutoMapper(typeof(MapperConfig));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//telling the pipline to use the policy above
app.UseCors("AllowAll");
app.UseAuthorization();

app.MapControllers();

app.Run();
