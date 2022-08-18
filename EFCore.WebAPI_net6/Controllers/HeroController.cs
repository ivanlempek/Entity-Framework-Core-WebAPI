using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCore.Domain;
using EFCore.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCore.WebAPI_net6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroController : ControllerBase
    {
        /*private readonly HeroContext _context;
        // Criamos esse construtor para poder chamar o contexto do banco de dados em cada endpoint
        // Sem a necessidade de ficar instanciando ele várias vezes 
        public HeroController(HeroContext context)
        {
            _context = context;
        }*/

        // GET: api/Hero
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                // Esse ok vai tranformar esse novo objeto hero em um JSON
                return Ok(new Hero());
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // GET: api/Hero/5
        [HttpGet, Route("[controller]/Get")]
        public ActionResult Get(int id)
        {
            return Ok("value");
        }

        // POST: api/Hero
        [HttpPost]
        public ActionResult Post(Hero model)
        {
            try
            {
                using (var context = new HeroContext())
                {
                    // Aqui vamos adicionar um herói pelo próprio Controller
                    /*var hero = new Hero
                    {
                        // Seu nome
                        Name = "Homem de Ferro",
                        // E vamos adicionar também algumas armas
                        // Para isso vamos usar o List
                        Guns = new List<Gun>
                        {
                            new Gun {Name = "Mac 3"},
                            new Gun {Name = "Mac 5"}
                        }
                    };*/

                    // Para inserir um herói pelo Postman não precisamos do código acima
                    // Precisamos apenas de um parâmetro ( model )

                    // Aqui estamos explicitando que vamos adicionar esse objeto héróis que acamos de criar na tabela 'heros'
                    // agora colocamos o model aqui que estamos passando como parâmetro
                    context.Heros.Add(model);
                    // Salva as inserções no banco de dados
                    context.SaveChanges();
                    return Ok("Herói adicionado com sucesso!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // PUT: api/Hero/5
        [HttpPut("{id}")]
        // Além do Id, vamos usar aqui quem vamos atualizar, no caso o herói 'model'
        public ActionResult Put(int id, Hero model)
        {
            using (var context = new HeroContext())
            {
                // Aqui atualizamos pelo Controller
                /*var hero = new Hero
                {
                    // Novo nome
                    // Id do herói que vamos atualizar
                    Id = id,
                    Name = "Iron Man",
                    Guns = new List<Gun>
                    {
                        // Nome e Id's das armas que vamos atualizar
                        new Gun {Id = 1, Name = "Mark III"},
                        new Gun {Id = 2, Name = "Mark V"}
                    }
                };*/

                // Vamos agora usar o PUT somente com o Postman 
                // Vamos utilizar o asnotracking para encontrarmos o herói e ele não fique travado
                // logo vamos usar uma lambda expression para comparar o id que estamos recebendo com parâmetro com o id do nosso objeto herói
                // Se o id for encontrado nós vamos pegar o model que estamos passando como parâmetro
                // Realizar o update e salvar ele no banco
                if (context.Heros.AsNoTracking().FirstOrDefault(h => h.Id == id) != null)
                {
                    context.Update(model);
                    context.SaveChanges();

                    return Ok("Herói atualizado com sucesso!");
                }

                return Ok("Herói não encontrado!!");

                // Executando o update no herói
                /*context.Heros.Update(hero);*/
                // context.Update(hero); Esse tbm funcionaria pois já explicitamos que esse objeto é do tipo herói ali em cima
                // Salva as inserções no banco de dados
                /*context.SaveChanges();*/
                /*return Ok("Herói atualizado com sucesso!");*/
            }
        }

        // DELETE: api/Hero/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}