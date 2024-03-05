using CatalogoAPI.Context;
using CatalogoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogoAPI.ApiEndpoints
{
    public static class ProdutosEndpoints
    {
        public static void MapProdutoEndpoints(this WebApplication app)
        {
            app.MapGet("/produtos", async (AppDbContext db) =>
            {
                return await db.Produtos.ToListAsync();
            }).WithTags("Produtos").RequireAuthorization();

            app.MapGet("/produtos/{id:int}", async (int id, AppDbContext db) =>
            {
                return await db.Produtos.FindAsync(id) is Produto produto ? Results.Ok(produto) : Results.NotFound();
            });

            app.MapPost("/produtos", async (Produto produto, AppDbContext db) =>
            {
                db.Produtos.Add(produto);
                await db.SaveChangesAsync();
                return Results.Created($"/produtos/{produto.ProdutoId}", produto);
            });

            app.MapPut("/produtos/{id:int}", async (int id, Produto produto, AppDbContext db) =>
            {
                if (produto.ProdutoId != id)
                {
                    return Results.BadRequest();
                }
                var produtoDB = await db.Produtos.FindAsync(id);

                if (produtoDB is null)
                {
                    return Results.NotFound();
                }

                produtoDB.Nome = produto.Nome;
                produtoDB.Descricao = produto.Descricao;
                produtoDB.Imagem = produto.Imagem;
                produtoDB.Preco = produto.Preco;
                produtoDB.DataCompra = produto.DataCompra;
                produtoDB.Estqoue = produto.Estqoue;
                produtoDB.CategoriaId = produto.CategoriaId;

                await db.SaveChangesAsync();
                return Results.Ok();
            });

            app.MapDelete("/produtos/{id:int}", async (int id, AppDbContext db) =>
            {
                var produto = await db.Produtos.FindAsync(id);

                if (produto is null)
                {
                    return Results.NotFound();
                }

                db.Remove(produto);
                await db.SaveChangesAsync();
                return Results.NoContent();

            });

        }
    }
}
