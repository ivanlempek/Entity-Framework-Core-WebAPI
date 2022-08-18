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
    public class BattleController : ControllerBase
    {
        // GET: api/Battle
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok(new Battle());
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // GET: api/Battle/5
        [HttpGet, Route("[controller]/Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Battle
        [HttpPost]
        public ActionResult Post(Battle model)
        {
            try
            {
                using (var context = new HeroContext())
                {
                    context.Battles.Add(model);
                    context.SaveChanges();

                    return Ok("Batalha adicionada com sucesso!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // PUT: api/Battle/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, Battle model)
        {
            using (var context = new HeroContext())
            {
                if (context.Battles.AsNoTracking().FirstOrDefault(b => b.Id == id) != null)
                {
                    context.Update(model);
                    context.SaveChanges();

                    return Ok("Batalha atualizada com sucesso!!");
                }

                return Ok("Batalha não encontrada!!");
            }
        }
        
        // DELETE: api/Battle/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}