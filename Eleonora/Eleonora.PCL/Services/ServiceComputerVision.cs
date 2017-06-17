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
        public static async Task<string> GetVision(Stream stream)
        {
            VisionServiceClient cliente = new VisionServiceClient(key);
            var vision = await cliente.CreateHandwritingRecognitionOperationAsync(stream);

            return vision.ToString();
        }
    }
}
