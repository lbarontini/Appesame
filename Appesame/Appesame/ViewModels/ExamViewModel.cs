
using Appesame.Data;
using Appesame.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Essentials;
using MvvmHelpers;

namespace Appesame.ViewModels
{
    public class ExamViewModel : BaseViewModel
    {
        public IEnumerable<ExamModel> ExamModelList { get; }

        public ICommand ItemTappedCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public Command<object> DeleteCommand { get; set; }
        public ExamViewModel()
        {
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
            DataService.DeleteExam(obj as ExamModel);
        }
    }
}
