using PersonalLogManager.Api.Models;

namespace PersonalLogManager.Service
{
    public interface IPersonalLogService
    {
        GetLogResponse GetLogs(GetLogRequest request);

        void StorePersonalLog(StoreLogRequest request);
    }
}
