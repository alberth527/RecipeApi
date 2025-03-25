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
            member.member_id = id;

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
        /// <summary>
        /// 會員登入
        /// </summary>
        [HttpPost("login")]
        public APIResult Login([FromBody] LoginModel model)
        {
            var result = new APIResult();
            try
            {
                // 查詢使用者
                var member = DB.NGConnection.QueryFirstOrDefault<Member>(
                    "SELECT * FROM public.members WHERE full_name = @full_name",
                    new { full_name = model.full_name });

                if (member == null)
                {
                    result.IsSuccess = false;
                    result.Message = "找不到此帳戶";
                    return result;
                }

                // 驗證密碼
               
                if (member.Password != model.Password)
                {
                    result.IsSuccess = false;
                    result.Message = "密碼錯誤";
                    return result;
                }

                // 登入成功，返回用戶信息（排除密碼）
                result.IsSuccess = true;
                result.Message = "登入成功";
                result.Data = new
                {
                    member.member_id,
                    member.full_name,
                    member.Email,
                    member.Phone,
                    member.date_of_birth,
                    member.Address,
                    member.registration_date,
                    member.Status
                };
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Data = ex.Message;
            }
            return result;
        }
    }
    public class LoginModel
    {
        public string full_name { get; set; }
        public string Password { get; set; }
    }
}
