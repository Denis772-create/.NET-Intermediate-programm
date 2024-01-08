using Facade._Task.Entities;

namespace Facade._Task.Interfaces;

interface IPaymentSystem
{
    bool MakePayment(Payment payment);
}