using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Vision.Contract;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Eleonora.PCL.Services
{
    public class ServiceComputerVision
    {
        private string SubscriptionKey = "";
        /// <summary>
        /// Get a list of available domain models
        /// </summary>
        /// <returns></returns>
        public async Task<ModelResult> GetAvailableDomainModels()
        {
            VisionServiceClient VisionServiceClient = new VisionServiceClient(SubscriptionKey, @"https://westeurope.api.cognitive.microsoft.com/vision/v1.0");
            //
            // Analyze the url against the given domain
            //
            ModelResult modelResult = await VisionServiceClient.ListModelsAsync();
            return modelResult;
        }
        public async Task<string> MakeAnalysisRequest(Stream stream, Model model)
        {
            try
            {
                // OcrResults text;
                VisionServiceClient client = new VisionServiceClient(SubscriptionKey, @"https://westeurope.api.cognitive.microsoft.com/vision/v1.0");
                var result = await client.AnalyzeImageInDomainAsync(stream, model);

                return "Algo";
            } catch (ClientException ex)
            {
                return ex.Message;
            }
        }
        
    }
}
