
using Appesame.Data;
using Appesame.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;
using Xamarin.Forms.Core;
using System;
using Android.Util;
using Xamarin.Essentials;
using MvvmHelpers;

namespace Appesame.ViewModels
{
    public class ExamViewModel : BaseViewModel
    {
        public List<ExamModel> ExamModelList{get; set;}
        public ICommand ItemTappedCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public Command<object> DeleteCommand { get; set; }
        public ICommand OnAppearingCommand { get; set; }


        public ExamViewModel()
        {
            ExamModelList = new List<ExamModel>();
            OnAppearingCommand = new Command(GetExams);
            ItemTappedCommand = new Command<ExamModel>(async (x) => await  OnItemSelectedAsync(x));
            AddCommand = new Command(async () => await AddExam());
            DeleteCommand = new Command<object>(DeleteExam);
        }

        private void GetExams(object obj)
        {
            ExamModelList = (List<ExamModel>)DataService.GetAllExams();
        }
        private async Task OnItemSelectedAsync(ExamModel examModel)
        {
            Preferences.Set("CurrentExam", examModel.Name);
            await Shell.Current.GoToAsync("//Tabbar", true);   
        }
        private async Task AddExam() 
        {
            await Shell.Current.GoToAsync("//AddExam", true);
        }
        private void DeleteExam(object obj)
        {
            var exam = obj as ExamModel;
            DataService.DeleteExam(exam);
            ExamModelList = (List<ExamModel>)DataService.GetAllExams();
        }
    }
}
