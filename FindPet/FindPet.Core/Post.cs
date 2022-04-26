using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FindPet.Core
{
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Status { get; set; }
        public string Photo { get; set; }
        [Required]
        public string Description { get; set; }
        public string Location { get; set; }
        [Required]
        public string Username { get; set; }
    }
}
