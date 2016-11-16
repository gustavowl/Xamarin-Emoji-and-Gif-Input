using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Graphics;
using Keyboard.Droid;
using Android.Views.InputMethods;
using Android.App;
using Android.Content;
using Android.OS;
using Keyboard;

[assembly: ExportRenderer(typeof(ChatTextEntry), typeof(CustomFontEntryRenderer))]
namespace Keyboard.Droid
{
	public class CustomFontEntryRenderer : EntryRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);

			if (!string.IsNullOrEmpty(e.NewElement?.FontFamily))
			{
				try
				{
					var font = Typeface.CreateFromAsset(Forms.Context.ApplicationContext.Assets, e.NewElement.FontFamily + "-android.ttf");
					Control.Typeface = font;
				}
				catch (Exception ex)
				{
					// An exception means that the custom font wasn't found.
					// Typeface.CreateFromAsset throws an exception when it didn't find a matching font.
					// When it isn't found we simply do nothing, meaning it reverts back to default.
				}
			}

			if (Control != null)
			{
				e.NewElement.Focused += delegate
				{
					var elem = (ChatTextEntry)e.NewElement;
					if (!elem.WasPreviouslyFocused)
					{
						Control.SetSelection(elem.FocusOnPosition);
						elem.WasPreviouslyFocused = true;
					}
					else
						elem.FocusOnPosition = Control.SelectionStart;
				};

				e.NewElement.Unfocused += delegate {
					var elem = (ChatTextEntry)e.NewElement;
					elem.FocusOnPosition = Control.SelectionStart;					
				};

				/*
				if (e.NewElement.ClassId == "InputEmoji")
				{
					
					Control.ShowSoftInputOnFocus = false;

					e.NewElement.Focused += delegate
					{
						/*
						var inputMethodManager = (InputMethodManager)Forms.Context.GetSystemService(Context.InputMethodService);
						if (inputMethodManager != null && Forms.Context is Activity)
						{
							var activity = Forms.Context as Activity;
							var token = activity.CurrentFocus == null ? null : activity.CurrentFocus.WindowToken;

							inputMethodManager.HideSoftInputFromWindow(activity.CurrentFocus.WindowToken, 0);
						}*
					};
				}

				else if (e.NewElement.ClassId == "InputText") {
					e.NewElement.Focused += delegate {
						Control.Text += Control.IsFocused.ToString() + ' ' + Control.HasFocus + " / ";
					};
				}*/
			}
		}

	}

}