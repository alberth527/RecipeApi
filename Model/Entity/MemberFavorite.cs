using System.ComponentModel.DataAnnotations;

namespace CommonApi.Model.Entity
{
    public class MemberFavorite
    {
        [Key]
        public int favorite_id { get; set; }
        // 收藏ID（主鍵）

        [Required]
        public int member_id { get; set; }
        // 會員ID（必填）

        [Required]
        public int recipe_id { get; set; }
        // 收藏項目（必填）

        public DateTime created_at { get; set; } = DateTime.Now;
        // 添加日期（自動設置為當前時間）
    }
}
