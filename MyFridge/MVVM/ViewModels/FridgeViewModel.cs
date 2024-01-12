﻿using MyFridge.MVVM.Models;
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
        //private List<Drink> _drinks;
        private string _fridgeName;
        private Fridge _currentFridge;
        private Drink _selectedDrink;

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

        //public List<Drink> Drinks
        //{
        //    get { return _drinks; }
        //    set
        //    {
        //        if (_drinks != value)
        //        {
        //            _drinks = value;
        //            OnPropertyChanged(nameof(Drinks));
        //        }
        //    }
        //}

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

        public ICommand AddOrUpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand ShowDetailsCommand { get; set; }

        public ICommand ChangeDrinkQuantityCommand { get; set; }

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

            //ShowDetailsCommand = new Command(() =>
            //{
            //    if (CurrentFridge != null)
            //    {
            //        ShowDetailsPage(CurrentFridge);
            //    }
            //});

            ChangeDrinkQuantityCommand = new Command(() =>
            {
                if (CurrentFridge != null)
                {
                    
                    App.DrinkRepo.SaveEntity(SelectedDrink);
                    Refresh();
                }
            });

        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Refresh()
        {
            Fridges = App.FridgeRepo.GetEntitiesWithChildren();
        }

        //private void ShowDetailsPage(Fridge fridge)
        //{
        //    Drinks = App.DrinkRepo.GetEntitiesWithChildren().Where(d => d.FridgeId == fridge.Id).ToList();
        //    OnPropertyChanged(nameof(Drinks));
        //}

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    //public class FridgeViewModel: INotifyPropertyChanged
    //{


    //    public List<Fridge>? Fridges { get; set; }
    //    public List<Drink>? Drinks { get; set; }
    //    public string FridgeName { get; set; }  
    //    public Fridge? CurrentFridge { get; set; }
    //    public ICommand? AddOrUpdateCommand { get; set; }
    //    public ICommand? DeleteCommand { get; set; }
    //    public ICommand? ShowDetailsCommand { get; set; }



    //    public FridgeViewModel()
    //    {
    //        Refresh();
    //        //GetAllFridges();

    //        AddOrUpdateCommand = new Command(async () =>
    //        {
    //            var fridge = new Fridge
    //            {
    //                name = FridgeName,
    //            };
    //            App.FridgeRepo.SaveEntity(fridge);
    //            Console.WriteLine(App.DrinkRepo.StatusMessage);

    //            Refresh();

    //        });

    //        DeleteCommand = new Command(() =>
    //        {
    //            App.FridgeRepo.DeleteEntityWithChildren(CurrentFridge);
    //            Refresh();

    //        });

    //        ShowDetailsCommand = new Command(() =>
    //        {
    //            // Controleer of er een geselecteerde koelkast is
    //            if (CurrentFridge != null)
    //            {
    //                // Roep de methode aan om de details te tonen
    //                ShowDetailsPage(CurrentFridge);
    //            }
    //        });


    //    }

    //    public event PropertyChangedEventHandler PropertyChanged;

    //    private void Refresh()
    //    {
    //        Fridges = App.FridgeRepo.GetEntitiesWithChildren();
    //    }



    //    private void ShowDetailsPage(Fridge fridge)
    //    {
    //        Drinks = App.DrinkRepo.GetEntitiesWithChildren().Where(d => d.FridgeId == fridge.Id).ToList();
    //    }

    //    //private void GetAllFridges()
    //    //{
    //    //    Fridges = App.FridgeRepo.GetEntitiesWithChildren();
    //    //}
    //}
}