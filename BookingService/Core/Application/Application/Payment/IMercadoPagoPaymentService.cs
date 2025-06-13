
using Application.Payment.Dtos;
using Application.Payment.Responses;

namespace Application.Payment
{
    public interface IMercadoPagoPaymentService
    {
        Task<PaymentResponse> PayWithCreditCart(string paymentIntention);
        Task<PaymentResponse> PayWithDebitCard(string paymentIntention);
        Task<PaymentResponse> PayBankTransfer(string paymentIntention);
    }
}
