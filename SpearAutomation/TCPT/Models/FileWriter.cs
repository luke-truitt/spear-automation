using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPT.Models
{
    public class FileWriter
    {
        public void WriteData(string filePath, List<Resource> resources)
        {
            string data = ConvertData(resources);
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath))
            {
                file.WriteLine(data);
            }
        }

        public string ConvertData(List<Resource> resources)
        {
            string header = "ResourceId,Resource Type,Availability,Location,Vehicle Type,Certification Level\n";

            StringBuilder csvData = new StringBuilder();
            csvData.Append(header);
            foreach (var resource in resources)
            {
                csvData.Append(resource.ToCSVString());
            }

            return csvData.ToString();
        }
    }
}
