using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public string Operation { get; set; }

        public ObservableCollection<string> Options { get; } = new ObservableCollection<string>
        { Operations.Sum, Operations.Average, Operations.Min, Operations.Max };

        private string _selectedOperation;
        public string SelectedOperation
        {
            get { return _selectedOperation; }
            set
            {
                _selectedOperation = value;
                this.Operation = value;
                OnPropertyChanged(nameof(SelectedOperation));
            }
        }

        private string _resultMessage;
        public string ResultMessage {
            get { return _resultMessage; }
            set
            {
                _resultMessage = value;
                OnPropertyChanged(nameof(ResultMessage));
            }
        }

        public Generators(string name, int interval, string operation)
        {
            Name = name;
            Interval = interval;
            Operation = operation;
            SelectedOperation = Operation;
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
            foreach (List<decimal> set in sets)
            {
                decimal result = this.Calculate(this.Operation, set);
                string display = $"{DateTime.Now} {this.Name} {result}\n";
                Console.WriteLine(display);
                this.ResultMessage += display;
                await Task.Delay(this.Interval * 1000);
            }            
        }

        public decimal Calculate(string operation, List<decimal> set)
        {
            switch (operation)
            {
                case Operations.Sum:
                    return set.Sum(); 
                case Operations.Average:
                    return decimal.Round(set.Average(), 2);
                case Operations.Min:
                    return set.Min();
                case Operations.Max:
                    return set.Max();
                default:
                    return 0;
            }
        }
    }

    public static class Operations
    {
        public const string Sum = "sum";
        public const string Average = "average";
        public const string Min = "min";
        public const string Max = "max";
    }    
}
