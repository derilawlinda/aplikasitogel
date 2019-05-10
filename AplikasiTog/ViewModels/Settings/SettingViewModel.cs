using Apel.DAL.Models;
using Apel.Services.Interfaces;
using Apel.UIServices;
using GenericCodes.CRUD.WPF.Core.MVVM;
using GenericCodes.CRUD.WPF.ViewModel.CRUDBases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apel.ViewModels.Settings
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

        private double _bettingThreshold2A;
        public double BettingThreshold2A
        {
            get
            {


                if (_bettingThreshold2A == 0)
                {
                    _bettingThreshold2A = Convert.ToDouble(SettingDictionary["BettingThreshold2A"]);
                }
                return _bettingThreshold2A;
            }
            set
            {
                this._bettingThreshold2A = value;
                this.RaisePropertyChanged("BettingThreshold2A");
            }

        }

        private double _bettingThreshold3A;
        public double BettingThreshold3A
        {
            get
            {


                if (_bettingThreshold3A == 0)
                {
                    _bettingThreshold3A = Convert.ToDouble(SettingDictionary["BettingThreshold3A"]);
                }
                return _bettingThreshold3A;
            }
            set
            {
                this._bettingThreshold3A = value;
                this.RaisePropertyChanged("BettingThreshold3A");
            }

        }

        private double _bettingThreshold4A;
        public double BettingThreshold4A
        {
            get
            {


                if (_bettingThreshold4A == 0)
                {
                    _bettingThreshold4A = Convert.ToDouble(SettingDictionary["BettingThreshold4A"]);
                }
                return _bettingThreshold4A;
            }
            set
            {
                this._bettingThreshold4A = value;
                this.RaisePropertyChanged("BettingThreshold4A");
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
                            _settingService.UpdateSettings(BettingThreshold2A.ToString(), BettingThreshold3A.ToString(), BettingThreshold4A.ToString());
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
