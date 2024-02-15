using Acr.UserDialogs;
using HorusApp.Contracts;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HorusApp.Services
{
	public class DialogService : IDialogService
	{
		private readonly IPageDialogService pageDialogService;

		public DialogService(IPageDialogService pageDialogService)
		{
			this.pageDialogService = pageDialogService;
		}

		public Task ShowConfirmationAsync(string title, string message, string cancelButton = "Aceptar")
		{
			return pageDialogService.DisplayAlertAsync(title, message, cancelButton);
		}

		public void ShowLoading(string title = null)
		{
			try
			{
				if (string.IsNullOrEmpty(title))
					title = "Cargando por favor espere...";

				UserDialogs.Instance.ShowLoading(title, MaskType.Black);
			}
			catch 
			{
				//Do nothing
			}
		}

		public void HideLoading()
		{
			UserDialogs.Instance.HideLoading();
		}
	}
}
