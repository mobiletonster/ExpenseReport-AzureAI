using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseReports.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ExpenseReports.Web.Controllers
{
    public class TryController : Controller
    {
        private StorageService _storageService;
        private RecognizerService _recognizerService;
        public TryController(StorageService storageService, RecognizerService recognizerService)
        {
            _storageService = storageService;
            _recognizerService = recognizerService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("receipts")]
        public async Task<IActionResult> UploadReceipt(IFormFile receiptFile)
        {
            await _storageService.WriteStreamToBlobAsync(receiptFile.FileName, receiptFile.OpenReadStream());
            return Created($"/receipts/{receiptFile.FileName}", receiptFile.FileName);
        }

        [HttpGet("receipts/{filename}")]
        public async Task<IActionResult> GetReceipt(string filename)
        {
            var file = await _storageService.ReadBlobToStreamAsync(filename);
            return Ok(file);
        }

        [HttpGet("analyze/{filename}")]
        public async Task<IActionResult> AnalyzeReceipt(string filename)
        {
            var operationId = await _recognizerService.ProcessReceiptAsync($"https://modex.azurewebsites.net/receipts/{filename}");
            return Ok(operationId);
        }

        [HttpGet("analyze/{operationId}/fullresults")]
        public async Task<IActionResult> AnalysisFullResults(string operationId)
        {
            var analysis = await _recognizerService.GetReceiptAnalysisAsync(operationId);
            return Ok(analysis);
        }

        [HttpGet("analyze/{operationId}/results")]
        public async Task<IActionResult> AnalysisResults(string operationId)
        {
            try
            {
                var analysis = await _recognizerService.GetReceiptAnalysisAsync(operationId);
                var analysisResults = JsonConvert.DeserializeObject<AnalysisResults>(analysis);
                return PartialView("_AnalysisResults", analysisResults);
            }
            catch
            {
                return Ok("<div>ERROR PROCESSING IMAGE</div>");
            }

        }
    }
}