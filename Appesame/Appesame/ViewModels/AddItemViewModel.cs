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
    [QueryProperty("ItemName", "ItemName")]
    public class AddItemViewModel : BaseViewModel
    {
        private string _ItemName;
        public string ItemName
        {
            get 
            {
                return _ItemName;
            }
            set
            {
                //retriving the parameter passed when navigating to this page
                _ItemName = Uri.UnescapeDataString(value);
                OnPropertyChanged(ItemName);
            }
        }
        private string _FileName = "";
        public string FileName
        {
            get => _FileName;
            set
            {
                _FileName = value;
                OnPropertyChanged("FileName");
            }
        }

        private string _FileUri = "";
        public string FileUri
        {
            get => _FileUri;
            set
            {
                _FileUri = value;
                OnPropertyChanged("FileUri");
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
            await Shell.Current.GoToAsync("../..", true);
        }
        private async Task AddItemToDatabase()
        {
            if (string.IsNullOrWhiteSpace(FileName) || string.IsNullOrWhiteSpace(FileUri))
                await App.Current.MainPage.DisplayAlert("Error", "Please choose a file", "OK");
            else
            {
                DataService.AddItem(Preferences.Get("CurrentExam", ""), ItemName, FileName, FileUri);
                await Shell.Current.GoToAsync("../..", true);
            }
        }
        private async Task ChooseFile()
        {
            //switching from items for writing in the correct portion of the database
            PickOptions opt;
            switch (ItemName)
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
                var result = await FilePicker.PickAsync(opt);
            if (result != null)
            {
                FileName = result.FileName;
                FileUri = result.FullPath;
            }
    }
}
