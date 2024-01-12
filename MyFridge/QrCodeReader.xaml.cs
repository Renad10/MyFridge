
using MyFridge.MVVM.ViewModels;
using MyFridge.MVVM.Views;

namespace MyFridge;

public partial class QrCodeReader : ContentPage
{
	public QrCodeReader()
	{
		InitializeComponent();
		barcodeReader.Options = new ZXing.Net.Maui.BarcodeReaderOptions
		{
			Formats = ZXing.Net.Maui.BarcodeFormat.Ean13,
			AutoRotate = true,
			Multiple = true,	
		};

		
	}
    private bool isProcessingBarcode = false;

    private async void barcodeReader_BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
    {
        if (isProcessingBarcode || e.Results == null || !e.Results.Any())
            return;

        isProcessingBarcode = true;

        var first = e.Results.First();

        var itemsTask = UPCLogic.GetItemByUPC(first.Value.ToString());
        var timeoutTask = Task.Delay(5000); // Timeout van 5 seconden, pas dit aan indien nodig

        var completedTask = await Task.WhenAny(itemsTask, timeoutTask);

        if (completedTask == timeoutTask)
        {
            // Handle timeout, e.g., show a message or take appropriate action
            isProcessingBarcode = false; // Reset de boolean als er een timeout is opgetreden
            return;
        }

        var items = itemsTask.Result;

        if (items == null || items.Count == 0)
        {
            isProcessingBarcode = false; // Reset de boolean als er geen items zijn gevonden
            return;
        }

        Dispatcher.DispatchAsync(async () =>
        {
            ItemViewModel viewModel = new ItemViewModel
            {
                Title = items[0].title,
                Description = items[0].description,
                ImageUrl = items[0].images[0].Replace("http://", "https://")
            };

            ItemView itemView = new ItemView(viewModel);

            await Navigation.PushAsync(itemView);

            isProcessingBarcode = false; // Reset de boolean nadat de view is gepusht
        });
    }


    //private bool barcodeProcessed = false;

    //private async void barcodeReader_BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
    //{
    //    if (barcodeProcessed || e.Results == null || !e.Results.Any())
    //        return;

    //    barcodeProcessed = true;

    //    var first = e.Results.First();

    //    var itemsTask = UPCLogic.GetItemByUPC(first.Value.ToString());
    //    var timeoutTask = Task.Delay(5000); 

    //    var completedTask = await Task.WhenAny(itemsTask, timeoutTask);

    //    if (completedTask == timeoutTask)
    //    {
    //        barcodeProcessed = false; 
    //        return;
    //    }

    //    var items = itemsTask.Result;

    //    if (items == null || items.Count == 0)
    //    {
    //        barcodeProcessed = false; 
    //        return;
    //    }

    //    Dispatcher.DispatchAsync(async () =>
    //    {
    //        ItemViewModel viewModel = new ItemViewModel
    //        {
    //            Title = items[0].title,
    //            Description = items[0].description,
    //            ImageUrl = items[0].images[0].Replace("http://", "https://")
    //        };

    //        ItemView itemView = new ItemView(viewModel);

    //        await Navigation.PushAsync(itemView);


    //        barcodeProcessed = false;
    //    });
    //}



}