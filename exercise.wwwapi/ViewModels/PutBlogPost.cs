using System.ComponentModel.DataAnnotations.Schema;

namespace exercise.wwwapi.ViewModels
{
    public class PutBlogPost
    {
        public string? Text { get; set; }
        public int? UserId { get; set; }
    }
}
