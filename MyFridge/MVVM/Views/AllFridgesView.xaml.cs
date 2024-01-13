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

        
        if (int.TryParse(result, out int quantity))
        {            
            _fridgeViewModel.SelectedDrink.quantity -= quantity;

            if (_fridgeViewModel.ChangeDrinkQuantityCommand.CanExecute(null))
            {
                _fridgeViewModel.ChangeDrinkQuantityCommand.Execute(null);
            }
        }
        else
        {
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