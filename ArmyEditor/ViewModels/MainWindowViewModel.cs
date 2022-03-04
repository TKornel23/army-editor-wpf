using ArmyEditor.Logic;
using ArmyEditor.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ArmyEditor.ViewModels
{
    public class MainWindowViewModel : ObservableRecipient
    {
        public ObservableCollection<Trooper> Barrack { get; set; }
        public ObservableCollection<Trooper> Army { get; set; }
        IArmyLogic logic;
        private Trooper selectedFromBarrack;

        public Trooper SelectedFromBarrack
        {
            get { return selectedFromBarrack; }
            set
            {
                SetProperty(ref selectedFromBarrack, value);
                (AddToArmyCommand as RelayCommand).NotifyCanExecuteChanged();
                (EditTrooperCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }

        private Trooper selectedFromArmy;

        public Trooper SelectedFromArmy
        {
            get { return selectedFromArmy; }
            set
            {
                SetProperty(ref selectedFromArmy, value);
                (RemoveFromArmyCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }

        public double AVGPower
        {
            get { return logic.AVGPower; }
        }

        public double AVGSpeed
        {
            get { return logic.AVGSpeed; }
        }

        public int AllCost
        {
            get { return logic.AllCost; }
        }

        public ICommand AddToArmyCommand { get; set; }
        public ICommand RemoveFromArmyCommand { get; set; }
        public ICommand EditTrooperCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public MainWindowViewModel()
            : this(IsInDesignMode ? null : Ioc.Default.GetService<IArmyLogic>())
        {

        }


        public MainWindowViewModel(IArmyLogic armyLogic)
        {
            this.logic = armyLogic;
            Barrack = new ObservableCollection<Trooper>();
            Barrack.Add(new Trooper()
            {
                Type = "marine",
                Power = 5,
                Speed = 8
            });
            Barrack.Add(new Trooper()
            {
                Type = "sniper",
                Power = 10,
                Speed = 3
            });
            Barrack.Add(new Trooper()
            {
                Type = "engineer",
                Power = 3,
                Speed = 9
            });
            Barrack.Add(new Trooper()
            {
                Type = "infantry",
                Power = 7,
                Speed = 8
            });
            Barrack.Add(new Trooper()
            {
                Type = "pilot",
                Power = 6,
                Speed = 3
            });

            Army = new ObservableCollection<Trooper>();
            Army.Add(Barrack[0].GetCopy());
            Army.Add(Barrack[1].GetCopy());

            logic.SetupCollections(Army, Barrack);

            AddToArmyCommand = new RelayCommand(
                () => logic.AddToArmy(SelectedFromBarrack),
                () => SelectedFromBarrack != null
            );

            RemoveFromArmyCommand = new RelayCommand(
                () => logic.RemoveFromArmy(SelectedFromArmy),
                () => SelectedFromArmy != null
            );

            EditTrooperCommand = new RelayCommand(
                () => logic.EditTrooper(SelectedFromBarrack),
                () => SelectedFromBarrack != null
            );

            Messenger.Register<MainWindowViewModel, string, string>(this, "TrooperInfo", (sender, args) =>
            {
                OnPropertyChanged("AVGSpeed");
                OnPropertyChanged("AVGPower");
                OnPropertyChanged("AllCost");
            });
        }
    }
}
