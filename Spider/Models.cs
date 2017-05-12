using System.ComponentModel.DataAnnotations;

namespace Spider
{
    public class UrlEntity
    {
        [Key]
        public int Id { get; set; }

        [StringLength(maximumLength: 255)]
        public string Value { get; set; }
    }

    public class VisitedEntity
    {
        [Key]
        public int Id { get; set; }

        [StringLength(maximumLength: 255)]
        public string Value { get; set; }
    }

    public class FoundEntity
    {
        [Key]
        public int Id { get; set; }

        [StringLength(maximumLength: 255)]
        public string Value { get; set; }
    }
}