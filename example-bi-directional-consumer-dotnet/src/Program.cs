using System;
using System.Threading.Tasks;
using Provider.Models;
using Newtonsoft.Json;

namespace Consumer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var baseUri = "http://localhost:9099/";

            Console.WriteLine("Fetching products from: " + baseUri);
            var consumer = new ProductClient();
            
            try
            {
                var result = await consumer.GetProducts(baseUri);
                Console.WriteLine("✅ All Products:");
                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));

                if (result.Count > 0)
                {
                    var firstProduct = result[0];
                    Console.WriteLine($"\n✅ Getting product with ID {firstProduct.id}:");
                    Product productResult = await consumer.GetProduct(baseUri, firstProduct.id);
                    Console.WriteLine(JsonConvert.SerializeObject(productResult, Formatting.Indented));
                }

                // Test error case
                Console.WriteLine("\n🧪 Testing product ID 999 (should fail):");
                try
                {
                    Product errorTest = await consumer.GetProduct(baseUri, 999);
                    Console.WriteLine("❌ Unexpected success!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"✅ Expected error: {ex.Message}");
                }
                
                Console.WriteLine("\n🎉 Consumer test completed!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
        }

        static private void WriteoutArgsUsed(string datetimeArg, string baseUriArg)
        {
            Console.WriteLine($"Running consumer with args: dateTimeToValidate = {datetimeArg}, baseUri = {baseUriArg}");
        }

        static private void WriteoutUsageInstructions()
        {
            Console.WriteLine("To use with your own parameters:");
            Console.WriteLine("Usage: dotnet run ");
            Console.WriteLine("Usage Example: dotnet run 01/01/2018 http://localhost:9000");
        }
    }
}