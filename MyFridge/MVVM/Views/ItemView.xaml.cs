using MyFridge.MVVM.ViewModels;


namespace MyFridge.MVVM.Views;

public partial class ItemView : ContentPage
{ 
	public ItemView(ItemViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}