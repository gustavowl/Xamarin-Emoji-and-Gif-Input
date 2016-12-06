using Android.Content.Res;
using Xamarin.Forms;
using System.Json;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System;
using System.Collections.Generic;

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

		float IUIHelper.ReturnScreenWidth()
		{
			return res.DisplayMetrics.WidthPixels / res.DisplayMetrics.Density;
		}
	}
}
