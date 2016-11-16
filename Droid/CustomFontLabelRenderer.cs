using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Graphics;
using Keyboard.Droid;

[assembly: ExportRenderer(typeof(Label), typeof(CustomFontLabelRenderer))]
namespace Keyboard.Droid
{
	public class CustomFontLabelRenderer : LabelRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
		{
			base.OnElementChanged(e);

			if (!string.IsNullOrEmpty(e.NewElement?.FontFamily))
			{
				try
				{
					var font = Typeface.CreateFromAsset(Forms.Context.ApplicationContext.Assets, e.NewElement.FontFamily + ".ttf");
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