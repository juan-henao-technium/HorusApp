using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HorusApp.Contracts
{
	public interface IDialogService
	{
		Task ShowConfirmationAsync(string title, string message, string cancelButton = "Aceptar");
		void HideLoading();
		void ShowLoading(string title = null);
	}
}
