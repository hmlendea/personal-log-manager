using PersonalLogManager.Api.Models;

namespace PersonalLogManager.Service
{
    public interface IPersonalLogService
    {
        void StorePersonalLog(StoreLogRequest request);

        GetLogResponse GetPersonalLogs(GetLogRequest request);

        void UpdatePersonalLog(UpdateLogRequest request);

        void DeletePersonalLog(DeleteLogRequest request);
    }
}
