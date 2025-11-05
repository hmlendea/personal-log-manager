using PersonalLogManager.Service.Models;

namespace PersonalLogManager.Service.TextBuilding
{
    public interface IPersonalLogTextBuilderFactory
    {
        string BuildLogText(PersonalLog log, string localisation);
    }
}
