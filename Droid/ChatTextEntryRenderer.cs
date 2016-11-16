using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Graphics;
using Keyboard.Droid;
using Keyboard;

[assembly: ExportRenderer(typeof(ChatTextEntry), typeof(ChatTextEntryRenderer))]
namespace Keyboard.Droid
{
	public class ChatTextEntryRenderer : EntryRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);

			if (Control != null)
			{
				if (!string.IsNullOrEmpty(e.NewElement.FontFamily))
				{
					try
					{
						var font = Typeface.CreateFromAsset(Forms.Context.ApplicationContext.Assets, e.NewElement.FontFamily + "-android.ttf");
						Control.Typeface = font;
					}
					catch (Exception excpt)
					{
						//thrown when the requested font is not found
					}
				}

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
			}
		}

	}

}