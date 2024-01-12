
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

    private async void barcodeReader_BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
    {
		var first = e.Results?.FirstOrDefault();
        
        var items = await UPCLogic.GetItemByUPC(first.Value.ToString());
        
        //var items = await ItemLogic.GetItemByBarcode(first.Value.ToString());

        if (first is null)
			return;
        Dispatcher.DispatchAsync(async () =>
		{
			await DisplayAlert("Barcode Detected", items[0].title, "Ok");
		});
    }
}