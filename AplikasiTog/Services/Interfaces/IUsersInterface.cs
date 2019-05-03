using Apel.DAL.Models;
using GenericCodes.Core.Services;
using System.Collections.Generic;

namespace Apel.Services.Interfaces
{
    public interface IUsersInterface : IService<User>
    {
        void UpdateCanSelect(List<User> entities);
        List<User> GetALL();

    }
}
