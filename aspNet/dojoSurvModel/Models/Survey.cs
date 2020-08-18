using System.ComponentModel.DataAnnotations;


namespace dojoSurvModel.Models
{
    public class Survey
    {
        [Required]
        [MinLength(2)]
        public string Name {get; set;}
        [Required]
        public string Location {get; set;}
        [Required]
        public string Language {get; set;}
        [Range(0,20)]
        public string Comment {get; set;}
    }
}