using System.Collections.Generic;

namespace EFCore.Domain
{
    public class Hero
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // Aqui criamos uma nova relação entre heroi e identidade secreta
        // E nem sempre um herois vai ter uma identidade secreta
        public SecretIdentity Identity { get; set; }
        
        // Como um heroi pode ter varias armas é bom adicionarmos um List de armas tbm
        // É bom colocar essas duas Listas pois caso vc for criar uma api ou passar esses dados para o dev front-end
        // Eles já irão receber o heroi com todas as batalhas que ele participou e todas as armas que ele já tem
        public List<Gun> Guns { get; set; }
        // Agora o heroi está para várias batalhas
        // E para isso iremos usar o List
        public List<HeroBattle> HerosBattles { get; set; }
        
        // Como o heroi agora n tem mais a relação de um para um com a batalha, retiramos esses dois campos
        // public Battle Battle { get; set; }
       // public int BattleId { get; set; }
    }
}