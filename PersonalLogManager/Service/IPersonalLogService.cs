using System.Collections.Generic;

using PersonalLogManager.Service.Models;

namespace PersonalLogManager.Service
{
    public interface IPersonalLogService
    {
        void StorePersonalLog(PersonalLogCreation creation);

        IEnumerable<string> GetPersonalLogs(PersonalLogFilter filter);

        void UpdatePersonalLog(PersonalLogUpdate update);

        void DeletePersonalLog(string id);
    }
}
