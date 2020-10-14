using Appesame.Data;
using Appesame.ViewModels.Utilities;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Appesame.ViewModels
{
    [QueryProperty("itemName", "itemName")]
    public class AddItemViewModel : BaseViewModel
    {
        private string _itemName;
        public string itemName
        {
            get 
            {
                return _itemName;
            }
            set
            {
                _itemName = Uri.UnescapeDataString(value);
                //OnPropertyChanged(itemName);
            }
        }
        private string _fileName = "";
        public string fileName
        {
            get => _fileName;
            set
            {
                _fileName = value;
                OnPropertyChanged("fileName");
            }
        }

        private string _fileUri = "";
        public string fileUri
        {
            get => _fileUri;
            set
            {
                _fileUri = value;
                OnPropertyChanged("fileUri");
            }
        }
        public Command GoBackCommand { get; set; }
        public Command OkCommand { get; set; }
        public Command SearchCommand { get; set; }

        public AddItemViewModel()
        {
            GoBackCommand = new Command(async () => await GoBack());
            OkCommand = new Command(async () => await AddItemToDatabase());
            SearchCommand = new Command(async () => await ChooseFile());
        }

        private async Task GoBack()
        {
            await Shell.Current.GoToAsync("//Tabbar", true);
        }
        private async Task AddItemToDatabase()
        {
            var examName = Preferences.Get("CurrentExam","");
            DataService.AddItem(examName, itemName, fileName,fileUri);
            await Shell.Current.GoToAsync("//Tabbar", true);
        }
        private async Task ChooseFile()
        {
            PickOptions opt;
            switch (itemName)
            {
                case "Flashcard":
                    opt = new PickOptions
                    {
                        FileTypes = CustomFilePickerFileType.Pdf
                    };
                    break;
                case "Recording":
                    opt = new PickOptions
                    {
                        FileTypes = CustomFilePickerFileType.Audio
                    };
                    break;
                case "Cmap":
                    opt = new PickOptions
                    {
                        FileTypes = FilePickerFileType.Images
                    };
                    break;
                case "Exercise":
                    opt = new PickOptions
                    {
                        FileTypes = CustomFilePickerFileType.Pdf
                    };
                    break;
                default:
                    opt = new PickOptions
                    {
                        FileTypes = FilePickerFileType.Images
                    };
                    break;
            }
            try
            {
                var result = await FilePicker.PickAsync(opt);
                if (result != null)
                {
                    fileName = result.FileName;
                    fileUri = result.FullPath;
                }
            }
            catch (Exception ex)
            {
                // The user canceled or something went wrong
            }          
        }
    }
}
