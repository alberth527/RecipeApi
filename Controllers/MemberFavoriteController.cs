using Comm.Model.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using CommonApi.Model.Entity;
using System;
using System.Linq;

namespace CommonApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberFavoriteController : BaseController
    {
        public MemberFavoriteController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : base(configuration, httpContextAccessor)
        {
        }

        [HttpGet()]
        public APIResult Get()
        {
            var result = new APIResult();
            try
            {
                result.Data = DB.NGConnection.Query<dynamic>(@"SELECT * FROM public.member_favorites;").ToList();
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Data = ex.Message;
            }
            return result;
        }

        /// <summary>   
        /// 新增收藏
        /// </summary>
        [HttpPost()]
        public APIResult Post([FromBody] MemberFavorite favorite)
        {
            var result = new APIResult();
            try
            {
                var sql = @"INSERT INTO public.member_favorites (member_id, recipe_id)
VALUES (@member_id, @recipe_id)
ON CONFLICT (member_id, recipe_id) DO NOTHING;";

                result.IsSuccess = DB.NGConnection.Execute(sql, favorite) > 0;  
            }
            catch (Exception ex) 
            {
                result.IsSuccess = false;
                result.Data = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 修改收藏
        /// </summary>
        [HttpPatch()]
        public APIResult Put([FromBody] MemberFavorite favorite)
        {
            var result = new APIResult();
            try
            {
                result.IsSuccess = DB.MemberFavoriteRepository.UpdateFavorite(favorite) > 0;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Data = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 透過id刪除收藏
        /// </summary>
        [HttpDelete("{id}")]
        public APIResult Delete(int id)
        {
            var favorite = new MemberFavorite();
            favorite.favorite_id = id;

            var result = new APIResult();
           
            try
            {
                result.IsSuccess = DB.MemberFavoriteRepository.DeleteFavorite(favorite) > 0;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Data = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 搜尋收藏
        /// </summary>
        [HttpGet("search")]
        public APIResult Search([FromQuery] int memberId)
        {
            var result = new APIResult();
            try
            {
                var favorite = new MemberFavorite();
                favorite.member_id = memberId;
                result.Data = DB.NGConnection.Query<dynamic>(@"SELECT id, title, description,rd.image_url,rd.ingredients ,rd.steps FROM public.recipes r
                left join public.recipe_details rd on r.id =rd.recipe_id where  recipe_id  in(
SELECT recipe_id FROM public.member_favorites WHERE member_id =@member_id)", favorite).ToList();
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
