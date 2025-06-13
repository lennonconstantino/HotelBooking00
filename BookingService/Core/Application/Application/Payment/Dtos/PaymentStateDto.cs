using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Payment.Dtos
{
    public enum Status { 
        Success = 0,
        Failed,
        Error,
        Undefined,
    }
    public class PaymentStateDto
    {
        public Status Status { get; set; }
        public string PaymentId { get; set; }
        public DateTime CreateData { get; set; } = DateTime.Now;
        public string Message { get; set; }
    }
}
