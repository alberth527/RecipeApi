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
    public class MemberController : BaseController
    {
        public MemberController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : base(configuration, httpContextAccessor)
        {
        }

        [HttpGet()]
        public APIResult Get()
        {
            var result = new APIResult();
            try
            {
                result.Data = DB.NGConnection.Query<dynamic>(@"SELECT * FROM public.members;").ToList();
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
        /// 新增會員
        /// </summary>
        [HttpPost()]
        public APIResult Post([FromBody] Member member)
        {
            var result = new APIResult();
            try
            {
                result.IsSuccess = DB.MemberRepository.AddMember(member) > 0;
            }
            catch (Exception ex) 
            {
                result.IsSuccess = false;
                result.Data = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 修改會員資料
        /// </summary>
        [HttpPatch()]
        public APIResult Put([FromBody] Member member)
        {
            var result = new APIResult();
            try
            {
                result.IsSuccess = DB.MemberRepository.UpdateMember(member) > 0;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Data = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 透過id刪除會員
        /// </summary>
        [HttpDelete("{id}")]
        public APIResult Delete(int id)
        {
            var member = new Member();
            member.Id = id;

            var result = new APIResult();
           
            try
            {
                result.IsSuccess = DB.MemberRepository.DeleteMember(member) > 0;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Data = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 搜尋會員
        /// </summary>
        [HttpGet("search")]
        public APIResult Search([FromQuery] string q)
        {
            var result = new APIResult();
            try
            {
                var query = @"SELECT * FROM public.members WHERE name ILIKE @Query OR email ILIKE @Query;";
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
