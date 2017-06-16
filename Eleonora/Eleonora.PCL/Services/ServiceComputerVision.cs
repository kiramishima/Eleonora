using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.ProjectOxford.Vision;

namespace Eleonora.PCL.Services
{
    class ServiceComputerVision
    {
        static string key = "";
        public static async Task<Dictionary<string, float>> GetEmotions(Stream stream)
        {
            VisionServiceClient cliente = new VisionServiceClient(key);
            var vision = await cliente.CreateHandwritingRecognitionOperationAsync(stream);
            if (vision == null)
                return null;
            return null;
        }
    }
}
