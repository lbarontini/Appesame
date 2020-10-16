
using Appesame.Data;
using Appesame.Models;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Appesame.ViewModels
{
    public class FlashcardViewModel : BaseViewModel
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

        public IEnumerable<FlashcardModel> FlashcardModelList { get; set; }
        public ICommand GoBackCommand { get; set; }
        public ICommand OnAppearingCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand ItemTappedCommand { get; set; }
        public Command<object> DeleteCommand { get; set; }

        public FlashcardViewModel()
        {
            OnAppearingCommand = new Command(OnAppearing);
            GoBackCommand = new Command(async () => await GoBack());          
            AddCommand = new Command(async () => await AddItem());
            ItemTappedCommand = new Command<FlashcardModel>(async (x) => await OnItemSelectedAsync(x));
            DeleteCommand = new Command<object>(DeleteItem);
        }
        private void OnAppearing()
        {
            //the name of the exam is retrived from the Preferences
            ExamName = Preferences.Get("CurrentExam", "Flashcards");
            FlashcardModelList = DataService.GetAllItems("Flashcard", ExamName) as IEnumerable<FlashcardModel>;
            OnPropertyChanged("FlashcardModelList");
        }
        private async Task GoBack()
        {
            await Shell.Current.GoToAsync("//ExamChooser", true);
        }
        private async Task AddItem()
        {
            //passing item name as parameter when navigating to addItem page 
            await Shell.Current.GoToAsync($"Flashcards/addItem?ItemName=Flashcard");
        }
        private async Task OnItemSelectedAsync(FlashcardModel x)
        {
            //opening the row corresponding file and handling the exception
            try
            {
                if (await Launcher.CanOpenAsync(x.Uri))
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
            catch
            {
                await App.Current.MainPage.DisplayAlert("Error", "the file must be misplaced or deleted", "OK");
            }
        }
        private void DeleteItem(object obj)
        {
            DataService.DeleteItem(obj, "Flashcard");
        }
    }
}
