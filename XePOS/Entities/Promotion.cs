using System.ComponentModel.DataAnnotations;

namespace XePOS.Application.Entities;

public struct Promotion
{
    [Range(1, int.MaxValue)]
    public int BundleQuantity { get; set; }

    [Required]
    [Range(0.0, double.MaxValue, ErrorMessage = "Invalid price value")]
    public decimal BundlePrice { get; set; }

    public Promotion(int bundleQuantity, decimal bundlePrice)
    {
        BundleQuantity = bundleQuantity;
        BundlePrice = bundlePrice;
    }
}