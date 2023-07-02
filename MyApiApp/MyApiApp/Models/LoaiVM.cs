using System.ComponentModel.DataAnnotations;

namespace MyApiApp.Models
{
    public class LoaiVM
    {
        [Required]
        [MaxLength(255)]
        public string TenLoai { get; set; }
    }
}
