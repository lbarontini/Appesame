using Android.Util;
using Appesame.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Appesame
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("Flashcards/addItem", typeof(AddItemView));
            Routing.RegisterRoute("Recordings/addItem", typeof(AddItemView));
            Routing.RegisterRoute("Cmaps/addItem", typeof(AddItemView));
            Routing.RegisterRoute("Exercises/addItem", typeof(AddItemView));
        }
    }
}