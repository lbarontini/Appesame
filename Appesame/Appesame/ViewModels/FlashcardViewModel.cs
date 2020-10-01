
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
    public class FlashcardViewModel : BaseViewModel
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

        public IEnumerable<ItemModel> FlashcardModelList { get; }
        public ICommand GoBackCommand { get; set; }
        public ICommand OnAppearingCommand { get; set; }
        public ICommand AddCommand { get; set; }

        public Command<object> DeleteCommand { get; set; }

        public FlashcardViewModel()
        {
            FlashcardModelList = DataService.GetAllItems("Flashcards", ExamName);

            GoBackCommand = new Command(async () => await GoBack());
            OnAppearingCommand = new Command(GetExamName);
            AddCommand = new Command(async () => await AddItem());
            DeleteCommand = new Command<object>(DeleteItem);
        }

        private void GetExamName()
        {
            ExamName = Preferences.Get("CurrentExam", "Flashcard");
        }
        private async Task GoBack()
        {
            await Shell.Current.GoToAsync("//ExamChooser", true);
        }
        private async Task AddItem()
        {
            await Shell.Current.GoToAsync("Flashcards/addItem", true);
        }
        private void DeleteItem(object obj)
        {
            DataService.DeleteItem(obj);
        }
    }
}
