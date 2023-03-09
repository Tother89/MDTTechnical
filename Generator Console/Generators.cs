using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator_Console
{
    public class Generators : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public int Interval { get; set; }
        public Operations Operation { get; set; }

        private string _resultMessage;
        public string ResultMessage {
            get { return _resultMessage; }
            set
            {
                _resultMessage = value;
                OnPropertyChanged(nameof(ResultMessage));
            }
        }

        public Generators(string name, int interval, Operations operation)
        {
            Name = name;
            Interval = interval;
            Operation = operation;
            ResultMessage = string.Empty;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public virtual void OnPropertyChanged(string eventName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(eventName));
        }

        public async Task PrintData(List<List<decimal>> sets)
        {
            this.ResultMessage = string.Empty;
            await Task.Delay(this.Interval * 1000);
            foreach (List<decimal> set in sets)
            {
                decimal result = this.Calculate(this.Operation, set);
                string display = $"{DateTime.Now} {this.Name} {result}\n";
                Console.WriteLine(display);
                this.ResultMessage += display;
                await Task.Delay(this.Interval * 1000);
            }            
        }

        public decimal Calculate(Operations operation, List<decimal> set)
        {
            switch (operation)
            {
                case Operations.Sum:
                    return set.Sum(); 
                case Operations.Average:
                    return set.Average();
                case Operations.Min:
                    return set.Min();
                case Operations.Max:
                    return set.Max();
                default:
                    return 0;
            }
        }
    }

    public enum Operations
    {
        Sum,
        Average,
        Min,
        Max,
    }
}
