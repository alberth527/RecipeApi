namespace CommonApi.Model.Entity
{
    public class Member
    {
        public int MemberId { get; set; }           // 會員ID
        public string FullName { get; set; }        // 會員姓名
        public string Email { get; set; }           // 會員電子郵件
        public string Password { get; set; }        // 密碼
        public string Phone { get; set; }           // 電話號碼（可選）
        public DateTime? DateOfBirth { get; set; }  // 生日（可選）
        public string Address { get; set; }         // 地址（可選）
        public DateTime RegistrationDate { get; set; }  // 註冊日期
        public string Status { get; set; }          // 會員狀態（默認是 'active'）
    }
}
