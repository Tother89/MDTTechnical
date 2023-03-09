
using CommunityToolkit.Mvvm.Input;
using Generator_Console;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Shapes;
using static System.Net.WebRequestMethods;
using File = System.IO.File;

namespace MDT_Technical
{
    public class MainPageViewModel : BaseViewModel
    {       
        private string? _status;
        public string? Status { get
            {
                return _status;
            }
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }     

        private ObservableCollection<Generators>? _generators;

        public ObservableCollection<Generators>? Generators {
            get { return _generators; }
            set
            {
                _generators = value;
                OnPropertyChanged(nameof(Generators));
            }
        }
        
        private ObservableCollection<ObservableCollection<decimal>>? _datasets;

        public ObservableCollection<ObservableCollection<decimal>>? Datasets {
            get { return _datasets; }
            set
            {
                _datasets = value;
                OnPropertyChanged(nameof(Datasets));
            }
        }

        private DataProcessor? Processor { get; set; }

        public ObservableCollection<string> Operations { get; } = new ObservableCollection<string> 
        { "Sum", "Min", "Max", "Average" };

        private string _selectedOperation;
        public string SelectedOperation
        {
            get { return _selectedOperation; }
            set
            {
                _selectedOperation = value;
                OnPropertyChanged(nameof(SelectedOperation));
            }
        }

        public ICommand LoadFileCommand { get; }
        public ICommand ActivateCommand { get; }

        public MainPageViewModel()
        {
            this.LoadFileCommand = new RelayCommand(Load);
            this.ActivateCommand = new RelayCommand(Activate);
            this.Status = "New module started. Please load a file.";
        }       

        private void Load()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.DefaultExt = ".json"; 
            fileDialog.Filter = "JSON files (.json)|*.json";
            fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (fileDialog.ShowDialog() == true)
            {
                string fileName = fileDialog.FileName;
                try
                {
                    this.Status = $"Importing file...";
                    string json = File.ReadAllText(fileName);
                    this.Processor = JsonConvert.DeserializeObject<DataProcessor>(json);

                    if (this.Processor == null)
                        throw new Exception("Unable to deserialize the json file.");

                    if (this.Processor.Generators == null)
                        throw new Exception("Unable to find generators.");
                    
                    if (this.Processor.Datasets == null)
                        throw new Exception("Unable to find datasets.");

                    this.Generators = new ObservableCollection<Generators>(this.Processor.Generators);
                    this.Datasets = new ObservableCollection<ObservableCollection<decimal>>();
                    foreach (var set in this.Processor.Datasets)
                    {
                        this.Datasets.Add(new ObservableCollection<decimal>(set));
                    }

                    this.Status = "File loaded";
                }
                catch (Exception ex)
                {
                    this.Status = $"Error loading JSON file: {ex.Message}";
                }
            }
        }     

        public async void Activate()
        {
            if (this.Processor == null)
                return;

            var actions = await this.Processor.ProcessData();

            if (actions == null)
                return;



            await Task.WhenAll(actions);
        }
    }
}
