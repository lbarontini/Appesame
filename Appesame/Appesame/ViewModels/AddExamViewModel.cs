using Appesame.Data;
using Appesame.Models;
using MvvmHelpers;
using Realms;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Appesame.ViewModels
{
    public class AddExamViewModel : BaseViewModel
    {
        public Command GoBackCommand { get; set; }
        public Command OkCommand { get; set; }

        private string _examName = "";        
        public string examName
        {
            get => _examName;
            set
            {
                _examName = value;
                OnPropertyChanged("ExamName");
            }
        }

        public AddExamViewModel()
        {
            GoBackCommand = new Command(async () => await GoBack());
            OkCommand = new Command(async () => await AddExamToDatabase());
        }
        private async Task GoBack()
        {
            await Shell.Current.GoToAsync("//ExamChooser", true);
        }

        private async Task AddExamToDatabase()
        {
            if (string.IsNullOrWhiteSpace(examName))
            { 
                await App.Current.MainPage.DisplayAlert("Error", "Enter exam name ", "OK");
                examName = "";
            }
            else
            {
                DataService.AddExam(examName);
                examName = "";
                await Shell.Current.GoToAsync("//ExamChooser", true);
            }
        }
    }
}
