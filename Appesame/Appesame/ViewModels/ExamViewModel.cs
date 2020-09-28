
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
using System.Collections.Specialized;

namespace Appesame.ViewModels
{
    public class ExamViewModel : BaseViewModel
    {
        public ObservableCollection<ExamModel> _ExamModelList;
        public ObservableCollection<ExamModel> ExamModelList
        {
            get { return _ExamModelList; }
            set
            {
                SetProperty(ref _ExamModelList, value);
                OnPropertyChanged("ExamModelList");
            }
        }
        public ICommand ItemTappedCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public Command<object> DeleteCommand { get; set; }
        public ExamViewModel()
        {
            ExamModelList = new ObservableCollection<ExamModel>();
            ExamModelList = DataService.GetAllExams();
            ItemTappedCommand = new Command<ExamModel>(async (x) => await  OnItemSelectedAsync(x));
            AddCommand = new Command(async () => await AddExam());
            DeleteCommand = new Command<object>(DeleteExam);
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
        }
    }
}
