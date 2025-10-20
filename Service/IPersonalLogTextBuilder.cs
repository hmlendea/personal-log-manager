using PersonalLogManager.Service.Models;

namespace PersonalLogManager.Service
{
    public interface IPersonalLogTextBuilder
    {
        string BuildLogText(PersonalLog log);
    }
}
