using Appesame.Data;
using MvvmHelpers;
using System;
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
        public ICommand GoBackCommand { get; set; }
        public ICommand OnAppearingCommand { get; set; }
        public ICommand AddCommand { get; set; }

        public RecordingViewModel()
        {
            GoBackCommand = new Command(async () => await GoBack());
            OnAppearingCommand = new Command(SetExamName);
            AddCommand = new Command(async () => await AddItem());

        }
        private void SetExamName()
        {
            ExamName = Preferences.Get("CurrentExam", "Recordings");
        }

        private async Task GoBack()
        {
            await Shell.Current.GoToAsync("//ExamChooser", true);
        }
        private async Task AddItem()
        {
            await Shell.Current.GoToAsync("Recording/addItem", true);
        }
    }
}
