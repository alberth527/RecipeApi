namespace CommonApi.Model.Entity
{
    public class RecipeDetails
    {
        public int RecipeId { get; set; } // 主键，对应食谱的ID
        public string Title { get; set; } // 食谱标题
        public string Description { get; set; } // 食谱描述
        public string Ingredients { get; set; } // 食材列表
        public string Steps { get; set; } // 制作步骤
        public string ImageUrl { get; set; } // 图片URL
    }
}
