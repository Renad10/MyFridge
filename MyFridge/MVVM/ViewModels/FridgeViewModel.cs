using MyFridge.MVVM.Models;
using MyFridge.MVVM.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyFridge.MVVM.ViewModels
{
    public class FridgeViewModel : INotifyPropertyChanged
    {
        private List<Fridge> _fridges;
        private string _fridgeName;
        private Fridge _currentFridge;
        private Drink _selectedDrink;

        public ICommand AddOrUpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand DeleteSelectedDrinkCommand { get; set; }
        public ICommand ChangeDrinkQuantityCommand { get; set; }

        public List<Fridge> Fridges

        {
            get { return _fridges; }
            set
            {
                if (_fridges != value)
                {
                    _fridges = value;
                    OnPropertyChanged(nameof(Fridges));
                }
            }
        }
        public string FridgeName
        {
            get { return _fridgeName; }
            set
            {
                if (_fridgeName != value)
                {
                    _fridgeName = value;
                    OnPropertyChanged(nameof(FridgeName));
                }
            }
        }

        public Fridge CurrentFridge
        {
            get { return _currentFridge; }
            set
            {
                if (_currentFridge != value)
                {
                    _currentFridge = value;
                    OnPropertyChanged(nameof(CurrentFridge));
                }
            }
        }
        public Drink SelectedDrink
        {
            get { return _selectedDrink; }
            set
            {
                if (_selectedDrink != value)
                {
                    _selectedDrink = value;
                    OnPropertyChanged(nameof(SelectedDrink));
                }
            }
        }

        public FridgeViewModel()
        {
            Refresh();

            AddOrUpdateCommand = new Command(async () =>
            {
                var fridge = new Fridge
                {
                    name = FridgeName,
                };
                App.FridgeRepo.SaveEntity(fridge);
                Console.WriteLine(App.DrinkRepo.StatusMessage);

                Refresh();
            });

            DeleteCommand = new Command(() =>
            {
                App.FridgeRepo.DeleteEntityWithChildren(CurrentFridge);
                Refresh();
            });

            ChangeDrinkQuantityCommand = new Command(() =>
            {
                if (CurrentFridge != null)
                {
                    
                    App.DrinkRepo.SaveEntity(SelectedDrink);
                    Refresh();
                }
            });
            DeleteSelectedDrinkCommand = new Command(() =>
            {
                App.DrinkRepo.DeleteEntityWithChildren(SelectedDrink);
                Refresh();
            });
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void Refresh()
        {
            Fridges = App.FridgeRepo.GetEntitiesWithChildren();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
