using MyFridge.MVVM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace MyFridge.MVVM.ViewModels
{
    public class DrinkViewModel:INotifyPropertyChanged
    {
        private List<Drink> _drinks;
        private string _drinkName;
        private int _quantity;
        private int _fridgeId;
        private Fridge _selectedFridge;
        private Drink _currentDrink;
        private List<Fridge> _fridges;


        public ICommand AddOrUpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand CaptureDrinkPhotoCommand { get; set; }

        public List<Drink> Drinks
        {
            get { return _drinks; }
            set
            {
                if (_drinks != value)
                {
                    _drinks = value;
                    OnPropertyChanged(nameof(Drinks));
                }
            }
        }

        public string DrinkName
        {
            get { return _drinkName; }
            set
            {
                if (_drinkName != value)
                {
                    _drinkName = value;
                    OnPropertyChanged(nameof(DrinkName));
                }
            }
        }
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                if (_quantity != value)
                {
                    _quantity = value;
                    OnPropertyChanged(nameof(Quantity));
                }
            }
        }

        public int FridgeId
        {
            get { return _fridgeId; }
            set
            {
                if (_fridgeId != value)
                {
                    _fridgeId = value;
                    OnPropertyChanged(nameof(FridgeId));
                }
            }
        }

        public Fridge SelectedFridge
        {
            get { return _selectedFridge; }
            set
            {
                if (_selectedFridge != value)
                {
                    _selectedFridge = value;
                    OnPropertyChanged(nameof(SelectedFridge));
                }
            }
        }

        public Drink CurrentDrink
        {
            get { return _currentDrink; }
            set
            {
                if (_currentDrink != value)
                {
                    _currentDrink = value;
                    OnPropertyChanged(nameof(CurrentDrink));
                }
            }
        }

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



        public DrinkViewModel()
        {
            Refresh();
            GetAllFridges();

            AddOrUpdateCommand = new Command(async ()=>
            {
                var drink = new Drink()
                {
                    name = DrinkName,
                    FridgeId = SelectedFridge.Id,
                    quantity = Quantity,
                    drinkImage = CompleteDrinkPhotoPath,
                };

                App.DrinkRepo.SaveEntity(drink);

                Console.WriteLine(App.DrinkRepo.StatusMessage);
                Refresh();
                GetAllFridges();
            });

            DeleteCommand = new Command(() =>
            {
                App.DrinkRepo.DeleteEntityWithChildren(CurrentDrink);
                Refresh();
                GetAllFridges();
            });


            CaptureDrinkPhotoCommand = new Command(DoCaptureDrinkPhoto, () => MediaPicker.IsCaptureSupported);

        }

        private async void DoCaptureDrinkPhoto()
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();
                CompleteDrinkPhotoPath = await LoadPhotoAsync(photo);

                OnPropertyChanged(nameof(CompleteDrinkPhotoPath));
                OnPropertyChanged(nameof(HasPhoto));
               
                Console.WriteLine("Drink Pthot Captured" + CompleteDrinkPhotoPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        private string drinkPhotoPath;
        public string CompleteDrinkPhotoPath
        {
            get => drinkPhotoPath;
            set
            {
                if (drinkPhotoPath != value)
                {
                    drinkPhotoPath = value;
                    HasPhoto = !string.IsNullOrEmpty(value);
                    OnPropertyChanged(nameof(CompleteDrinkPhotoPath)); 
                }
            }
        }
        private bool _hasPhoto;
        public bool HasPhoto
        {
            get => _hasPhoto;
            set
            {
                if (_hasPhoto != value)
                {
                    _hasPhoto = value;
                    OnPropertyChanged(nameof(HasPhoto));
                }
            }
        }
        private void Refresh()
        {
            Drinks = App.DrinkRepo.GetEntitiesWithChildren();
            OnPropertyChanged(nameof(Drinks));
        }

        private void GetAllFridges()
        {
            Fridges = App.FridgeRepo.GetEntitiesWithChildren();
            OnPropertyChanged(nameof(Fridges));
        }

        public async Task<String> LoadPhotoAsync(FileResult photo)
        {
            var stream = photo.OpenReadAsync().Result;
            byte[] imagedata;

            using (MemoryStream ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                imagedata = ms.ToArray();
            }

            var folderpath = Path.Combine(FileSystem.AppDataDirectory, "DrinkPhoto");
            if (!File.Exists(folderpath))
            {
                Directory.CreateDirectory(folderpath);

            }

            var drinkFileName = Guid.NewGuid() + "_drinkPhoto.jpg";

            var newfile = Path.Combine(folderpath, drinkFileName); // The complete path of the photo

            using (var stream2 = new MemoryStream(imagedata))
            using (var newstream = File.OpenWrite(newfile))
            {
                await stream2.CopyToAsync(newstream);
            }
            return newfile;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
    }
}
