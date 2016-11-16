using System;
using Android.Graphics;
using Keyboard;
using Keyboard.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ChatEmojiEntry), typeof(MyEntryRenderer))]
namespace Keyboard.Droid
{
	public class MyEntryRenderer : CustomFontEntryRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			
			base.OnElementChanged(e);

			if (Control != null)
			{
				Control.ShowSoftInputOnFocus = false;

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

				e.NewElement.PropertyChanged += (sender, p) =>
				{
					if (p.PropertyName == "CursorFocus")
					{
						var elem = (ChatEmojiEntry)e.NewElement;
						if (elem.CursorFocus)
						{
							Control.RequestFocus();
							Control.SetSelection(elem.FocusOnPosition);
						}
					}
					else if (p.PropertyName == "FocusOnPosition")
					{
						var elem = (ChatEmojiEntry)e.NewElement;
						Control.SetSelection(elem.FocusOnPosition);
					}
					/*if (p.PropertyName != "Text")
					{
						e.NewElement.Text = p.PropertyName;
					}*/
					//e.NewElement
				};

				e.NewElement.Unfocused += delegate {
					var elem = (ChatEmojiEntry)e.NewElement;
					elem.CursorFocus = false;
				};
			}
		}
	}
}