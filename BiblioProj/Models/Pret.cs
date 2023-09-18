using System.Text.Json.Serialization;

namespace BiblioProj.Models
{
    public class Pret
    {
        public int Id { get; set; }
        public string Emprunteur { get; set; }
        public DateTime DateDeRetour { get; set; }
        public int LivreId { get; set; }
        [JsonIgnore]
        public Livre? Livre { get; set;}
    }
}
