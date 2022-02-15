using XePOS.Application.Entities;

namespace XePOS.Application.Interfaces;

public interface IPointOfSaleTerminal
{
    /// <summary>
    /// Return the current pricing list
    /// </summary>
    IList<Product> GetProductPricing();

    /// <param name="code">The product code</param>
    /// <returns>The pricing for the product code</returns>
    /// <exception cref="ArgumentException"> Thrown when the code is null or empty </exception>
    /// <exception cref="InvalidOperationException">Thrown when the code cannot be found </exception>
    Product GetProductPricing(string code);

    /// <summary> Set pricing for a list of products </summary>
    /// <returns> The pricing list </returns>
    /// <param name="productPricingList"> The pricing list </param>
    /// <exception cref="ArgumentException"></exception>
    IList<Product> SetPricing(IList<Product> productPricingList);

    /// <summary> Scan a product code</summary>
    /// <returns> The quantity of the product</returns>
    /// <exception cref="ArgumentException"> Thrown when the code is null or empty </exception>
    /// <exception cref="InvalidOperationException"> Thrown when the code cannot be found </exception>
    int ScanProduct(string code);

    /// <summary>
    /// Remove 1 product in the cart
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentException"> Thrown when the code is null or empty </exception>
    /// <exception cref="InvalidOperationException">Thrown when the code cannot be found </exception>
    int DropProduct(string code);

    /// <returns> The cart's total price </returns>
    /// <exception cref="InvalidOperationException"> Thrown when the cart is empty </exception>
    decimal CalculateTotal();

    /// <summary> Clear the cart </summary>
    void ClearCart();
}