using Apel.DAL.Models;
using GenericCodes.CRUD.WPF.ViewModel.CRUDBases;
using Apel.Services.Interfaces;
using Apel.DAL;
using System.Linq;

namespace Apel.ViewModels.Users
{
    public class UserViewModel : GenericCrudBase<User>
    {
        public UserViewModel(UserSearchViewModel userSearch, IUsersInterface userService,
                                  UserAddEditViewModel userAddEdit)
            : base(userSearch, userAddEdit)
        {
            PostDataRetrievalDelegate = (list) =>
            {
                userService.UpdateCanSelect(list);
            };

        }


    }
}
