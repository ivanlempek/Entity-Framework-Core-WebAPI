using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCore.Domain;
using EFCore.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EFCore.WebAPI_net6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BattleController : ControllerBase
    {
        // Criamos aqui o contexto com a interface
        private readonly IEfCoreRepository _repo;

        public BattleController(IEfCoreRepository repo)
        {
            _repo = repo;
        }

        // GET: api/Battle
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var heros = await _repo.GetAllBattles(incluirBatalha: true);
                return Ok(heros);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // GET: api/Battle/5
        [HttpGet("{id}", Name = "GetBattle")]
        public async Task<IActionResult>Get(int id)
        {
            try
            {
                var heros = await _repo.GetBattleById(id, true);
                return Ok(heros);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // POST: api/Battle
        [HttpPost]
        public async Task<IActionResult> Post(Battle model)
        {
            try
            {
                /*using (var context = new HeroContext())
                {
                    context.Battles.Add(model);
                    context.SaveChanges();*/
                _repo.Add(model);

                if (await _repo.SaveChangeAsync())
                {
                    return Ok("Batalha adicionada com sucesso!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }

            return BadRequest("Não foi possível salvar a Batalha.");
        }

        // PUT: api/Battle/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Battle model)
        {
            try
            {
                var hero = await _repo.GetBattleById(id);
                if (hero != null)
                {
                    _repo.Update(model);
                    if(await _repo.SaveChangeAsync()) 

                        return Ok("Objeto atualizado com sucesso!!!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
            return BadRequest($"Não deletado!");
        }

        // DELETE: api/Battle/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var hero = await _repo.GetBattleById(id);
                if (hero != null)
                {
                    _repo.Delete(hero);
                   if(await _repo.SaveChangeAsync()) 

                    return Ok("Objeto deletado com sucesso!!!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
            return BadRequest($"Não deletado!");
        }
    }
}