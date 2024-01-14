using MyFridge.MVVM.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFridge
{
    public class UPCLogic
    {
        public async static Task<List<Item>> GetItemByUPC(string upc)
        {
            List<Item> itemList = new List<Item>();

            try
            {
                var url = string.Format(Constants.UPC_LOOKUP, upc);

                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var upcItemResponse = JsonConvert.DeserializeObject<Example>(json);

                        if (upcItemResponse != null && upcItemResponse.items != null)
                        {
                            itemList = upcItemResponse.items;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }

            return itemList;
        }


    }
}
