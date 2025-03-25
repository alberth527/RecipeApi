using System.ComponentModel.DataAnnotations;

namespace CommonApi.Model.Entity
{
    public class Member
    {
        [Key]
        public int member_id { get; set; }           // 會員ID（主鍵）

        [Required]
        public string full_name { get; set; }        // 會員姓名（必填）

        [Required]
        [EmailAddress]
        public string Email { get; set; }           // 會員電子郵件（必填）

        [Required]
        public string Password { get; set; }        // 密碼（必填）

        public string? Phone { get; set; }           // 電話號碼（可選）
        public DateTime? date_of_birth { get; set; }  // 生日（可選）
        public string? Address { get; set; }         // 地址（可選）

        public DateTime registration_date { get; set; } = DateTime.Now;  // 註冊日期（自動設置為當前時間）
        public string Status { get; set; } = "active";          // 會員狀態（預設是 'active'）
    }
}
