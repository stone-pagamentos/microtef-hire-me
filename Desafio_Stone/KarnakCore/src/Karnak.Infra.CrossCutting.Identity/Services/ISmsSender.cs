using System.Threading.Tasks;

namespace Karnak.Infra.CrossCutting.Identity.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
