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
        private string apiRoot = @"https://westeurope.api.cognitive.microsoft.com/vision/v1.0";
        /// <summary>
        /// Get a list of available domain models
        /// </summary>
        /// <returns></returns>
        public async Task<ModelResult> GetAvailableDomainModels()
        {
            VisionServiceClient VisionServiceClient = new VisionServiceClient(SubscriptionKey, apiRoot);
            //
            // Analyze the url against the given domain
            //
            ModelResult modelResult = await VisionServiceClient.ListModelsAsync();
            return modelResult;
        }
        public async Task<AnalysisInDomainResult> MakeAnalysisRequest(Stream stream, Model model)
        {
            // OcrResults text;
            VisionServiceClient client = new VisionServiceClient(SubscriptionKey, apiRoot);
            AnalysisInDomainResult analysisResult = await client.AnalyzeImageInDomainAsync(stream, model);

            return analysisResult;
        }
        
    }
}
