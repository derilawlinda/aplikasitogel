﻿using AplikasiTog.DAL.Models;
using GenericCodes.CRUD.WPF.ViewModel.CRUDBases;
using AplikasiTog.Services.Interfaces;

namespace AplikasiTog.ViewModels.Users
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
