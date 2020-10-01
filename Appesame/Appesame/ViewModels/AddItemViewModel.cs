using Appesame.Data;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Appesame.ViewModels
{
    public class AddItemViewModel : BaseViewModel
    {
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
            var itemName = "Flashcard";
            var examName = "test";
            DataService.AddItem(examName,itemName,fileName,fileUri);
            await Shell.Current.GoToAsync("//Tabbar", true);
        }
    }
}
