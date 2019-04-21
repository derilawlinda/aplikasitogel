﻿using AplikasiTog.DAL.Models;
using AplikasiTog.Services.Interfaces;
using GenericCodes.Core.Repositories;
using GenericCodes.Core.Services;
using GenericCodes.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.ServiceLocation;
using AplikasiTog.DAL;


namespace AplikasiTog.Services
{
    public class TransactionsService : Service<Transaction>, ITransactionsInterface
    {
        public TransactionsService(IRepository<Transaction> repository) : base(repository)
        {

        }
        TogelContext togelContext = new TogelContext();
        public List<Transaction> GetALL()
        {
            using (var unitOfWork = ServiceLocator.Current.GetInstance<IUnitOfWork>())
            {
                var transactions = unitOfWork.Repository<Transaction>().List().ToList();
                return transactions;
            }
        }

        public void UpdateCanSelect(List<Transaction> entities)
        {
            using (var unitOfWork = ServiceLocator.Current.GetInstance<IUnitOfWork>())
            {
                var userRepo = unitOfWork.Repository<Transaction>();
                List<int> usedIds = userRepo.List().Select(p => p.TransactionID).ToList();
                if(usedIds.Count > 0)
                {
                    entities.ForEach(t =>
                    {
                        t.IsSelectable = true;
                    });
                }
                
            }
        }

        public void InsertTransaction(Transaction entity)
        {
            
            togelContext.Transactions.Add(entity);
            togelContext.SaveChanges();
        }

        public void InsertTransactionList(List<Transaction> entities)
        {
            foreach (var entity in entities)
            {
                togelContext.Transactions.Add(entity);
            }
            togelContext.SaveChanges();
        }

    }
}
