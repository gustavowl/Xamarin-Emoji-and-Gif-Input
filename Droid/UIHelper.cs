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

		async void IUIHelper.metodo(Label lb)
		{
			lb.Text = null;
			// Get the latitude and longitude entered by the user and create a query.
			/*string url = "http://api.geonames.org/findNearByWeatherJSON?lat=" + 
				(47.55) + "&lng=" + (-122.31666666666666) + "&username=demo";*/
			string url = "http://api.giphy.com/v1/gifs/search?q=funny+cat&api_key=dc6zaTOxFJmzC";
			// Fetch the weather information asynchronously, 
			// parse the results, then update the screen:
			JsonValue json = await FetchWeatherAsync(url);
			string data = json.ToString();
			data = data.Substring(data.IndexOf('['), data.IndexOf(']') - data.IndexOf('[') + 1);
			//List<string> toreturn = new List<string>();
			string lbtext = "";

			string id_str = "\"id\": \"";
			int fio = data.IndexOf(id_str);
			while (fio >= 0)
			{
				data = data.Substring(fio + id_str.Length);
				//toreturn.Add(data.Substring(0, data.IndexOf('\"')));
				lbtext += data.Substring(0, data.IndexOf('\"')) + '/';
				fio = data.IndexOf(id_str);
			}
			lb.Text = lbtext;
			// ParseAndDisplay (json);
			//return toreturn;
		}

		// Gets weather data from the passed URL.
		private async Task<JsonValue> FetchWeatherAsync(string url)
		{
			// Create an HTTP web request using the URL:
			HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
			request.ContentType = "application/json";
			request.Method = "GET";

			// Send the request to the server and wait for the response:
			using (WebResponse response = await request.GetResponseAsync())
			{
				// Get a stream representation of the HTTP web response:
				using (Stream stream = response.GetResponseStream())
				{
					// Use this stream to build a JSON document object:
					JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
					Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());

					// Return the JSON document:
					return jsonDoc;
				}
			}
		}
	}
}
