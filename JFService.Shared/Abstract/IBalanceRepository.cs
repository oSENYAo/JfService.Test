using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JFService.Models.Models;

namespace JFService.Shared.Abstract
{
    public interface IBalanceRepository
    {
        Task<Balance> GetBalanceByaccId(int accId);
        Task<Balance> GetBalanceById(int id);
        Task<Balance> FirstOrderBy(int accId);
        Task<Balance> FirstOrderByDescending(int accId);
        Task UpdateDatabase();
    }
}
