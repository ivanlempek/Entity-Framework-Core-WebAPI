using System.Collections.Generic;
using System.Threading.Tasks;
using EFCore.Domain;

namespace EFCore.Repository
{
    public interface IEfCoreRepository
    {
        // Aqui criamos a nossa Interface inicial, que é um contrato
        // O void Add é o método que vai adicionar todas as nossas entidades por aqui
        // Vamos passar um tipo genérico que vamos chamar de 'T'
        // E vamos dizer que esse 'T' é uma entidade : (T entity)
        // Esse 'T' onde 'T' é igual uma classe : where T : class
        // E o 'Add' está totalmente relacionado ao tipo 'T' : <T>
        // Agora nós temos uma assinatura genérica que aceita qualquer tipo
        // E vamos replicar ela para um 'Update' e um ' Delete'
        // E com esses 3 métodos genéricos eu sei que eu posso passar qualquer entidade do contexto
        // Ou seja, o Herói, Batalha e qualquer outro modelo do banco
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;

        Task<bool> SaveChangeAsync();
        
        // Aqui fazemos o select ( listagem de objetos do nosso banco )
        Task<Hero[]> GetAllHeros(bool incluirBatalha = false);
        Task<Hero?> GetHeroById(int id, bool incluirBatalha = false);
        Task<Hero[]> GetHerosByName(string name, bool incluirBatalha =false);
        
        Task<Battle[]> GetAllBattles(bool incluirBatalha = false);
        Task<Battle?> GetBattleById(int id, bool incluirBatalha = false);
    }
}