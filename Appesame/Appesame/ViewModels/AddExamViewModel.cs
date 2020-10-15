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
            if (string.IsNullOrWhiteSpace(ExamName))
            { 
                await App.Current.MainPage.DisplayAlert("Error", "Enter exam name ", "OK");
                ExamName = "";
            }
            else
            {
                DataService.AddExam(ExamName);
                ExamName = "";
                await Shell.Current.GoToAsync("//ExamChooser", true);
            }
        }
    }
}
