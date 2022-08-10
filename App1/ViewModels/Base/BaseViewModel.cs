using App1.Exceptions;
using App1.Models.Base;
using App1.Services.Infrastructure;
using Autofac;
using Reyma.Utils.Http.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace App1.ViewModels.Base
{
    public abstract class BaseViewModel : Bindable
    {
        protected BaseViewModel()
        {
        }

        public string Title
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }


        /// <summary>
        /// Gets or sets the Busy Indicator.
        /// </summary>
        /// <value>The Busy Indicator.</value>
        public bool IsBusy
        {
            get => GetProperty<bool>();
            set => SetProperty(value, () =>
            {
                if (!value)
                    BusyMessage = null;
            });
        }

        /// <summary>
        /// Gets or sets the busy message.
        /// </summary>
        /// <value>The busy message.</value>
        public string BusyMessage
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }

        /// <summary>
        /// 
        /// </summary>
        protected IDialogService Dialog => App.Container.Resolve<IDialogService>();


        #region Executing Busy
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="request"></param>
        /// <param name="function"></param>
        /// <param name="confirmMessage"></param>
        /// <param name="successMessage"></param>
        /// <param name="modalConfig"></param>
        /// <param name="loadingMessage"></param>
        /// <returns></returns>
        public async Task ExecutingBusy<TRequest>(TRequest request, Func<TRequest, Task> function, string confirmMessage = Messages.DefaultConfirmMessage, string successMessage = Messages.DefaultSuccessMessage, string loadingMessage = Messages.PleaseWaitMessage)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            if (!(await ConfirmMessage(confirmMessage)))
                return;

            //var loadingPage = await ShowLoadingPage(modalConfig, loadingMessage);

            try
            {
                await function(request);

                if (!string.IsNullOrWhiteSpace(successMessage))
                    await Dialog.ShowAsync(successMessage, Messages.SuccessTitle, null, "Ok");
            }
            catch (Exception ex)
            {
                await EvaluateException(ex, function.Method.Name);
            }

            IsBusy = false;

            //await DismissLoadingPage(loadingPage);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="request"></param>
        /// <param name="function"></param>
        /// <param name="confirmMessage"></param>
        /// <param name="successMessage"></param>
        /// <param name="modalConfig"></param>
        /// <param name="loadingMessage"></param>
        /// <returns></returns>
        public async Task<TResult> ExecutingBusy<TRequest, TResult>(TRequest request, Func<TRequest, Task<TResult>> function, string confirmMessage = Messages.DefaultConfirmMessage, string successMessage = Messages.DefaultSuccessMessage, string loadingMessage = Messages.PleaseWaitMessage)
        {
            if (IsBusy)
                return default;

            IsBusy = true;

            if (!(await ConfirmMessage(confirmMessage)))
                return default;

            //var loadingPage = await ShowLoadingPage(modalConfig, loadingMessage);

            var response = default(TResult);

            try
            {
                response = await function(request);

                if (!string.IsNullOrWhiteSpace(successMessage))
                    await Dialog.ShowAsync(successMessage, Messages.SuccessTitle, null, "Ok");
            }
            catch (Exception ex)
            {
                await EvaluateException(ex, function.Method.Name);
            }

            IsBusy = false;

            //await DismissLoadingPage(loadingPage);

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="function"></param>
        /// <param name="confirmMessage"></param>
        /// <param name="successMessage"></param>
        /// <param name="modalConfig"></param>
        /// <param name="loadingMessage"></param>
        /// <returns></returns>
        public async Task ExecutingBusy(Func<Task> function, string confirmMessage = Messages.DefaultConfirmMessage, string successMessage = Messages.DefaultSuccessMessage, string loadingMessage = Messages.PleaseWaitMessage)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            if (!(await ConfirmMessage(confirmMessage)))
                return;

            //var loadingPage = await ShowLoadingPage(modalConfig, loadingMessage);

            try
            {
                await function();

                if (!string.IsNullOrWhiteSpace(successMessage))
                    await Dialog.ShowAsync(successMessage, Messages.SuccessTitle, null, "Ok");
            }
            catch (Exception ex)
            {
                await EvaluateException(ex, function.Method.Name);
            }

            IsBusy = false;

            //await DismissLoadingPage(loadingPage);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="function"></param>
        /// <param name="confirmMessage"></param>
        /// <param name="successMessage"></param>
        /// <param name="modalConfig"></param>
        /// <param name="loadingMessage"></param>
        /// <returns></returns>
        public async Task<TResult> ExecutingBusy<TResult>(Func<Task<TResult>> function, string confirmMessage = Messages.DefaultConfirmMessage, string successMessage = Messages.DefaultSuccessMessage, string loadingMessage = Messages.PleaseWaitMessage)
        {
            if (IsBusy)
                return default;

            IsBusy = true;

            if (!(await ConfirmMessage(confirmMessage)))
                return default;

            //var loadingPage = await ShowLoadingPage(modalConfig, loadingMessage);

            var response = default(TResult);

            try
            {
                response = await function();

                if (!string.IsNullOrWhiteSpace(successMessage))
                    await Dialog.ShowAsync(successMessage, Messages.SuccessTitle, null, "Ok");
            }
            catch (Exception ex)
            {
                await EvaluateException(ex, function.Method.Name);
            }

            IsBusy = false;

            //await DismissLoadingPage(loadingPage);

            return response;
        }
        #endregion

        private async Task<bool> ConfirmMessage(string confirmMessage)
        {
            if (!string.IsNullOrWhiteSpace(confirmMessage))
            {
                if (!(await Dialog.ShowAsync(Messages.ConfirmTitle, confirmMessage, Messages.YesButtonText, Messages.NoButtonText)))
                {
                    IsBusy = false;
                    return false;
                }

            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="methodName"></param>
        /// <returns></returns>
        protected virtual async Task EvaluateException(Exception exception, string methodName)
        {
            if (exception is FriendlyException friendlyException)
            {
                //if (friendlyException.TrackException)
                //    await Log(friendlyException.EventName, friendlyException, CriticalityLevel.Low, new { Message = string.Format(Messages.FailsWhileInvokingMessage, methodName) });
            }

            if (exception is InvalidCredentialException)
            {
                await Dialog.ShowAsync(Messages.ErrorTitle,Messages.InvalidCredentialsMessage, Messages.AcceptButtonText, null);
                return;
            }

            if (exception is HttpResponseException httpResponseException)
            {
                //await Log(Messages.CallToWebApi, httpResponseException, CriticalityLevel.High, new { Message = string.Format(Messages.FailsWhileCallingApiMessage, methodName), httpResponseException.JsonResponse, httpResponseException.UrlCalled, httpResponseException.HttpStatusCode });

                if (new HttpStatusCode[] { HttpStatusCode.Forbidden, HttpStatusCode.Unauthorized }.Contains(httpResponseException.HttpStatusCode))
                {
                    await Dialog.ShowAsync(Messages.ErrorTitle, Messages.UserHasNotPermissions,  Messages.AcceptButtonText, null);
                    return;
                }
            }

            var exceptionType = exception.GetType();

            //if (!AbstractionsInstance.IgnoredExceptionsForLogging.Contains(exceptionType) && !new Type[] { typeof(FriendlyException), typeof(HttpResponseException) }.Contains(exceptionType))
            //    await Log(Messages.UnexpectedError, exception, CriticalityLevel.High, new { Message = string.Format(Messages.FailsWhileInvokingMessage, methodName) });

            //if (!AbstractionsInstance.IgnoredExceptionsForLogging.Contains(exceptionType) && !new Type[] { typeof(FriendlyException) }.Contains(exceptionType) && AbstractionsInstance.IsDebugging)
            //    throw exception;

            await Dialog.ShowAsync(Messages.ErrorTitle, exception, Messages.AcceptButtonText);
        }

        private class Messages
        {
            internal const string DefaultConfirmMessage = "";
            internal const string DefaultSuccessMessage = "";
            internal const string PleaseWaitMessage = "Por favor espere...";
            internal const string FailsWhileInvokingMessage = "Fails while invoking the method '{0}'.";
            internal const string FailsWhileCallingApiMessage = "Has failed API Call. Fails while invoking the method '{0}'.";
            internal const string UnexpectedError = "Unexpected Error";
            internal const string SuccessTitle = "Éxito";
            internal const string ErrorTitle = "Error";
            internal const string AcceptButtonText = "Ok";
            internal const string YesButtonText = "Sí";
            internal const string NoButtonText = "No";
            internal const string ConfirmTitle = "Confirmar";
            internal const string CallToWebApi = "Call to Web API";
            internal const string InvalidCredentialsMessage = "Usuario o contraseña incorrecto. Verifique su información.";
            internal const string UserHasNotPermissions = "No cuentas con los permisos suficientes para consumir el recurso web solicitado.";
        }

    }

    public abstract class BaseViewModel<TDataStore> : BaseViewModel where TDataStore : class, IDataStore
    {
        protected BaseViewModel()
        {
        }

        public TDataStore DataStore => App.Container.Resolve<TDataStore>();
    }
}
