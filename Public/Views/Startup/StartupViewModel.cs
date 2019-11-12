using System;
using System.Threading;
using System.Threading.Tasks;
using Common.ViewModels;
using Public.Views.ConfirmationDialog;

namespace Public.Views
{
    public class StartupViewModel : BaseConductorOneActive<IViewModel>, IViewModel
    {
        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            var t = base.OnInitializeAsync(cancellationToken);

            
            
            return t;
        }


        public async void DisplayConfirmationDialog()
        {
            ConfirmationDialogViewModel vm = new ConfirmationDialogViewModel("Chcete provést další měření?");
            vm.ConfirmationButtonCaption = "Ano";
            vm.CancelButtonCaption = "Ne";
            vm.OnConfirmationClicked += () =>
            {
                Console.WriteLine("YES");
            };
            vm.OnCancelClicked += () =>
            {
                Console.WriteLine("NO");
            };

            var token = LocalOverlay.DisplayOverlay(vm);

            bool result = await vm.WaitForConfirmationAsync(token);
        }
    }
}