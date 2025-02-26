using Comm.Model;
using CommonApi.Model.Entity;
using System.Xml.Linq;

namespace CommonApi.Model.Repositorys
{
    public class RecipeRepository : BaseRepository<Recipe>
    {
        public RecipeRepository()
        {
            TableName = "Recipe";
            DBName = "NGsql";
        }
        public int AddRecipe(Recipe item)
        {
          
            return AddItem(item);
        }
        public int UpdateRecipe(Recipe item)
        {
         
            return Update(item);
        }
    }
}
