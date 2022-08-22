using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCore.Domain;
using Microsoft.EntityFrameworkCore;


namespace EFCore.Repository
{
    // Vamos aqui implementar a interface que criamos no IEFCoreRepository
    public class EfCoreRepository : IEfCoreRepository
    {
        private readonly HeroContext _context;

        // Quando criarmos a instância desse objeto 'EFCoreRepository' vamos receber o contexto como parâmetro
        public EfCoreRepository(HeroContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            // Agora conseguimos Adicionar a entidade que recemos como parâmetro na nossa assinatura
            //  Direto no banco de dados através do contexto
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangeAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        // Aqui iremos retornar todos os heróis
        public async Task<Hero[]> GetAllHeros(bool incluirBatalha = false)
        {
            // Aqui estamos atribuindo a 'query' todos os heróis do banco
            // E todos os heróis possuem uma identidade secreta e armas
            IQueryable<Hero> query = _context.Heros
                .Include(h =>h.Identity)
                .Include(h => h.Guns);

            // Vamos usar esse if para evitar o loop infinito
            // Nesse só vamos incluir a batalha caso o usuário confirmar como true
            if (incluirBatalha)
            {
                // Aqui adicionamos com o include os heros battles e batalhas
                query = query.Include(h => h.HerosBattles)
                    .ThenInclude(herob => herob.Battle);
            }
            query = query.AsNoTracking().OrderBy(h => h.Id);

            return await query.ToArrayAsync();
        }
        
        // Aqui iremos retornar um heróis por Id
        public async Task<Hero?> GetHeroById(int id, bool incluirBatalha)
        {
            IQueryable<Hero> query = _context.Heros
                .Include(h =>h.Identity)
                .Include(h => h.Guns);
            
            if (incluirBatalha)
            {
                query = query.Include(h => h.HerosBattles)
                    .ThenInclude(herob => herob.Battle);
            }
            query = query.AsNoTracking().OrderBy(h => h.Id);

            return await query.FirstOrDefaultAsync(h => h.Id == id);
        }

        // E aqui vamos retornar vários heróis por nome
        public async Task<Hero[]> GetHerosByName(string name, bool incluirBatalha = false)
        {
            IQueryable<Hero> query = _context.Heros
                .Include(h =>h.Identity)
                .Include(h => h.Guns);
            
            if (incluirBatalha)
            {
                query = query.Include(h => h.HerosBattles)
                    .ThenInclude(herob => herob.Battle);
            }
            query = query.AsNoTracking()
                .Where(h => h.Name.Contains(name))
                .OrderBy(h => h.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Battle[]> GetAllBattles(bool incluirHerois = false)
        {
            IQueryable<Battle> query = _context.Battles;

            if (incluirHerois)
            {
                query = query.Include(h => h.HerosBattles)
                    .ThenInclude(herob => herob.Hero);
            }
            query = query.AsNoTracking().OrderBy(h => h.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Battle?> GetBattleById(int id, bool incluirHerois = false)
        {
            IQueryable<Battle> query = _context.Battles;

            if (incluirHerois)
            {
                query = query.Include(h => h.HerosBattles)
                    .ThenInclude(herob => herob.Hero);
            }
            query = query.AsNoTracking().OrderBy(h => h.Id);

            return await query.FirstOrDefaultAsync();
        }
    }
}