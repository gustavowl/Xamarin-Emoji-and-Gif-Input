using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Graphics;
using Keyboard.Droid;
using Android.Views.InputMethods;
using Android.App;
using Android.Content;
using Keyboard;

[assembly: ExportRenderer(typeof(EmojiEntry), typeof(EmojiEntryRenderer))]
namespace Keyboard.Droid
{
	public class EmojiEntryRenderer : EntryRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);

			if (Control != null)
			{
				//var nativeEditText = (global::Android.Widget.EditText)Control;
				//if (e.NewElement.ClassId == "InputEmoji")
				//{
				e.NewElement.Focused += delegate
				{
						/*
						if (e.NewElement.ClassId == "InputText")
						{
							e.NewElement.Text = "KEYBOARD SHOULD APPEAR";
						}*/
   					//var nativeEditText = (global::Android.Widget.EditText)Control;
					//nativeEditText.cli
					while (!e.NewElement.Focus()) { }
					e.NewElement.Text = "KEYBOARD SHOULD DISAPPEAR";
					IUIHelper hlp = DependencyService.Get<IUIHelper>();
					hlp.HideKeyboard();
						/*
							var inputMethodManager = (InputMethodManager)Forms.Context.GetSystemService(Context.InputMethodService);
							if (inputMethodManager != null && Forms.Context is Activity)
							{
								var activity = Forms.Context as Activity;
								var token = activity.CurrentFocus == null ? null : activity.CurrentFocus.WindowToken;


								inputMethodManager.HideSoftInputFromWindow(activity.CurrentFocus.WindowToken, 0);
							}*/
						/*string k = e.NewElement.IsFocused.ToString();
						k += '\t' + e.NewElement.ClassId;
						k += ';';*/
						/*
						else {
							e.NewElement.Text = "SOU KUWABARA!";
						}*/
				};
				//}
			}

			/*if (e.NewElement.IsFocused)
			{
				var inputMethodManager = (InputMethodManager)Forms.Context.GetSystemService(Context.InputMethodService);
				if (inputMethodManager != null && Forms.Context is Activity)
				{
					var activity = Forms.Context as Activity;
					var token = activity.CurrentFocus == null ? null : activity.CurrentFocus.WindowToken;
					inputMethodManager.HideSoftInputFromWindow(activity.CurrentFocus.WindowToken, 0);
				}
			}*/
		}

	}
}