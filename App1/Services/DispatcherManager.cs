using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.ViewManagement;

namespace App1.Services
{
    public class DispatcherManager
    {
        public int CurrentWindowId { get; set; }

        public DispatcherManager(int windowId)
        {
            CurrentWindowId = windowId;
        }


        public async Task AwaitableRunAsync(Action function)
        {
            foreach (var iteratingview in CoreApplication.Views)
            {
                await iteratingview.Dispatcher.TryRunAsync(CoreDispatcherPriority.Normal , () =>
                {
                    if (ApplicationView.GetForCurrentView().Id == CurrentWindowId)
                    {
                        function.Invoke();
                    }
                });
            }
        }

        public async Task AwaitableRunAsync(Func<Task> function)
        {
            foreach (var iteratingview in CoreApplication.Views)
            {
                await iteratingview.Dispatcher.TryRunAsync(CoreDispatcherPriority.Normal,async () =>
                {
                    if (ApplicationView.GetForCurrentView().Id == CurrentWindowId)
                    {
                        await function.Invoke();
                    }
                });
            }

        }
    }
}
