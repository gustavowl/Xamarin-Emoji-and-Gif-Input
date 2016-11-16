using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Graphics;
using Keyboard.Droid;

[assembly: ExportRenderer(typeof(Button), typeof(EmojiButtonRenderer))]
namespace Keyboard.Droid
{
	public class EmojiButtonRenderer : ButtonRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
		{
			base.OnElementChanged(e);

			if (Control != null)
			{
				if (!string.IsNullOrEmpty(e.NewElement?.FontFamily))
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
			}
		}
	}
}
