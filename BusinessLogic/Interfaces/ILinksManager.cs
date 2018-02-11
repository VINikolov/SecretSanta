using System.Threading.Tasks;
using Models.DataTransferModels;

namespace BusinessLogic.Interfaces
{
    public interface ILinksManager
    {
        Task StartLinkingProcess(string groupName, string username);
        Task<Link> GetReceiver(string username, string groupName);
    }
}
