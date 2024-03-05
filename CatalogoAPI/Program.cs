using CatalogoAPI.ApiEndpoints;
using CatalogoAPI.AppServicesExtensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.AddApiSwagger();
builder.AddPersistensen();
builder.Services.AddCors();
builder.AddAutenticationJwt();



var app = builder.Build();

//Endpoint para login
app.MapAutenticacaoEndPoints();


//Endpoints
app.MapCategoriasEndpoints();

//Produtos endpoints
app.MapProdutoEndpoints();

// Configure the HTTP request pipeline.
var environment = app.Environment;
app.UseExceptionHandling(environment).UseSwaggerBuilder().UseAppCors();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
