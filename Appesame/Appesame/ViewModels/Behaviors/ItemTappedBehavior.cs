
using System.Windows.Input;
using Xamarin.Forms;

namespace Appesame.ViewModels.Behaviors
{
    //class for launching a command when an item of the listview is tapped
    public class ItemTappedBehavior : Behavior<ListView>
    {
        public static readonly BindableProperty CommandProperty = 
            BindableProperty.Create("Command", 
                typeof(ICommand),
                typeof(ItemTappedBehavior));

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        protected override void OnAttachedTo(ListView bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.ItemTapped += Bindable_ItemTapped;
            bindable.BindingContextChanged += Bindable_BindingContextChanged;
        }

        private void Bindable_BindingContextChanged(object sender, System.EventArgs e)
        {
            var lv = sender as ListView;
            BindingContext = lv.BindingContext;
        }

        private void Bindable_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return;
            }
            if (Command != null && Command.CanExecute(e.Item))
            {
                Command.Execute(e.Item);
            }
        }

        protected override void OnDetachingFrom(ListView bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.ItemTapped -= Bindable_ItemTapped;
            bindable.BindingContextChanged -= Bindable_BindingContextChanged;
        }
    }
}
