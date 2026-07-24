using player.Musica;
using System.Text.Json;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.WebHost.UseUrls("http://0.0.0.0:8000");

var app = builder.Build();

app.UseCors("AllowAll");

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

app.MapGet("/musicas", () =>
{
    Musica[] musicasCadastradas = new Musica[todasMusicas];

    for (int i = 0; i < todasMusicas; i++)
    {
        musicasCadastradas[i] = musicas[i];
    }

    return Results.Ok(new
    {
        musicasCadastradas
    });
});

app.MapGet("/musicas/{titulo}", (string titulo) =>
{
    Musica[] musicasEncontradas = new Musica[todasMusicas];

    int todasEncontradas = 0;

    for (int i = 0; i < todasMusicas; i++)
    {
        if (musicas[i].Titulo.ToLower() == titulo.ToLower())
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
        message = "Nenhuma Musica encontrado esse nesse genero."
    });
});

app.MapGet("/musicas/genero/{genero}", (string genero) =>
{
    Musica[] musicasEncontradas = new Musica[todasMusicas];

    int todasEncontradas = 0;

    for (int i = 0; i < todasMusicas; i++)
    {
        if (musicas[i].Genero.ToLower() == genero.ToLower())
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
            genero,
            musicas = musicasEncontradas
        });
    } 

    return Results.NotFound(new
    {
        message = "Nenhuma Musica encontrado esse nesse genero."
    });
});

app.MapGet("/musicas/artista/{artista}", (string artista) =>
{
    Musica[] musicasEncontradas = new Musica[todasMusicas];

    int todasEncontradas = 0;

    for (int i = 0; i < todasMusicas; i++)
    {
        if (musicas[i].Artista.ToLower() == artista.ToLower())
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
            artista,
            musicas = musicasEncontradas
        });
    } 

    return Results.NotFound(new
    {
        message = "Nenhuma música encontrada para este artista."
    });
});

app.MapGet("/musicas/ano/{ano}", (int ano) =>
{
    Musica[] musicasEncontradas = new Musica[todasMusicas];

    int todasEncontradas = 0;

    for (int i = 0; i < todasMusicas; i++)
    {
        if (musicas[i].Ano == ano)
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
            ano,
            musicas = resultadoFinal
        });
    } 

    return Results.NotFound(new
    {
        message = "Nenhuma música encontrada para este ano."
    });
});

app.MapGet("/musicas/compositor/{compositor}", (string compositor) =>
{
    Musica[] musicasEncontradas = new Musica[todasMusicas];

    int todasEncontradas = 0;

    for (int i = 0; i < todasMusicas; i++)
    {
        if (musicas[i].Compositor.ToLower() == compositor.ToLower())
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
            compositor,
            musicas = musicasEncontradas
        });
    } 

    return Results.NotFound(new
    {
        message = "Nenhuma música encontrada para este compositor."
    });
});

app.MapDelete("/musica/{id}", (int id) =>
{
    for (int i = 0; i < todasMusicas; i++)
    {
        if (musicas[i].Id == id)
        {
            Musica musicaRemovida = musicas[i];
            
            for (int j = i; j < todasMusicas - 1; j++)
            {
                musicas[j] = musicas[j + 1];
            }            

            todasMusicas--;

            return Results.Ok(new
            {
                mensagem = "Música removida com sucesso.",
                musica = musicaRemovida
            });
        }
    }

    return Results.NotFound(new
    {
        message = "Música não encontrada."
    });
});


app.Run();