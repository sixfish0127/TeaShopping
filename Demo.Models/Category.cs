using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Demo.Models
{
    public class Category
    {
        [Key]
        public int Id {  get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("類別名稱")]
        public string Name { get; set; }
        [DisplayName("類別順序")]
        [Range(1,200,ErrorMessage ="輸入1~200之間")]
        public int DisplayOrder{ get; set; }
    }
}
