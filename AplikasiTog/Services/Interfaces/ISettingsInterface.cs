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

        void UpdateSettings(string winning2Nomor, string winning3Nomor, string winning4Nomor, string bettingThreshold);

    }
}
