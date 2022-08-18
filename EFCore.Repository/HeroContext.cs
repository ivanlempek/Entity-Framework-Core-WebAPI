using EFCore.Domain;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Repository
{
    // O DBContext é o que vai encapsular/mapear todas as nossas entidades do banco
    public class HeroContext : DbContext
    {
        // Quando vc cria uma classe / entidade lista ela precisa ficar no plural
        // Por que por via de regra elas já vão ser identificadas como listas sem precisar ver o tipo
        // Outro motivo seria que todas as nossas entidades do banco vão ser criadas com esses mesmos nomes, por isso o plural
        public DbSet<Hero> Heros { get; set; }
        public DbSet<Battle> Battles { get; set; }
        public DbSet<Gun> Guns { get; set; }
        // Vamos inserir agora no contexto de banco de dados as duas novas entidades que criamos
        public DbSet<HeroBattle> HerosBattles { get; set; }
        public DbSet<SecretIdentity> SecretIdentities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // String de conexão
            // Feita conexão já podemos fazer nossas migrações
            // Para gerar a connectionString, nesse caso, criei um arquilo .udl na área de trabalho
            // e adicionei o nome do server do SQL Management Studio e depois peguei a linha da conexão em alguma editor de texto
            // Mudar o catalog para o nome do banco desejado
            optionsBuilder.UseSqlServer("Password=269545;Persist Security Info=True;Trusted_Connection=True;Encrypt=False;Integrated Security=False;User ID=sa;Initial Catalog=HeroApp;Data Source=DESKTOP-JADL8B2");
        }
        
        // Aqui vamos dizer para o Entityframework o que fazer no banco de dados
        // Ou seja, que ele precisa considerar essas duas chaves ( batalha e heroi ) dentro de herobattle
        // que se tornam a minha chave composta
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HeroBattle>(entity =>
            {
                // Tem chave? tem. E logo nós dizemos que tem chave usando uma LAMBDA expression
                // Aqui dizemos para o banco que existe uma chave e ela é composta
                entity.HasKey(e => new {e.BattleId, e.HeroId});
            });
        }
    }
}
