﻿using Comm.Model.Entity;

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
            result.Data=DB.NGConnection.Query<dynamic>(@"SELECT id, title, description,rd.image_url FROM public.recipes r
                left join public.recipe_details rd on r.id =rd.recipe_id ;").ToList();

            

        
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
        [HttpDelete("{id}")]
        public APIResult Delete( int id )
        {
            var recipe = new Recipe();
            recipe.id = id;

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

    
            /// <summary>
        /// 搜尋食譜
        /// </summary>
        [HttpGet("search")]
        public APIResult Search([FromQuery] string q)
        {
            var result = new APIResult();
            try
            {
                var query = @"SELECT id, title, description, rd.image_url FROM public.recipes r
                              LEFT JOIN public.recipe_details rd ON r.id = rd.recipe_id
                              WHERE title ILIKE @Query OR description ILIKE @Query;";
                result.Data = DB.NGConnection.Query<dynamic>(query, new { Query = "%" + q + "%" }).ToList();
                result.IsSuccess = true;
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
