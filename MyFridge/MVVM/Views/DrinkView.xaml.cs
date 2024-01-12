using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using MyFridge.MVVM.ViewModels;
using Font = Microsoft.Maui.Font;

namespace MyFridge.MVVM.Views;


public partial class DrinkView : ContentPage
{
    private readonly DrinkViewModel drinkViewModel;    
	public DrinkView(DrinkViewModel drinkViewModel)
	{
		InitializeComponent();
        BindingContext = drinkViewModel; 
        this.drinkViewModel = drinkViewModel;
        
        
	}
    private void OnDrinkQuantityEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        if (!IsNumeric(e.NewTextValue))
        {
            DisplayAlert("Fout", "Voer alleen getallen in.", "OK");
            ((Entry)sender).Text = e.OldTextValue;
        }
    }

    private bool IsNumeric(string text)
    {
        return double.TryParse(text, out _);
    }

    private async void AddButton_Clicked(object sender, EventArgs e)
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        string text = "Drink is added succesfully";
        ToastDuration duration = ToastDuration.Short;
        double fontSize = 14;

        var toast = Toast.Make(text, duration, fontSize);
        
        await toast.Show(cancellationTokenSource.Token);    
    }

    private async void TestButton_Clicked(object sender, EventArgs e)
    {
        string result = await DisplayPromptAsync("Question 1", "What's your name?");

        drinkViewModel.CurrentDrink.quantity -= int.Parse(result);
    }
}