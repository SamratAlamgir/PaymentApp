using Microsoft.EntityFrameworkCore;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        public PaymentRepository(PaymentDataContext dbContext): base(dbContext)
        {

        }

        public Task<Payment> GetPaymentById(Guid paymentId)
        {
            return GetAll().SingleOrDefaultAsync(x => x.PaymentId == paymentId);
        }
    }
}
