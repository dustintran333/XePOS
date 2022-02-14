using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using XePOS.Application.Entities;

namespace XePOS.Application.Extensions;

public static class ValidationExtensions
{
    [ExcludeFromCodeCoverage]
    public static List<ValidationResult> ValidateModel(object model)
    {
        var validationResults = new List<ValidationResult>();
        Validator.TryValidateObject(
            model,
            new ValidationContext(model, null, null),
            validationResults,
            true);
        return validationResults;
    }

    [ExcludeFromCodeCoverage]
    public static List<ValidationResult> ValidateProductPricing(Product product)
    {
        var results = ValidateModel(product);

        if (product.Promotion.HasValue)
            results.AddRange(ValidateModel(product.Promotion.Value));

        return results;
    }

    public static bool IsPricingDataValid(this IList<Product> pricingList) =>
        !pricingList.Any(p => ValidateProductPricing(p).Count > 0);
}