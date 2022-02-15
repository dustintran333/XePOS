using System.ComponentModel.DataAnnotations;

namespace XePOS.Application.Entities;

public struct Product
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Code cannot not be null or empty.")]
    public string Code { get; set; }

    [Range(0.0, double.MaxValue, ErrorMessage = "Price cannot be negative.")]
    public decimal Price { get; set; }

    public Promotion? Promotion { get; set; }
}