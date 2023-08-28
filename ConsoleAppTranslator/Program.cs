using System.Text;
using Newtonsoft.Json.Linq;
using ConsoleAppTranslator.Inputs;

Console.WriteLine("***** Utilizando o Azure Text Translation *****");
Console.WriteLine();

var endpointTextTranslation = InputHelper.GetEndpointTextTranslation();
Console.WriteLine();

var regionTextTranslation = InputHelper.GetRegionTextTranslation();
Console.WriteLine();

var subscriptionKey = InputHelper.GetSubscriptionKey();
Console.WriteLine();

using (var client = new HttpClient())
{
    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Region", regionTextTranslation);

    string continuar;
    do
    {
        var textToTranslate = InputHelper.GetTextToTranslate();
        Console.WriteLine();

        var targetLanguage = InputHelper.GetTargetLanguage();
        Console.WriteLine();

        var uri = $"{endpointTextTranslation}/translate?api-version=3.0&to={targetLanguage}";
        Console.WriteLine($"Uri Translation: {uri}");

        var requestBody = $$"""[ { "Text": "{{textToTranslate}}" } ]""";
        Console.WriteLine($"Request Body: {requestBody}");

        var response = await client.PostAsync(uri, new StringContent(requestBody,
            Encoding.UTF8, "application/json"));
        var responseBody = await response.Content.ReadAsStringAsync();
        var jsonResponseBody = JToken.Parse(responseBody);

        Console.WriteLine();
        Console.WriteLine($"Status Code: {(int)response.StatusCode} {response.StatusCode}");
        Console.WriteLine($"Response Body: {responseBody}");
        Console.WriteLine(
            $"Conteudo traduzido: {jsonResponseBody.SelectToken("$[0].translations[0].text")!.Value<string>()}");
        Console.WriteLine();

        continuar = InputHelper.GetAnswerContinue();
        Console.WriteLine();
    } while (continuar == "Sim");

    Console.WriteLine("Testes concluidos!");
}