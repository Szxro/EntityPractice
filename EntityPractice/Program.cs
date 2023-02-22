using Context;
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