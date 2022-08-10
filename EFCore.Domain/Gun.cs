namespace EFCore.Domain
{
    public class Gun
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Hero Hero { get; set; }
        public int HeroId { get; set; }
    }
}