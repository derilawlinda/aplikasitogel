using AplikasiTog.DAL.Models;
using AplikasiTog.Services.Interfaces;
using AplikasiTog.UIServices;
using GenericCodes.CRUD.WPF.Core.MVVM;
using GenericCodes.CRUD.WPF.ViewModel.CRUDBases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AplikasiTog.ViewModels.Nomors
{
    public class NomorViewModel : GenericCrudBase<Nomor>, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        bool nomorExist;
        #endregion

        void SetField<X>(ref X field, X value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<X>.Default.Equals(field, value)) return;

            field = value;

            var h = PropertyChanged;
            if (h != null) h(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly INomorsInterface _nomorService;

        public NomorViewModel(NomorSearchViewModel nomorSearch, INomorsInterface nomorService,
                                  NomorAddEditViewModel nomorAddEditViewModel)
           : base(nomorSearch, nomorAddEditViewModel)
        {
            _nomorService = nomorService;
        }

        private RelayCommand _submitNomorHariIni;

        public RelayCommand SubmitNomorHariIni
        {

            get
            {
               

                return _submitNomorHariIni
                    ?? (_submitNomorHariIni = new RelayCommand(
                    () =>
                    {
                                          

                        DialogService dialog = new DialogService();
                        nomorExist = _nomorService.IsTodayNumberExist();
                        if (nomorExist)
                        {
                            bool dialogResponse = dialog.ShowConfirmDialog("Info", "Sudah ada nomor hari ini, update nomor hari ini?");
                            if (dialogResponse)
                            {
                                _nomorService.UpdateTodayNumber(NomorHariIni);
                                dialog.ShowOKDialog("Info", "Nomor dirubah");
                            }
                            else
                            {
                                return;
                            }

                        }
                        else
                        {
                            try
                            {
                                Nomor nomor = new Nomor();
                                nomor.WinningNomor = NomorHariIni;
                                nomor.Date = DateTime.Now;
                                _nomorService.InserTodayNumber(nomor);
                                dialog.ShowOKDialog("Info", "Nomor hari ini terpasang.");

                            }
                            catch (Exception ex)
                            {
                                dialog.ShowErrorDialog("Error", ex.Message.ToString());
                            }
                        }
                    }));


            }
        }       

        private int _NomorHariIni;
        public int NomorHariIni
        {
            get
            {
                if(_NomorHariIni == 0 && _nomorService.GetTodayNomor() != null)
                {
                    _NomorHariIni = _nomorService.GetTodayNomor().WinningNomor;
                }
                return _NomorHariIni;
            }
            set {
                this._NomorHariIni = value;
                this.RaisePropertyChanged("NomorHariIni");
            }
                
        }

       
    }
}
