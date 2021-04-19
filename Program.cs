using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json; 


class Program
{ 
    //Key and Endpoint provided when generating Resource groups, required to access Translator
    private static readonly string subscriptionKey = "f3c99b4236cd4a7c936d271bc3687768";
    private static readonly string endpoint = "https://api.cognitive.microsofttranslator.com/";

    //Location is required for using Cognitive Services, Global is default
    private static readonly string location = "global";
    
    void Main(string[] args)
    {
        Console.WriteLine("Enter your phrase to be translated"); //Debugging line
        await Translate();  //Call Translate function
    }
    
    //Translate function, Asynchronous Task function, allows use of Async and Await
    //Requires: nothing
    //Returns: currently nothing
    //TODO: Add variable to let user change what language is used
    //TODO: Connect Translator to HTML code for release
    static async Task Translate()
    {
        // Output languages are defined as parameters, input language detected.
        //string base_url = "https://api.translator.azure";
        string route = "/translate?api-version=3.0&to=de&to=it";        //Delects what language to use. "to=de&to=it"
        string textToTranslate = TextInput();                           //Input text for translation
        object[] body = new object[] { new { Text = textToTranslate } };
        var requestBody = JsonConvert.SerializeObject(body);
    
        //Create client/request
        using (var client = new HttpClient())
        using (var request = new HttpRequestMessage())
        {
            // Build the request to send to Cognitive Services for translation
            request.Method = HttpMethod.Post;
            request.RequestUri = new Uri(endpoint + route);   //Create new Unique Resource Identifier(URI) for HTTPRequestMessage   
            request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");   //Add StringContent to request for translation
            request.Headers.Add("Ocp-Apim-Subscription-Key", subscriptionKey);  //Add Subscription key info to request
            request.Headers.Add("Ocp-Apim-Subscription-Region", location);      //Add Location info to Request
            
            // Send the request and get response.
            HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
            // Read response as a string.
            string result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);  //Write result, only for debugging

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