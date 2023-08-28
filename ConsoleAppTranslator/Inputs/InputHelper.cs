using DustInTheWind.ConsoleTools.Controls.InputControls;
using Sharprompt;

namespace ConsoleAppTranslator.Inputs;

public static class InputHelper
{
    private const string EndpointTextTranslation = "https://api.cognitive.microsofttranslator.com/";
    private const string RegionTextTranslation = "eastus";
    private const string EnglishLanguage = "Ingles";
    private const string PortugueseLanguage = "Portugues";
    private const string SpanishLanguage = "Espanhol";
    private const string EnglishCode = "en";
    private const string PortugueseCode = "pt";
    private const string SpanishCode = "es";

    public static string GetEndpointTextTranslation()
    {
        var inputEndpointTextTranslation = new StringValue(
            $"Endpoint Text Translation Service (default -> {EndpointTextTranslation}):");
        inputEndpointTextTranslation.DefaultValue = EndpointTextTranslation;
        inputEndpointTextTranslation.AcceptDefaultValue = true;
        string endpointTextTranslation;
        do
        {
            endpointTextTranslation = inputEndpointTextTranslation.Read();
        } while (String.IsNullOrWhiteSpace(endpointTextTranslation));
        return endpointTextTranslation;
    }

    public static string GetRegionTextTranslation()
    {
        var inputRegionTextTranslation = new StringValue(
            $"Region do Azure (default -> {RegionTextTranslation}):");
        inputRegionTextTranslation.DefaultValue = RegionTextTranslation;
        inputRegionTextTranslation.AcceptDefaultValue = true;
        string regionTextTranslation;
        do
        {
            regionTextTranslation = inputRegionTextTranslation.Read();
        } while (String.IsNullOrWhiteSpace(regionTextTranslation));
        return regionTextTranslation;
    }

    public static string GetSubscriptionKey()
    {
        var inputSubscriptionKey = new StringValue("Subscription Key:");
        string subscriptionKey;
        do
        {
            subscriptionKey = inputSubscriptionKey.Read();
            if (String.IsNullOrWhiteSpace(subscriptionKey))
                ShowErrorMessage("Informe uma Subscription Key valida para o recurso!");
        } while (String.IsNullOrWhiteSpace(subscriptionKey));
        return subscriptionKey;
    }

    public static string GetTextToTranslate()
    {
        var inputTextToTranslate = new StringValue("Texto para traducao:");
        string textToTranslate;
        do
        {
            textToTranslate = inputTextToTranslate.Read();
            if (String.IsNullOrWhiteSpace(textToTranslate))
                ShowErrorMessage("Informe um texto valido para traducao!");
        } while (String.IsNullOrWhiteSpace(textToTranslate));
        return textToTranslate;
    }

    public static string GetTargetLanguage() =>
        GetLanguageCode(Prompt.Select("Lingua a ser usada na traducao:",
            new[] { EnglishLanguage, PortugueseLanguage, SpanishLanguage }));

    public static string GetAnswerContinue() =>
        Prompt.Select("Continuar?", new[] { "Sim", "Nao" });

    private static string GetLanguageCode(string language)
    {
        switch (language)
        {
            case EnglishLanguage:
                return EnglishCode;
            case PortugueseLanguage:
                return PortugueseCode;
            case SpanishLanguage:
                return SpanishCode;
            default:
                throw new ArgumentException("Lingua invalida!");
        }
    }
    
    private static void ShowErrorMessage(string message)
    {
        var oldForegroundColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ForegroundColor = oldForegroundColor;
        Console.WriteLine();
    }
}