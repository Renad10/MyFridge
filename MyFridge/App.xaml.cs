using MyFridge.MVVM.Models;
using MyFridge.Repositories;

namespace MyFridge
{
    public partial class App : Application
    {
        public static BaseRepository<Drink>? DrinkRepo { get; private set; }
        public static BaseRepository<Fridge>? FridgeRepo { get; private set; }
        public App(BaseRepository<Drink> drinkRepo, BaseRepository<Fridge> fridgeRepo)
        {
            InitializeComponent();
            DrinkRepo = drinkRepo;  
            FridgeRepo = fridgeRepo;
            MainPage = new AppShell();
        }
    }
}