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
using AplikasiTog.UIServices;

namespace AplikasiTog.Services
{
    public class UsersService : Service<User>, IUsersInterface
    {
        public UsersService(IRepository<User> repository) : base(repository)
        {

        }

        public List<User> GetALL()
        {
            using (var unitOfWork = ServiceLocator.Current.GetInstance<IUnitOfWork>())
            {
                var users = unitOfWork.Repository<User>().List().ToList();
                return users;
            }
        }        

        public void UpdateCanSelect(List<User> entities)
        {
            using (var unitOfWork = ServiceLocator.Current.GetInstance<IUnitOfWork>())
            {
                var userRepo = unitOfWork.Repository<User>();
                List<int> usedIds = userRepo.List().Select(p => p.UserID).ToList();
                entities.ForEach(t =>
                {
                    t.IsSelectable = true;
                });
            }
        }

        
    }
}
