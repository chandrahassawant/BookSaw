namespace BulkyWeb.Helpers
{
    public static class ViewHelpers
    {
        public static string IsSelected(int categoryId, int selectedCategoryId)
        {
            return categoryId == selectedCategoryId ? "selected" : "";
        }
    }
}
