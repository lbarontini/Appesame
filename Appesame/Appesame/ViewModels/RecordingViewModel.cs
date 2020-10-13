using Appesame.Data;
using Appesame.Models;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Appesame.ViewModels
{
    public class RecordingViewModel : BaseViewModel
    {
        private string _examName = "";
        public string examName
        {
            get => _examName;
            set
            {
                _examName = value;
                OnPropertyChanged("examName");
            }
        }

        public IEnumerable<RecordingModel> RecordingModelList { get; set; }
        public ICommand GoBackCommand { get; set; }
        public ICommand OnAppearingCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand ItemTappedCommand { get; set; }
        public Command<object> DeleteCommand { get; set; }

        public RecordingViewModel()
        {
            OnAppearingCommand = new Command(OnAppearing);
            GoBackCommand = new Command(async () => await GoBack());
            AddCommand = new Command(async () => await AddItem());
            ItemTappedCommand = new Command<RecordingModel>(async (x) => await OnItemSelectedAsync(x));
            DeleteCommand = new Command<object>(DeleteItem);
        }
        private void OnAppearing()
        {
            examName = Preferences.Get("CurrentExam", "Recordings");
            RecordingModelList = DataService.GetAllItems("Recording", examName) as IEnumerable<RecordingModel>;
            OnPropertyChanged("RecordingModelList");
        }
        private async Task GoBack()
        {
            await Shell.Current.GoToAsync("//ExamChooser", true);
        }
        private async Task AddItem()
        {
            await Shell.Current.GoToAsync($"Recordings/addItem?itemName=Recording");
        }

        private async Task OnItemSelectedAsync(RecordingModel x)
        {
            //Uri uriToOpen = new Uri(x.Uri);           
            if (await Launcher.CanOpenAsync(x.Uri))
            {
                await Launcher.OpenAsync(new OpenFileRequest
                {
                    File = new ReadOnlyFile(x.Uri, "audio/*")
                });
            }
        }
        private void DeleteItem(object obj)
        {
            DataService.DeleteItem(obj, "Recording");
        }
    }
}
