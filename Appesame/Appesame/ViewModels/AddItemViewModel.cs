using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Appesame.ViewModels
{
    public class AddItemViewModel : BaseViewModel
    {
        public Command GoBackCommand { get; set; }

        public AddItemViewModel()
        {
            GoBackCommand = new Command(async () => await GoBack());
        }
        private async Task GoBack()
        {
            await Shell.Current.GoToAsync("//Tabbar", true);
        }
    }
}
