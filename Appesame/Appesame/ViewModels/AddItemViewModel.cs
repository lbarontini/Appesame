using Appesame.Data;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Appesame.ViewModels
{
    [QueryProperty("ItemName", "itemName")]
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
                _ItemName = Uri.UnescapeDataString(value);
                OnPropertyChanged(ItemName);
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

        public AddItemViewModel()
        {
            GoBackCommand = new Command(async () => await GoBack());
            OkCommand = new Command(async () => await AddItemToDatabase());
        }
        private async Task GoBack()
        {
            await Shell.Current.GoToAsync("//Tabbar", true);
        }
        private async Task AddItemToDatabase()
        {
            var examName = Preferences.Get("CurrentExam","");
            DataService.AddItem(examName, ItemName, fileName,fileUri);
            await Shell.Current.GoToAsync("//Tabbar", true);
        }
    }
}
