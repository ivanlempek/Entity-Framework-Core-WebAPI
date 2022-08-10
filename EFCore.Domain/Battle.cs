using System;
using System.Collections.Generic;

namespace EFCore.Domain
{
    public class Battle
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DtBegining { get; set; }
        public DateTime DtEnd { get; set; }
        // Agora iremos fazer o mesmo relacionamento feito em herois aqui em batalha
        public List<HeroBattle> HerosBattles { get; set; }
    }
}