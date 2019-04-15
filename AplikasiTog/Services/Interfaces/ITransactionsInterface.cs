﻿using AplikasiTog.DAL.Models;
using GenericCodes.Core.Services;
using System.Collections.Generic;

namespace AplikasiTog.Services.Interfaces
{
    public interface ITransactionsInterface : IService<Transaction>
    {
        void UpdateCanSelect(List<Transaction> entities);
        List<Transaction> GetALL();

    }
}
