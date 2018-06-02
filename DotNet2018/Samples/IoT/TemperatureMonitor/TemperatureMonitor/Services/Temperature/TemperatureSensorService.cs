using System;
using System.Diagnostics;
using TemperatureMonitor.Models;

namespace TemperatureMonitor.Services.Temperature
{
    public class TemperatureSensorService : ITemperatureSensorService
    {
        readonly public string DHT22 = "22";
        readonly public int Pin = 18;
        // This is an example of reading a sensor using the output of a python script
        // the python code is available from https://github.com/adafruit/Adafruit_Python_DHT/blob/master/examples/AdafruitDHT.py 
        // you need to copy to the raspberry.
        readonly public string AdafruitDhtExecutablePath = "/home/pi/Adafruit_Python_DHT/examples/AdafruitDHT.py";

        public SensorData GetData()
        {
            SensorData data = new SensorData();
            data.Date = DateTime.Now.ToString();

            string value = string.Empty;
            var order = $"{AdafruitDhtExecutablePath} {DHT22} {Pin}";
            var arguments = string.Format("-c \"sudo {0}\"", order);

            using (var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = arguments,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            })
            {
                proc.Start();
                proc.WaitForExit();
                value = proc.StandardOutput.ReadToEnd().Trim();
            };

            var elements = value.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            if (elements.Length == 2)
            {
                foreach (var element in elements)
                {
                    var elementValues = element.Split('=');
                    if (elementValues[0] == "Temp")
                    {
                        data.Temperature = Convert.ToInt32(elementValues[1]);
                    }
                    else if (elementValues[0] == "Humidity")
                    {
                        data.Humidity = Convert.ToInt32(elementValues[1]);
                    }
                }
            }

            return data;
        }
    }
}