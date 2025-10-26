using Microsoft.OpenApi.Models;
using firstNetMicroservice.Models;
using firstNetMicroservice.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PizzaStore API", Description = "Making the Pizzas you love", Version = "v1" });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
   c.SwaggerEndpoint("/swagger/v1/swagger.json", "PizzaStore API V1");
});

app.MapGet("/pizzas", () => PizzaService.GetAll());
app.MapGet("/pizzas/{id}", (int id) => PizzaService.Get(id));
app.MapPost("/pizzas", (Pizza pizza) => { PizzaService.Add(pizza); return Results.Created($"/pizzas/{pizza.Id}", pizza); });
app.MapPut("/pizzas", (Pizza pizza) => { PizzaService.Update(pizza); return Results.NoContent(); });
app.MapDelete("/pizzas/{id}", (int id) => { PizzaService.Delete(id); return Results.NoContent(); });

app.Run();
