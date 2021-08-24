using System.Collections.Generic;
using System.Threading.Tasks;

namespace JFService.Shared
{
    public interface ICalculate<Y, M, Q>
    {
        Task<List<Y>> Years(int accId);
        Task<List<M>> Monts(int accId);
        Task<List<Q>> Quarters(int accId);

    }
}
