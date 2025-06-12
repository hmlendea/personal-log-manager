using PersonalLogManager.Api.Models;

namespace PersonalLogManager.Service
{
    public interface IPersonalLogService
    {
        void StoreTextLog(StoreTextLogRequest request);
    }
}
