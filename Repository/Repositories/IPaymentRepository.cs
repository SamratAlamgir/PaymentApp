using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        Task<Payment> GetPaymentById(Guid paymentId);
    }
}
