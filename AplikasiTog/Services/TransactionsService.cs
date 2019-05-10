using Apel.DAL.Models;
using Apel.Services.Interfaces;
using GenericCodes.Core.Repositories;
using GenericCodes.Core.Services;
using GenericCodes.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.ServiceLocation;
using Apel.DAL;
using System.Collections.ObjectModel;

namespace Apel.Services
{
    public class TransactionsService : Service<Transaction>, ITransactionsInterface
    {

        private readonly INomorsInterface _nomorService;
        private readonly ISettingsInterface _settingService;
        public TransactionsService(IRepository<Transaction> repository, INomorsInterface nomorService, ISettingsInterface settingService) : base(repository)
        {
            _nomorService = nomorService;
            _settingService = settingService;


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
        public List<TodayTransaction> GetTodayTransactions()
        {
            List<TodayTransaction> todayTransaction = new List<TodayTransaction>();
            var todayQuery = togelContext.Transactions.Where(t => t.Date.Year == DateTime.Today.Year &&
            t.Date.Month == DateTime.Today.Month && t.Date.Day == DateTime.Today.Day
            );

            if(todayQuery.Count() > 0)
            {
                todayTransaction = todayQuery
               .Select(t => new TodayTransaction
               {
                   UserName = t.User.Name,
                   BetAmount = t.BetAmount,
                   BetNumber = t.BetNumber,
                   Date = t.Date

               }).ToList();
            }
            

            return todayTransaction;
        }
        double bettingThreshold;

        public double GetBettingThreshold(int betNumber)
        {
            var bettingThreshold2A = _settingService.GetSettingKeyValuePairs()["BettingThreshold2A"];
            var bettingThreshold3A = _settingService.GetSettingKeyValuePairs()["BettingThreshold3A"];
            var bettingThreshold4A = _settingService.GetSettingKeyValuePairs()["BettingThreshold4A"];
            if (betNumber.ToString().Length == 2)
            {
                bettingThreshold = Convert.ToDouble(bettingThreshold2A);
            }

            if (betNumber.ToString().Length == 3)
            {
                bettingThreshold = Convert.ToDouble(bettingThreshold3A);
            }

            if (betNumber.ToString().Length == 4)
            {
                bettingThreshold = Convert.ToDouble(bettingThreshold4A);
            }

            return bettingThreshold;

        }

        public List<BetRecap> GetAggregateTodayTransactions()
        {
            List<BetRecap> todayAggregateTransaction = new List<BetRecap>();
            var queryCount = togelContext.Transactions.Where(t => t.Date.Year == DateTime.Today.Year &&
            t.Date.Month == DateTime.Today.Month && t.Date.Day == DateTime.Today.Day).Count();
            

            if (queryCount > 0)
            {
                todayAggregateTransaction = togelContext.Transactions.Where(t => t.Date.Year == DateTime.Today.Year &&
            t.Date.Month == DateTime.Today.Month && t.Date.Day == DateTime.Today.Day)
               .GroupBy(t => t.BetNumber)

                .Select(t => new BetRecap
                {
                    TotalBetAmount = t.Sum(tr => tr.BetAmount),
                    BetNumber = t.FirstOrDefault().BetNumber,
                    

                }).ToList();
                foreach (var tat in todayAggregateTransaction)
                {
                    tat.BettingThreshold = GetBettingThreshold(tat.BetNumber);
                    tat.BetRecapDetails = togelContext.Transactions.Where(tr => (tr.BetNumber == tat.BetNumber) && (tr.Date.Year == DateTime.Today.Year &&
                tr.Date.Month == DateTime.Today.Month && tr.Date.Day == DateTime.Today.Day)).Select(upt => new BetRecapDetail
                {
                    UserName = upt.User.Name,
                    BetAmount = upt.BetAmount,
                    BetNomor = upt.BetNumber
                }).ToList();
                }

            
                
            }
            return todayAggregateTransaction;
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
        

        public List<Winning> GetWinningAggregator()
        {

            List<Winning> todayWinningAggregators = new List<Winning>();
            if (_nomorService.GetTodayNomor() != null)
            {
                var todayWinningNomor = _nomorService.GetTodayNomor().WinningNomor;

                var winningNomor2Angka = Convert.ToInt32(todayWinningNomor.ToString().Substring(2, 2));
                var winningNomor3Angka = Convert.ToInt32(todayWinningNomor.ToString().Substring(1, 3));
                var winningNomor4Angka = Convert.ToInt32(todayWinningNomor);
              

                var userIds = togelContext.Transactions.Where(t => t.Date.Year == DateTime.Today.Year &&
                t.Date.Month == DateTime.Today.Month && t.Date.Day == DateTime.Today.Day).Select(t => t.UserID).Distinct().ToArray();

                
                foreach (var userId in userIds)
                {
                    Winning todayWinningAggregator = new Winning();
                    List<Transaction> todayTransactionByUsers = togelContext.Transactions.ToList().Where(t => t.Date.Date == DateTime.Today.Date && t.UserID == userId).ToList();
                    User user = togelContext.Users.Find(userId);
                    todayWinningAggregator.UserName = user.Name;
                    List<WinningDetail> winningDetails = new List<WinningDetail>();
                    foreach (var ttbu in todayTransactionByUsers)
                    {
                        WinningDetail winningDetail = new WinningDetail();
                        winningDetail.BetNumber = ttbu.BetNumber;
                        winningDetail.WinningMultiplier = 0;
                        winningDetail.BetAmount = ttbu.BetAmount;
                        if(ttbu.BetNumber.ToString().Length == 2)
                        {
                            winningDetail.Discount = user.Discount2A;
                            winningDetail.Winning = (ttbu.BetAmount * ((100 - user.Discount2A) * 0.01)) * -1;
                            

                            if (ttbu.BetNumber == winningNomor2Angka)
                            {
                                winningDetail.Winning = ttbu.BetAmount * user.Winning2A;
                                winningDetail.WinningMultiplier = user.Winning2A;
                                winningDetail.Discount = 0;
                            };
                        }

                        if (ttbu.BetNumber.ToString().Length == 3)
                        {
                            winningDetail.Discount = user.Discount3A;
                            winningDetail.Winning = (ttbu.BetAmount * ((100 - user.Discount3A) * 0.01)) * -1;

                            if (ttbu.BetNumber == winningNomor3Angka)
                            {
                                winningDetail.Winning = ttbu.BetAmount * user.Winning3A;
                                winningDetail.WinningMultiplier = user.Winning3A;
                                winningDetail.Discount = 0;
                            };
                        }

                        if (ttbu.BetNumber.ToString().Length == 4)
                        {
                            winningDetail.Discount = user.Discount4A;
                            winningDetail.Winning = (ttbu.BetAmount * ( (100 - user.Discount4A) * 0.01 )) * -1;

                            if (ttbu.BetNumber == winningNomor4Angka)
                            {
                                winningDetail.Winning = ttbu.BetAmount * user.Winning3A;
                                winningDetail.WinningMultiplier = user.Winning4A;
                                winningDetail.Discount = 0;
                            };
                        }                        

                        winningDetails.Add(winningDetail);
                    }
                    var asd = winningDetails;
                    var winningSum = winningDetails.Sum(wd => wd.Winning);
                    todayWinningAggregator.TotalWinning = winningSum;
                    todayWinningAggregator.WinningDetails = winningDetails;
                    todayWinningAggregators.Add(todayWinningAggregator);


                }
            }            
            return todayWinningAggregators;
        }

        

    }
}
