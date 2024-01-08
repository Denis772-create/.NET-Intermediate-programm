using Facade._Task.Entities;

namespace Facade._Task.Interfaces;

interface IInvoiceSystem
{
    void SendInvoice(Invoice invoice);
}