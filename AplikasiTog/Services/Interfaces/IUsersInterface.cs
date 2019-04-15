﻿using AplikasiTog.DAL.Models;
using GenericCodes.Core.Services;
using System.Collections.Generic;

namespace AplikasiTog.Services.Interfaces
{
    public interface IUsersInterface : IService<User>
    {
        void UpdateCanSelect(List<User> entities);
        List<User> GetALL();
    }
}
