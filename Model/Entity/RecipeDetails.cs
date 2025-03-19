namespace CommonApi.Model.Entity
{
    public class RecipeDetails
    {
        public int recipe_id { get; set; } // 主键，对应食谱的ID
        public string Title { get; set; } // 食谱标题
        public string[] ingredients { get; set; }
        public string[] steps { get; set; }
        public string image_url { get; set; } // 图片URL
    }
}
