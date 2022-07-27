namespace EFCore.WebAPI_net6.Models
{
    public class HeroBattle
    {
        // Aqui vamos fazer a relaçao de muitos para muitos (many to many)
        // Aqui temos a tabela de herois e a tabela de batalhas
        // Muitos herois podem fazer parte de muitas batalhas
        // Logo, precisamos fazer uma tabela de relacionamento entre essas duas tabelas (herois e batalhas)
        // Entao precisamos do Id das duas tabelas no meio
        // Aqui em baixo temos o id do heroi e o objeto embaixo
        public int HeroId { get; set; }
        public Hero Hero { get; set; }
        // Aqui temos o id da batalha e o objeto embaixo
        public int BattleId { get; set; }
        public Battle Battle { get; set; }
    }
}