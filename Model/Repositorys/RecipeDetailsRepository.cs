using Comm.Model;
using CommonApi.Model.Entity;
using System.Xml.Linq;

namespace CommonApi.Model.Repositorys
{
    public class RecipeDetailsRepository : BaseRepository<RecipeDetails>
    {
        public RecipeDetailsRepository()
        {
            TableName = "public.recipe_details";
            DBName = "NGsql";
        }
        public int AddRecipeDetails(RecipeDetails item)
        {

            return AddItem(item);
        }
        public int UpdateRecipeDetails(RecipeDetails item)
        {

            return Update(item);
        }
    }
}
