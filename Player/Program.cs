using player.Musica;
using System.Text.Json;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

builder.WebHost.UseUrls("http://0.0.0.0:8000");

Musica[] musicas = new Musica[1500];
int todasMusicas = 0;

app.MapGet("/", () =>
{
    return Results.Ok("API Player funcionando com sucesso!");
});

app.MapPost("/musica", (JsonElement body) => {

    Random random = new();

    Musica nova_musica = new Musica();

    nova_musica.Id = random.Next(1, 1500);
    nova_musica.Titulo = body.GetProperty("titulo").GetString();
    nova_musica.Artista = body.GetProperty("artista").GetString();
    nova_musica.Compositor = body.GetProperty("compositor").GetString();
    nova_musica.Genero = body.GetProperty("genero").GetString();
    nova_musica.Ano = body.GetProperty("ano").GetInt32();

    musicas[todasMusicas] = nova_musica;
    todasMusicas++;

    return Results.Ok(new
    {
        todasMusicas
    });
});

app.MapGet("/musicas/busca", (string titulo) =>
{
    Musica[] musicasEncontradas = new Musica[todasMusicas];

    int todasEncontradas = 0;

    for (int i = 0; i < todasMusicas; i++)
    {
        if (musicas[i].titulo.ToLower() == titulo.ToLower())
        {
            musicasEncontradas[todasEncontradas] = musicas[i];
            todasEncontradas++;
        }
    }

    if (todasEncontradas > 0)
    {
        Musica[] resultadoFinal = new Musica[todasEncontradas];

        for (int i = 0; i < todasEncontradas; i++)
        {
            resultadoFinal[i] = musicasEncontradas[i];
        }        

        return Results.Ok(new
        {
            titulo,
            musicas = musicasEncontradas
        });
    } 

    return Results.NotFound(new
    {
        message = "Nenhuma Musica encontrado esse titulo."
    });
});





app.Run();