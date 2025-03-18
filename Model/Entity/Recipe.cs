using System.ComponentModel.DataAnnotations;

namespace CommonApi.Model.Entity
{
 

    public class Recipe
    {

        [Key]
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
    }
}