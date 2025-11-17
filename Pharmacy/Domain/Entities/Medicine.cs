using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Domain.Entities
{
    public class Medicine
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(200)]
        public string? Description { get; set; }

        [Required]
        [StringLength(50)]
        public string Category { get; set; } = string.Empty;

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }

        public DateTime? ExpiryDate { get; set; }

        [StringLength(50)]
        public string? Manufacturer { get; set; }
    }
}
