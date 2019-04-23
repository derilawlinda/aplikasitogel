using AplikasiTog.DAL.Models;
using AplikasiTog.Services.Interfaces;
using AplikasiTog.UIServices;
using GenericCodes.CRUD.WPF.Core.MVVM;
using GenericCodes.CRUD.WPF.ViewModel.CRUDBases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplikasiTog.ViewModels.Settings
{
    public class SettingViewModel : GenericCrudBase<Setting>, INotifyPropertyChanged
    {
        private readonly ISettingsInterface _settingService;
        public SettingViewModel(SettingSearchViewModel settingSearch, ISettingsInterface settingService,
                                  SettingAddEditViewModel settingnAddEditViewModel)
            : base(settingSearch, settingnAddEditViewModel)
        {
            PostDataRetrievalDelegate = (list) =>
            {
                settingService.UpdateCanSelect(list);

            };
            _settingService = settingService;
            Dictionary<string, string> settingKeyValuePair = _settingService.GetSettingKeyValuePairs();

        }
        private Dictionary<string, string> _SettingDictionary;

        private Dictionary<string, string> SettingDictionary
        {
            get
            {
                return _settingService.GetSettingKeyValuePairs();
            }
        }



        private string _Setting2NomorWinning;
        public string Setting2NomorWinning
        {
            get
            {


                if (_Setting2NomorWinning == null)
                {
                    _Setting2NomorWinning = SettingDictionary["Winning2Nomor"];
                }
                return _Setting2NomorWinning;
            }
            set
            {
                this._Setting2NomorWinning = value;
                this.RaisePropertyChanged("Setting2NomorWinning");
            }

        }

        private string _Setting3NomorWinning;
        public string Setting3NomorWinning
        {
            get
            {


                if (_Setting3NomorWinning == null)
                {
                    _Setting3NomorWinning = SettingDictionary["Winning3Nomor"];
                }
                return _Setting3NomorWinning;
            }
            set
            {
                this._Setting3NomorWinning = value;
                this.RaisePropertyChanged("Setting3NomorWinning");
            }

        }

        private string _Setting4NomorWinning;
        public string Setting4NomorWinning
        {
            get
            {


                if (_Setting4NomorWinning == null)
                {
                    _Setting4NomorWinning = SettingDictionary["Winning4Nomor"];
                }
                return _Setting4NomorWinning;
            }
            set
            {
                this._Setting4NomorWinning = value;
                this.RaisePropertyChanged("Setting4NomorWinning");
            }

        }

        private RelayCommand _saveSettingCommand;
        DialogService dialog = new DialogService();
        public RelayCommand SaveSettingCommand
        {
            get
            {
                return _saveSettingCommand
                    ?? (_saveSettingCommand = new RelayCommand(
                    () =>
                    {
                        try
                        {
                            _settingService.UpdateSettings(Setting2NomorWinning, Setting3NomorWinning, Setting4NomorWinning);
                            dialog.ShowOKDialog("Info", "Setting tersimpan");
                        }
                        catch (Exception ex)
                        {
                            dialog.ShowErrorDialog("Error", ex.Message.ToString());
                        }
                    }));
            }

        }
    }
}
