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

		async void IUIHelper.metodo(string search_str, Label lb)
		{
			// Get the latitude and longitude entered by the user and create a query.
			/*string url = "http://api.geonames.org/findNearByWeatherJSON?lat=" + 
				(47.55) + "&lng=" + (-122.31666666666666) + "&username=demo";*/
			//string url = "http://api.giphy.com/v1/gifs/search?q=funny+cat&api_key=dc6zaTOxFJmzC";
			string url = "http://api.giphy.com/v1/gifs/search?q=" + search_str + "&api_key=dc6zaTOxFJmzC";
			// Fetch the weather information asynchronously, 
			// parse the results, then update the screen:
			JsonValue json = await FetchWeatherAsync(url);
			string data = json.ToString();
			data = data.Substring(data.IndexOf('['), data.IndexOf(']') - data.IndexOf('[') + 1);
			//List<string> toreturn = new List<string>();
			string lbtext = "";

			string id_str = "}}";

			data = data.Substring(data.IndexOf("images\": {") + 10);
			int fio = data.IndexOf(id_str);
			string image, larger_dim;

			while (fio >= 0)
			{
				image = data.Substring(0, data.IndexOf(id_str) + id_str.Length);
				data = data.Remove(0, data.IndexOf(id_str) + id_str.Length);
				//detects which dimensions is the largest
				if (GetDimension(image, true) > GetDimension(image, false))
					larger_dim = "height";
				else
					larger_dim = "width";
				image = image.Substring(image.IndexOf("fixed_" + larger_dim + "_downsampled"));
				image = image.Substring(image.IndexOf("url\": \"") + 7);
				lbtext += image.Substring(0, image.IndexOf('\"')) + '\t';
				/*
				if (fio < data.IndexOf("images\": {"))
				{
					if (fio < data.IndexOf('}'))
					{
						data = data.Substring(data.IndexOf("url\": \"") + 7);
						if (size > -1)
						{
							temp_url = data.Substring(0, data.IndexOf('\"'));
							data = data.Substring(data.IndexOf(id_str) + id_str.Length);
							temp_size = Convert.ToInt32(data.Substring(0, data.IndexOf('\"')));
							if (temp_size < size)
							{
								url = temp_url;
								size = temp_size;
							}
						}
						else {
							url = data.Substring(0, data.IndexOf('\"'));
							data = data.Substring(data.IndexOf(id_str) + id_str.Length);
							size = Convert.ToInt32(data.Substring(0, data.IndexOf('\"')));
						}
					}
					else
						data = data.Substring(data.IndexOf('}') + 1);
				}
				else {
					lbtext += url + "\t";
					size = -1;
					if (data.IndexOf("images\": {") >= 0)
						data = data.Substring(data.IndexOf("images\": {") + 10);
					else
						data = "";
				}
				*/
				fio = data.IndexOf(id_str);
			}
			lb.Text = lbtext;
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

		private int GetDimension(string imageInfo, bool height)
		{
			//height == true if to return height's dimension. False to return width's dimension
			string dim;
			if (height)
				dim = "height\": \"";
			else
				dim = "width\": \"";
			int length = imageInfo.IndexOf(dim) + dim.Length;
			imageInfo = imageInfo.Substring(length);

			//DELETE THIS
			int toreturn = Convert.ToInt32(imageInfo.Substring(0, imageInfo.IndexOf('\"')));
			return toreturn;
		}
	}
}
