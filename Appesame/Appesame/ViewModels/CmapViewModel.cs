﻿
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
    public class CmapViewModel : BaseViewModel
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

        public IEnumerable<CmapModel> CmapModelList { get; set; }
        public ICommand GoBackCommand { get; set; }
        public ICommand OnAppearingCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand ItemTappedCommand { get; set; }
        public Command<object> DeleteCommand { get; set; }

        public CmapViewModel()
        {
            OnAppearingCommand = new Command(OnAppearing);
            GoBackCommand = new Command(async () => await GoBack());
            AddCommand = new Command(async () => await AddItem());
            ItemTappedCommand = new Command<CmapModel>(async (x) => await OnItemSelectedAsync(x));
            DeleteCommand = new Command<object>(DeleteItem);
        }
        private void OnAppearing()
        {
            ExamName = Preferences.Get("CurrentExam", "Cmaps");
            CmapModelList = DataService.GetAllItems("Cmap", ExamName) as IEnumerable<CmapModel>;
            OnPropertyChanged("CmapModelList");
        }
        private async Task GoBack()
        {
            await Shell.Current.GoToAsync("//ExamChooser", true);
        }
        private async Task AddItem()
        {
            await Shell.Current.GoToAsync($"Cmaps/addItem?ItemName=Cmap");
        }
        private async Task OnItemSelectedAsync(CmapModel x)
        {
            try
            {
                await Launcher.OpenAsync(new OpenFileRequest
                {
                    File = new ReadOnlyFile(x.Uri)
                    {
                        ContentType = "image/*"
                    }
                });
            }
            catch
            {
                await App.Current.MainPage.DisplayAlert("Error", "the file must be misplaced or deleted", "OK");
            }
        }
        private void DeleteItem(object obj)
        {
            DataService.DeleteItem(obj, "Cmap");
        }
    }
}