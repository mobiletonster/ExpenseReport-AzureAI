using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace ExpenseReports.Services
{
    public class RecognizerService
    {
        private readonly string recognizerBaseUrl;
        private readonly string recognizerKey;
        public RecognizerService(IConfiguration configuration)
        {
            recognizerBaseUrl = configuration.GetSection("recognizer:recognizerBaseUrl")?.Value;
            recognizerKey = configuration.GetSection("recognizer:recognizerKey")?.Value;
        }

        public async Task<string> ProcessReceiptAsync(string receiptUrl)
        {
            var analyzerUrl = $"{recognizerBaseUrl}prebuilt/receipt/asyncBatchAnalyze";
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", recognizerKey);
            var recognizerBody = new RecognizerBody() { url = receiptUrl };
            var recognizerBodyString = JsonConvert.SerializeObject(recognizerBody);
            var content = new StringContent(recognizerBodyString,Encoding.UTF8,"application/json");
            var response = await client.PostAsync(analyzerUrl, content);
            var operationLocation = response.Headers.GetValues("Operation-Location").FirstOrDefault();
            var operationId = new Uri(operationLocation).Segments.Last();
            return operationId;
        }

        public async Task<string> GetReceiptAnalysisAsync(string operationId)
        {
            var operationUrl = $"{recognizerBaseUrl}prebuilt/receipt/operations/{operationId}";
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", recognizerKey);
            var response = await client.GetAsync(operationUrl);
            var jsonResult = await response.Content.ReadAsStringAsync();
            while (jsonResult.Contains("{\"status\":\"Running\"}"))
            {
                await Task.Delay(1000);
                response = await client.GetAsync(operationUrl);
                jsonResult = await response.Content.ReadAsStringAsync();
            }
            return jsonResult;
        }
    }

    public class RecognizerBody
    {
        public string url { get; set; }
    }

}
