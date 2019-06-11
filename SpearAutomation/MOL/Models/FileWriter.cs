using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOL.Models
{
    public class FileWriter
    {
        public void WriteData(string filePath, List<Personnel> marines)
        {
            string data = ConvertData(marines);
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath))
            {
                file.WriteLine(data);
            }
        }

        public string ConvertData(List<Personnel> marines)
        {
            string header = "MarineId,Date Returning,Location,Certification Level\n";

            StringBuilder csvData = new StringBuilder();
            csvData.Append(header);
            foreach (var marine in marines)
            {
                csvData.Append(marine.ToCSVString());
            }

            return csvData.ToString();
        }
    }
}
