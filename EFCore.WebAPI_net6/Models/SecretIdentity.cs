﻿namespace EFCore.WebAPI_net6.Models
{
    // Classe da identidade secreta de um heroi
    // Que ele pode ter ou não
    public class SecretIdentity
    {
        public int Id { get; set; }
        // Nome Real do heroi
        public int RealName { get; set; }
        public int HeroId { get; set; }
        public Hero Hero { get; set; }
    }
}