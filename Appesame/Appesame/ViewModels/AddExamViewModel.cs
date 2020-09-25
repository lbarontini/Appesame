using Appesame.Data;
using Appesame.Models;
using MvvmHelpers;
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
            ExamModel Exam = new ExamModel(ExamName);
            ExamName = "";
            DataService.AddExam(Exam);
            await Shell.Current.GoToAsync("//ExamChooser", true);
        }
    }
}
