using Apel.DAL.Models;
using GenericCodes.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apel.Services.Interfaces
{
    public interface ISettingsInterface : IService<Setting>
    {
        void UpdateCanSelect(List<Setting> entities);
        List<Setting> GetALL();

        Dictionary<string, string> GetSettingKeyValuePairs();

        void UpdateSettings(string bettingThreshold2A, string bettingThreshold3A, string bettingThreshold4A);

    }
}
