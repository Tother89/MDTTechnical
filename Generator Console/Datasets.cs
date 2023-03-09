using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Generator_Console
{
    public class Datasets
    {
        public List<List<decimal>> Data { get; set; }


        public Datasets(List<List<decimal>> datasets)
        {
            this.Data = datasets;
        }        
    }
}
