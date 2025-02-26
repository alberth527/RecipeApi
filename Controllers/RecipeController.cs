using Comm.Model.Entity;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using CommonApi.Model.Entity;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CommonApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : BaseController
    {
        public RecipeController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : base(configuration, httpContextAccessor)
        {
        }

        [HttpGet()]
        public APIResult Get()
        {
            var result = new APIResult();
            result.Data=DB.NGConnection.Query<dynamic>("SELECT id, title, description\r\nFROM public.recipes;").ToList();

            

        
            return result;
        }
        /// <summary>   
        /// 新增食譜
        /// 

        [HttpPost()]
        public APIResult Post([FromBody] Recipe recipe)
        {
            var result = new APIResult();
            try
            {

            result.IsSuccess= DB.RecipeRepository.AddRecipe(recipe)>0;
            }
            catch (Exception ex) 
            {

                result.IsSuccess = false;
                result.Data=ex.Message;

            }
         
            return result;
        }
        /// <summary>
        /// 修改食譜
        /// 
        [HttpPatch()]
        public APIResult Put([FromBody] Recipe recipe)
        {
            var result = new APIResult();
            try
            {

                result.IsSuccess = DB.RecipeRepository.UpdateRecipe(recipe) > 0;
            }
            catch (Exception ex)
            {

                result.IsSuccess = false;
                result.Data = ex.Message;

            }
            return result;
        }
        /// <summary>
        /// by id 刪除食譜
        /// 
        [HttpDelete()]
        public APIResult Delete([FromBody] Recipe recipe)
        {
            var result = new APIResult();
           
            try
            {
                result.IsSuccess = DB.RecipeRepository.Delete(recipe) > 0;
            }
            catch (Exception ex)
            {

                result.IsSuccess = false;
                result.Data = ex.Message;

            }
            return result;
        }

    }
}
