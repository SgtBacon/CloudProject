using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json; 


class Program
{ 
    private static readonly string subscriptionKey = "f3c99b4236cd4a7c936d271bc3687768";
    private static readonly string endpoint = "https://api.cognitive.microsofttranslator.com/";

    // Add your location, also known as region. The default is global.
    // This is required if using a Cognitive Services resource.
    private static readonly string location = "global";
    
    static async Task Main(string[] args)
    {
        Console.WriteLine("Thank you");
        await Translate();
    }
    
    static async Task Translate()
    {
                // Output languages are defined as parameters, input language detected.
        string base_url = "https://api.translator.azure";
        string route = "/translate?api-version=3.0&to=de&to=it"; //This line selects what language to use. "to=de&to=it"
        string textToTranslate = TextInput();
        object[] body = new object[] { new { Text = textToTranslate } };
        var requestBody = JsonConvert.SerializeObject(body);
    
        using (var client = new HttpClient())
        using (var request = new HttpRequestMessage())
        {
            // Build the request.
            request.Method = HttpMethod.Post;
            request.RequestUri = new Uri(endpoint + route);
            request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
            request.Headers.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
            request.Headers.Add("Ocp-Apim-Subscription-Region", location);
            
            // Send the request and get response.
            HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
            // Read response as a string.
            string result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);
            
        }
    }
    // Function to let Users input Text to be translated
    // Requires nothing
    // Returns String
    public static string TextInput()  
    {
        string ToTranslate;
        return ToTranslate = Console.ReadLine();
    }
}