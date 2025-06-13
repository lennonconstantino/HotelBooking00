using Application.Payment.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Payment
{
    public interface IStripePaymentService
    {
        Task<PaymentStateDto> PayWithCreditCart(string paymentIntention);
        Task<PaymentStateDto> PayWithDebitCard(string paymentIntention);
        Task<PaymentStateDto> PayBankTransfer(string paymentIntention);
    }
}
