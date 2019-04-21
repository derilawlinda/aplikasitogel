using AplikasiTog.DAL.Models;
using GenericCodes.Core.Services;
using System.Collections.Generic;

namespace AplikasiTog.Services.Interfaces
{
    public interface INomorsInterface : IService<Nomor>
    {
        void UpdateCanSelect(List<Nomor> entities);
        List<Nomor> GetALL();
    }
}
