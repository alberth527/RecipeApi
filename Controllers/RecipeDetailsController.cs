using Comm.Model.Entity;

using CommonApi.Model.Entity;

using Dapper;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommonApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeDetailsController : BaseController
    {
        public RecipeDetailsController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : base(configuration, httpContextAccessor)
        {
        }
        [HttpGet("{id}")]

        public APIResult Get(int id)
        {
           
            var result = new APIResult();
            var query = new RecipeDetails();
            query.recipe_id = id;
            result.Data = DB.NGConnection.Query<dynamic>(@"SELECT id, title, description,rd.image_url,rd.ingredients ,rd.steps FROM public.recipes r
                left join public.recipe_details rd on r.id =rd.recipe_id where rd.recipe_id=@recipe_id", query).FirstOrDefault();
            return result;
        }

    }
}
