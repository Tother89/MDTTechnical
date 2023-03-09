using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator_Console
{
    public class DataProcessor
    {
        public List<List<decimal>> Datasets { get; set; }
        public List<Generators>? Generators { get; set; }       

        public async Task<List<Task>?> ProcessData()
        {
            if (Datasets == null)
            {
                Console.WriteLine("Missing dataset data");
                return null;
            }
            
            if (Generators == null)
            {
                Console.WriteLine("Missing generator data");
                return null;
            }



            var actions = new List<Task>();
            foreach(Generators generator in Generators)
            {
                actions.Add(generator.PrintData(this.Datasets));
            }

            return actions;
        }
    }
}
