using System.Threading.Tasks;

namespace LH.CommandLine
{
    public interface IAsyncCommand<in TOptions>
    {
        Task Execute(TOptions options);
    }
}