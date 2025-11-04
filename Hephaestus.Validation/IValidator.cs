using System.Threading;
using System.Threading.Tasks;

namespace Hephaestus.Validation
{
    public interface IValidator
    {
        ValueTask<bool> ValidateAsync<T>(T value, CancellationToken cancellationToken = default);
    }
}
