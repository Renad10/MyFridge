using MyFridge.MVVM.ViewModels;

namespace MyFridge.MVVM.Views;

public partial class AllFridgesView : ContentPage
{
    private readonly FridgeViewModel _fridgeViewModel;
    public AllFridgesView()
	{
		InitializeComponent();
        _fridgeViewModel = new FridgeViewModel();
        BindingContext = _fridgeViewModel;
    }



    private  async void TakeDrinkButton_Clicked(object sender, EventArgs e)
    {
        string result = await DisplayPromptAsync("Quantity" ,"Enter the quantity");

        // Controleer of de invoer geldig is voordat de commando wordt uitgevoerd
        if (int.TryParse(result, out int quantity))
        {
            // Pas de hoeveelheid aan in de viewmodel
            _fridgeViewModel.SelectedDrink.quantity -= quantity;

            // Roep de ICommand.Execute-methode aan
            if (_fridgeViewModel.ChangeDrinkQuantityCommand.CanExecute(null))
            {
                _fridgeViewModel.ChangeDrinkQuantityCommand.Execute(null);
            }
        }
        else
        {
            // Geef een foutmelding weer of voer andere logica uit voor ongeldige invoer
            if (result != null)
            {
                await DisplayAlert("Error", "Invalid quantity input", "OK");
            }      
        }
    }

    private void Test_Clicked(object sender, EventArgs e)
    {
        
        string text = (_fridgeViewModel.SelectedDrink.quantity - 1).ToString();
        DisplayAlert("Fout", text, "OK");
    }
}