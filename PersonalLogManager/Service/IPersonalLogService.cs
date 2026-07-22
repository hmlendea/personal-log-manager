using PersonalLogManager.Api.Models;

namespace PersonalLogManager.Service
{
    public interface IPersonalLogService
    {
        void StorePersonalLog(StoreLogRequest request);

        GetLogResponse GetPersonalLogs(GetLogRequest request);

        GetLogByIdResponse GetPersonalLog(string id);

        void UpdatePersonalLog(UpdateLogRequest request);

        void DeletePersonalLog(DeleteLogRequest request);
    }
}
