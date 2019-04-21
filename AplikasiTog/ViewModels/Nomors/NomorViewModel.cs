using AplikasiTog.DAL.Models;
using AplikasiTog.Services.Interfaces;
using GenericCodes.CRUD.WPF.ViewModel.CRUDBases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplikasiTog.ViewModels.Nomors
{
    public class NomorViewModel : GenericCrudBase<Nomor>, INotifyPropertyChanged
    {
        private readonly INomorsInterface _nomorService;
        public NomorViewModel(NomorSearchViewModel nomorSearch, INomorsInterface nomorService,
                                  NomorAddEditViewModel nomorAddEditViewModel)
           : base(nomorSearch, nomorAddEditViewModel)
        {
            _nomorService = nomorService;
        }
    }
}
