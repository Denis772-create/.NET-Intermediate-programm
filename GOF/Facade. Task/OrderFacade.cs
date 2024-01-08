using Facade._Task.Entities;
using Facade._Task.Interfaces;

namespace Facade._Task;

internal class OrderFacade
{
    private readonly IProductCatalog _productCatalog;
    private readonly IPaymentSystem _paymentSystem;
    private readonly IInvoiceSystem _invoiceSystem;

    public OrderFacade(IProductCatalog productCatalog, 
        IPaymentSystem paymentSystem, 
        IInvoiceSystem invoiceSystem)
    {
        _productCatalog = productCatalog;
        _paymentSystem = paymentSystem;
        _invoiceSystem = invoiceSystem;
    }

    public void PlaceOrder(string productId, int quantity, string email)
    {
        var product = _productCatalog.GetProductDetails(productId);

        var totalCost = product.Price * quantity;

        var payment = new Payment { Amount = totalCost };
        var paymentSuccess = _paymentSystem.MakePayment(payment);

        if (paymentSuccess)
        {
            var invoice = new Invoice
            {
                Details = $"Invoice for {quantity} units of {productId} to {email}"
            };
            _invoiceSystem.SendInvoice(invoice);
        }
        else
        {
            // handle payment failure
        }
    }
}