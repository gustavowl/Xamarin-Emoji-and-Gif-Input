using Android.Content.Res;
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
	}
}
