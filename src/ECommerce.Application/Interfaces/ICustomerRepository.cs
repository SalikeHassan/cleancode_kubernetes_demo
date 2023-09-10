using System.Threading;
using System.Threading.Tasks;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces
{
    public interface ICustomerRepository
    {
        Task<int> CreateCustomerAsync(Customer customer, CancellationToken cancellationToken);
    }
}