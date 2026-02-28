using Microsoft.EntityFrameworkCore;
using ProductApi.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.AddDbContext<ProductDbContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHealthChecks()
    .AddDbContextCheck<ProductDbContext>("sqlserver");

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// app.MapGet("/api/products/{id:int}", async (int id, ProductDbContext dbContext, CancellationToken cancellationToken) =>

app.MapGet("/api/products/{id:int}", (int id, CancellationToken cancellationToken) =>
    {
       return Results.Ok(new
        {
            Id = id,
            Name = $"Product {id}",
            Description = $"Description for product {id}",
            Price = 9.99m,
            Currency = "USD",
            InStock = true
        });
        // var product = await dbContext.Products
        //     .AsNoTracking()
        //     .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        // return product is null
        //     ? Results.NotFound(new { message = $"Product with id {id} was not found." })
        //     : Results.Ok(product);
    })
    .WithName("GetProductById");

app.MapHealthChecks("/health");

app.Run();
