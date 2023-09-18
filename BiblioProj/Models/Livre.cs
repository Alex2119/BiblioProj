using System.Text.Json.Serialization;

namespace BiblioProj.Models
{
    public class Livre
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public DateTime AnneeDePublication { get; set; }
        public int AuteurId { get; set; }
        [JsonIgnore]
        public Auteur? Auteur { get; set; }
    }
}
