using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCSS.Models
{
    public class FileWriter
    {
        public void WriteData(string filePath, List<Vehicle> vehicles)
        {
            string data = ConvertData(vehicles);
            using(System.IO.StreamWriter file = new System.IO.StreamWriter(filePath))
            {
                file.WriteLine(data);
            }
        }

        public string ConvertData(List<Vehicle> vehicles)
        {
            string header = "Tam,Date Available,Location,Vehicle Type\n";

            StringBuilder csvData = new StringBuilder();
            csvData.Append(header);
            foreach(Vehicle vehicle in vehicles)
            {
                csvData.Append(vehicle.ToCSVString());
            }

            return csvData.ToString();
        }
    }
}
