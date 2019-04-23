using AplikasiTog.DAL.Models;
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
    public class NomorsService : Service<Nomor>, INomorsInterface
    {
        TogelContext togelContext = new TogelContext();
        public NomorsService(IRepository<Nomor> repository) : base(repository)
        {

        }
        public List<Nomor> GetALL()
        {
            using (var unitOfWork = ServiceLocator.Current.GetInstance<IUnitOfWork>())
            {
                var nomors = unitOfWork.Repository<Nomor>().List().ToList();
                return nomors;
            }
        }

        public void UpdateCanSelect(List<Nomor> entities)
        {
            using (var unitOfWork = ServiceLocator.Current.GetInstance<IUnitOfWork>())
            {
                var nomorRepo = unitOfWork.Repository<Nomor>();
                List<int> usedIds = nomorRepo.List().Select(p => p.NomorID).ToList();
                entities.ForEach(t =>
                {
                    t.IsSelectable = true;
                });
            }
        }
        public Nomor GetTodayNomor()
        {
            Nomor lastNomor = togelContext.Nomors.ToList().Where(n => n.Date.Date == DateTime.Now.Date).FirstOrDefault();
            return lastNomor;
        }
        public void InserTodayNumber(Nomor nomor)
        {
            togelContext.Nomors.Add(nomor);
            togelContext.SaveChanges();
        }

        public bool IsTodayNumberExist()
        {
            var todayNumber = togelContext.Nomors.ToList().Where(n => n.Date.Date == DateTime.Now.Date).FirstOrDefault();
            if(todayNumber != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void UpdateTodayNumber(int winningNomor)
        {
            var todayNumber = togelContext.Nomors.ToList().Where(n => n.Date.Date == DateTime.Now.Date).FirstOrDefault();
            todayNumber.WinningNomor = winningNomor;
            togelContext.SaveChanges();


        }
    }

}
