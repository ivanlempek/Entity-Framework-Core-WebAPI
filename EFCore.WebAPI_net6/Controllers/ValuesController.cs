using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using EFCore.Domain;
using EFCore.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace EFCore.WebAPI_net6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // Vamos fazer o get dos nossos elementos agora usando o Linq
        [HttpGet]
        public ActionResult Get()
        {
            // Vamos instanciar e chamar a nossa conexão com o banco de dados com o new HeroContext
            using (var context = new HeroContext())
            {
                // Aqui usamos o método 'ToList' do Linq para listar os herois
                var listHeros = context.Heros.ToList();
                return Ok(listHeros);
            }
        }

        // Get por Nome usando o Linq por querie ou lambda expression
        [HttpGet("filter/{name}")]
        public ActionResult GetFilter(string name)
        {
            using (var context = new HeroContext())
            {
                // Fazendo o get com as queryes do linq
                /*var listHeroName = (from hero in context.Heros
                    where hero.Name.Contains(name)
                    select hero).ToList();*/

                // Fazendo o get com lambda expression
                // o .ToList vai retornar todos os registros do banco de dados
                var listHeroName = context.Heros.Where(h => h.Name.Contains(name)).ToList();
                // Fazendo o get com o Like do EntityFramework
                // O orderBy vai retornar a lista de heróis por Id - podendo ser de forma descendente: OrderByDescending (que seria o último)
                // O FirtOrDefault retorna o primeiro da lista no banco de dados, ou seja, apenas um elemento do banco
                var listHeroi2 = context.Heros.Where(h => EF.Functions.Like(h.Name, $"%{name}%"))
                    .OrderByDescending(h => h.Id)
                    .FirstOrDefault();
                // Método para retornar somente um elemento da lista ( primeiro ) - se ele encontrar mais de um elemento ele irá levantar uma exceção (erro)
                //  .SingleOrDeafult(); 
                // Método para retornar o último elemento da lista  de Id's
                //  .LastOrDefault();
                return Ok(listHeroi2);

                // Um detalhe é que sempre que terminamos de usar o ToList a conexão com o banco é fechada
                // Para executar várias funções sem interromper a conexão é bom usar um foreach
                /*foreach (var item in listHeroName)
                {
                    realizaCalculo();
                    criaArquivos();
                    salvaRelatorio();
                }*/
            }
        }

        // EndPoint para inserir um herói
        [HttpPost("{nameHero}")]
        public void Post(string nameHero)
        {
            // Aqui é o objeto 'Hero' que vamos criar para fazer o insert
            // Dentro do objeto nós vamos colocar os parâmetros para a criação do 'Heroi'
            // Quando vc coloca nos parâmetros um name sem um id
            // O entityframework entende que isso é um insert
            // Caso estivesse com um id junto ele iria entender que seria um update ( mesmo tendo um Heros.Add ali embaixo)
            var hero = new Hero {Name = nameHero};
            // o using vai realizar o insert
            using (var context = new HeroContext())
            {
                // Agora a var hero se torna o objeto heroi do banco, recebendo aqui todos seus atributos
                // para esse endpoint funcionar basta dar /update/'nomeDoHeroi'
                hero.Name = nameHero;

                // Aqui vamos utilizar o contexto para fazer o insert
                // .Heros.Add estamos explicitando que vamos inserir um heroi
                context.Heros.Add(hero);
                context.SaveChanges();
            }
        }

        // EndPoint para adicionar novos Heróis
        [HttpGet("AddRange")]
        public void GetAddRange()
        {
            using (var context = new HeroContext())
            {
                // Aqui vamos adicionar vários herois via AddRange
                // Usando o addrange podemos adicionar vários objetos do tipo 'hero'
                // E como o tipo é 'Hero' todos esses novos objetos vão ser automaticamente adicionados na tabela Hero
                context.AddRange(
                    new Hero {Name = "Capitão América"},
                    new Hero {Name = "Doutor Estranho"},
                    new Hero {Name = "Homem Aranha"},
                    new Hero {Name = "Hulk"},
                    new Hero {Name = "Capitão Marvel"},
                    new Hero {Name = "Cable"}
                );
                context.SaveChanges();
            }
        }

        // EndPoint de atualização de um herói
        [HttpPut("{id}")]
        public void Put(int id, string name)
        {
            {
                using (var context = new HeroContext())
                {
                    // Agora estamos usando esse endpoint para o método UPDATE
                    // Logo vamos dar o update por id
                    // Esse id = 1 é o número da id do banco de dados que vai ser retornado para essa var hero
                    // Aqui, através do contexto(banco), fazemos um SELECT dos Herois onde os Id's são iguais a 1
                    // FirsOrDeafult retorna o primeiro ou o padrão
                    var hero = context.Heros.Where(h => h.Id == id).Single();

                    // Aqui vamos fazer o UPDATE do atributo nome do id escolhido
                    hero.Name = name;

                    context.SaveChanges();
                }
            }
        }

        // EndPoint para deletar um herói
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            using (var context = new HeroContext())
            {
                // Vamos criar a variável hero e atribuir o nosso hero pelo id do contexto (banco de dados)
                // E vamos usar uma lambda expression para passar o id como parãmetro
                var hero = context.Heros.Where(x => x.Id == id).Single();

                // E aqui usamos o Remove para remover o heroi que acabamos de chamar com a variável de cima
                context.Heros.Remove(hero);
                context.SaveChanges();
            }
        }
    }
}