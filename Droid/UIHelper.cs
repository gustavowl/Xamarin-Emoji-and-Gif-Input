using System;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Views.InputMethods;
using Xamarin.Forms;

[assembly: Dependency(typeof(Keyboard.Droid.UIHelper))]
namespace Keyboard.Droid
{
	public class UIHelper : IUIHelper
	{
		static Resources res;

		public UIHelper()
		{
		}

		public UIHelper(Resources r)
		{
			res = r;
		}

		float IUIHelper.ReturnScreenHeight()
		{
			return res.DisplayMetrics.HeightPixels / res.DisplayMetrics.Density;
		}

		void IUIHelper.HideKeyboard()
		{
			/*
			var inputMethodManager = (InputMethodManager) Forms.Context.GetSystemService(Context.InputMethodService);
			if (inputMethodManager != null && Forms.Context is Activity)
			{
				var activity = Forms.Context as Activity;
				var token = activity.CurrentFocus == null ? null : activity.CurrentFocus.WindowToken;
				inputMethodManager.HideSoftInputFromWindow(token, HideSoftInputFlags.None);
				return activity.CurrentFocus.ToString();
			}*/
			//while (entry.Focus()) { }

			//entry.Text = "KEYBOARD SHOULD DISAPPEAR";
			/*
			var inputMethodManager = (InputMethodManager)Forms.Context.GetSystemService(Context.InputMethodService);
			if (inputMethodManager != null && Forms.Context is Activity)
			{
				var activity = Forms.Context as Activity;
				var token = activity.CurrentFocus == null ? null : activity.CurrentFocus.WindowToken;
				System.Threading.Thread.Sleep(2000);
				inputMethodManager.HideSoftInputFromWindow(activity.CurrentFocus.WindowToken, 0);
			}/*
			var inputMethodManager = (InputMethodManager)Forms.Context.GetSystemService(Context.InputMethodService);
			if (inputMethodManager != null && Forms.Context is Activity)
			{
				var activity = Forms.Context as Activity;
				var token = activity.CurrentFocus == null ? null : activity.CurrentFocus.WindowToken;

				inputMethodManager.HideSoftInputFromWindow(activity.CurrentFocus.WindowToken, 0);
			}*/
		}
	}
}
