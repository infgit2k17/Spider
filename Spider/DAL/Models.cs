using System.ComponentModel.DataAnnotations;

namespace Spider.DAL
{
    public class UrlEntity
    {
        [Key]
        public int Id { get; set; }

        [StringLength(maximumLength: 400)]
        public string Value { get; set; }
    }

    public class VisitedEntity
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(32)]
        public byte[] Value { get; set; }
    }

    public class FoundEntity
    {
        [Key]
        public int Id { get; set; }

        [StringLength(maximumLength: 400)]
        public string Value { get; set; }
    }
}