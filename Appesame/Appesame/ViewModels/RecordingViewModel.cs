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
        private string examName = "";
        public string ExamName
        {
            get => examName;
            set
            {
                examName = value;
                OnPropertyChanged("ExamName");
            }
        }

        public IEnumerable<RecordingModel> RecordingModelList { get; set; }
        public ICommand GoBackCommand { get; set; }
        public ICommand OnAppearingCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public Command<object> DeleteCommand { get; set; }

        public RecordingViewModel()
        {
            OnAppearingCommand = new Command(OnAppearing);
            GoBackCommand = new Command(async () => await GoBack());
            AddCommand = new Command(async () => await AddItem());
            DeleteCommand = new Command<object>(DeleteItem);
        }
        private void OnAppearing()
        {
            ExamName = Preferences.Get("CurrentExam", "Recordings");
            RecordingModelList = DataService.GetAllItems("Recording", ExamName) as IEnumerable<RecordingModel>;
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
        private void DeleteItem(object obj)
        {
            DataService.DeleteItem(obj, "Recording");
        }
    }
}
