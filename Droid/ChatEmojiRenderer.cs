using System;
using Android.Graphics;
using Keyboard;
using Keyboard.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ChatEmojiEntry), typeof(ChatEmojiRenderer))]
namespace Keyboard.Droid
{
	public class ChatEmojiRenderer : ChatTextEntryRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			
			base.OnElementChanged(e);

			if (Control != null)
			{
				Control.ShowSoftInputOnFocus = false;

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
				};

				e.NewElement.Unfocused += delegate {
					var elem = (ChatEmojiEntry)e.NewElement;
					elem.CursorFocus = false;
				};
			}
		}
	}
}