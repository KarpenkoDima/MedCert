namespace WindowsFormsApp1.Models
{
    public class TextTemplate
    {
        public const string CategoryMedCheck = "7. Результат огляду";
        public const string CategoryMedAnalisys = "8. Результат обстеження";

        public static readonly string[] Categories = { CategoryMedCheck, CategoryMedAnalisys };
        public int Id { get; set; }
        public string Category { get; set; } 
        public string Text { get; set; }
    }
}