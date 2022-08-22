using EFCore.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NuGet.Packaging.Signing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Vamos adicionar o AddScoped aqui para fazer a implementação da interface
// Logo adicionamos a nossa interface 'IEFCoreRepository' e a implementação dessa interface que é o EFCoreRepository:
// Então, dado a necessidade da nossa controller de um repositório eu quero que
// Sempre que ela precisar de um tipo 'IEFCoreRepository', ou seja, alguém que inplementou essa interface
// Mande para ela o 'EFCoreRepository'
builder.Services.AddScoped<IEfCoreRepository, EfCoreRepository>();

// Configuração para não cair em um loop infinito na hora de fazer pesquisas dentro do banco de dados
/*builder.Services.AddMvc()
    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
    .AddJsonOptions(opt => opt.SerializerSettings.ReferenceLoopHandling =
        Newtonsoft.Json.ReferenceLoopHandling.Ignore);*/

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();