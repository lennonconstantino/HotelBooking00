using Application;
using Application.MercadoPago.Exceptions;
using Application.Payment;
using Application.Payment.Dtos;
using Application.Payment.Responses;
using Domain.Guest.Enums;
using Status = Application.Payment.Dtos.Status;

namespace Payment.Application.MercadoPago
{
    public class MercadoPagoAdapter : IMercadoPagoPaymentService
    {
        public Task<PaymentResponse> PayBankTransfer(string paymentIntention)
        {
            throw new NotImplementedException();
        }

        public Task<PaymentResponse> PayWithCreditCart(string paymentIntention)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(paymentIntention))
                {
                    throw new InvalidPaymentIntetionException();
                }

                paymentIntention += "/success";

                var dto = new PaymentStateDto
                {
                    CreateData = DateTime.Now,
                    Message = $"Successfully paid {paymentIntention}",
                    PaymentId = "1234",
                    Status = Status.Success,
                };

                var response = new PaymentResponse
                {
                    Data = dto,
                    Success = true,
                    Message = "Payment successfully processed"
                };
                return Task.FromResult(response);
            }
            catch (InvalidPaymentIntetionException)
            {
                var resp = new PaymentResponse
                {
                    Success = false,
                    ErrorCodes = ErrorCodes.PAYMENT_INVALID_PAYMENT_INTENTION,
                };
                return Task.FromResult(resp);
            } 

        }

        public Task<PaymentResponse> PayWithDebitCard(string paymentIntention)
        {
            throw new NotImplementedException();
        }
    }
}
