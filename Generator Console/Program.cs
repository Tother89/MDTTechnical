using Generator_Console;
using Newtonsoft.Json;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static async Task Main(string[] args)
        {            
            string path = @"data.json";

            if (args.Length > 0)
            {
                path = args[0];
            }

            if (string.IsNullOrEmpty(path))
            {
                Console.WriteLine("The input file could not be found");
                return;
            }

            using (StreamReader file = File.OpenText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                DataProcessor processor = (DataProcessor)serializer.Deserialize(file, typeof(DataProcessor));

                if (processor != null)
                {
                    await processor.ProcessData();
                }
            }
        }
    }
}