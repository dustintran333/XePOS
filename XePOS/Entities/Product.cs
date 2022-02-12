using System.ComponentModel.DataAnnotations;

namespace XePOS.Application.Entities;

public struct Product
{
    [Required(AllowEmptyStrings = false)]
    public string Code { get; set; }

    [Required]
    [Range(0.0, double.MaxValue, ErrorMessage = "Invalid price value")]
    public decimal Price { get; set; }

    public Promotion? Promotion { get; set; }
}