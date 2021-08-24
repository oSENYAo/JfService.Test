using JFService.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFService.Shared.Abstract
{
    public interface IPaymentRepository
    {
        Task<Payment> GetPaymentByaccId(int accId);
        Task<Payment> GetPaymentById(int id);
        Task<Payment> FirstPaymentOrderBy(int accId);
        Task<Payment> FirstPaymentOrderByDescending(int accId);
    }
}
