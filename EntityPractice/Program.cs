using Context;
using EntityPractice.Geometry;
using EntityPractice.Repositories.CinemaMovieRepository;
using EntityPractice.Repositories.CinemaRepository;
using EntityPractice.Repositories.MovieRepository;
using EntityPractice.Repositories.MovieTheaterRepository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.
    builder.Services.AddControllers();
    builder.Services.AddDbContext<AppDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("default"),
            //option to use netTopology
            sqlServerOptionsAction => sqlServerOptionsAction.UseNetTopologySuite());
    });
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    //Dependecy Injection
    builder.Services.AddScoped<IMovieTheaterRepository, MovieTheaterRepository>();
    builder.Services.AddScoped<ICinemaRepository, CinemaRepository>();
    builder.Services.AddScoped<IMovieRepository, MovieRepository>();
    builder.Services.AddScoped<ICinemaMovieRepository, CinemaMovieRepository>();
    //Other Services
    builder.Services.AddTransient<IGeometryFactory, GeometryFactory>();
    //Adding the AutoMapper
    builder.Services.AddAutoMapper(typeof(Program));
}

var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.MapControllers();

    app.Run();
}
