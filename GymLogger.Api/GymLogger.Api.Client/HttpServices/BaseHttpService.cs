using GymLogger.Api.Client.Models;
using GymLogger.Shared.SourceGeneration;
using Microsoft.AspNetCore.WebUtilities;
using System.Collections;
using System.Text.Json;

namespace GymLogger.Api.Client.HttpServices;

public abstract class BaseHttpService
{
    private static readonly JsonSerializerOptions Options = new(JsonSerializerDefaults.Web) { PropertyNameCaseInsensitive = true };
    protected static JsonSourceGenerationContext Context = new JsonSourceGenerationContext(Options);
    protected readonly string ApiRoute;
    protected readonly HttpClient HttpClient;

    protected BaseHttpService(IHttpClientFactory httpClientFactory, string apiRoute)
    {
        this.HttpClient = httpClientFactory.CreateClient("BlazorServerHttpClient");
        this.ApiRoute = apiRoute;
    }

    protected async Task<HttpResponse> GetHttpResponseAsync<TQueryParameter>(string url,
        TQueryParameter queryParameter)
    {
        url = GetQueryParameterDictionary(url, queryParameter);
        var response = await this.HttpClient.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) throw new ApplicationException(content);

        return new HttpResponse(content, response);
    }

    protected static string GetQueryParameterDictionary<TQueryParameter>(
        string url, TQueryParameter queryParameter)
    {
        try
        {
            var dictionary = new Dictionary<string, string>();
            foreach (var property in queryParameter.GetType().GetProperties())
            {
                var propertyValue = "";
                var prop = property.GetValue(queryParameter);
                if (prop is IEnumerable enumerable && prop.GetType() != typeof(string))
                {
                    url = enumerable.Cast<object>().Aggregate(url, (current, item) => QueryHelpers.AddQueryString(current, property.Name, item.ToString()));
                }
                else if (Nullable.GetUnderlyingType(property.PropertyType) != null)
                {
                    if (property.GetValue(queryParameter, null) != null)
                    {
                        if (!string.IsNullOrEmpty(property.GetValue(queryParameter, null)?.ToString()))
                        {
                            if (property.PropertyType == typeof(DateTime?))
                            {
                                var date = property.GetValue(queryParameter, null) as DateTime?;
                                if (date != null)
                                {
                                    var dateValue = date.Value;
                                    var dateStr = $"{dateValue.Month}/{dateValue.Day}/{dateValue.Year}";
                                    propertyValue = dateStr;
                                }
                            }
                            else
                            {
                                propertyValue = property.GetValue(queryParameter, null).ToString();
                            }
                            dictionary.Add(property.Name, propertyValue);
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(property.GetValue(queryParameter, null)?.ToString()))
                    {
                        propertyValue = property.GetValue(queryParameter, null)?.ToString(); dictionary.Add(property.Name, propertyValue);
                    }
                }
            }
            url = QueryHelpers.AddQueryString(url, dictionary);
            return url;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}
