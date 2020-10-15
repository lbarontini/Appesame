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
    public class ExerciseViewModel : BaseViewModel
    {
        private string _ExamName = "";
        public string ExamName
        {
            get => _ExamName;
            set
            {
                _ExamName = value;
                OnPropertyChanged("ExamName");
            }
        }

        public IEnumerable<ExerciseModel> ExerciseModelList { get; set; }
        public ICommand GoBackCommand { get; set; }
        public ICommand OnAppearingCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand ItemTappedCommand { get; set; }
        public Command<object> DeleteCommand { get; set; }

        public ExerciseViewModel()
        {
            OnAppearingCommand = new Command(OnAppearing);
            GoBackCommand = new Command(async () => await GoBack());
            AddCommand = new Command(async () => await AddItem());
            ItemTappedCommand = new Command<ExerciseModel>(async (x) => await OnItemSelectedAsync(x));
            DeleteCommand = new Command<object>(DeleteItem);
        }
        private void OnAppearing()
        {
            ExamName = Preferences.Get("CurrentExam", "Exercises");
            ExerciseModelList = DataService.GetAllItems("Exercise", ExamName) as IEnumerable<ExerciseModel>;
            OnPropertyChanged("ExerciseModelList");
        }
        private async Task GoBack()
        {
            await Shell.Current.GoToAsync("//ExamChooser", true);
        }
        private async Task AddItem()
        {
            await Shell.Current.GoToAsync($"Exercises/addItem?ItemName=Exercise");
        }
        private async Task OnItemSelectedAsync(ExerciseModel x)
        {
            Uri uriToOpen = new Uri(x.Uri);
            if (await Launcher.CanOpenAsync(uriToOpen))
            {
                await Launcher.OpenAsync(new OpenFileRequest
                {
                    File = new ReadOnlyFile(x.Uri)
                    {
                        ContentType = "application/pdf"
                    }
                });
            }
        }
        private void DeleteItem(object obj)
        {
            DataService.DeleteItem(obj, "Exercise");
        }
    }
}

