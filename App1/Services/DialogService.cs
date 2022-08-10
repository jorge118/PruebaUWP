using App1.Models;
using App1.Services.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace App1.Services
{
    public class DialogService : IDialogService
    {
        public async Task ShowAsync(string title, Exception ex, string ok = "Ok")
        {
            await ShowAsync(title, ex.Message, ok);
        }

        public async Task ShowAsync(string title, string content, string ok = "Ok")
        {
            await ShowAsync(title, content, null, "Ok");
        }

        public async Task<bool> ShowAsync(string title, string content, string ok = null, string cancel = null)
        {
            var dialog = new ContentDialog
            {
                Title = title,
                Content = content,
                PrimaryButtonText = ok != null ? ok : string.Empty,
            };

            if (!string.IsNullOrEmpty(cancel))
                dialog.CloseButtonText = cancel;

            var result = await dialog.ShowAsync();
            return result == ContentDialogResult.Primary;
        }
    }
}
