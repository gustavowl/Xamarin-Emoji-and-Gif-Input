using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace Keyboard
{
	public partial class KeyboardPage : ContentPage
	{
		float screenheight;

		public KeyboardPage()
		{
			InitializeComponent();
			//this is necessary since the conde for screen size depends on the platform
			IUIHelper hlp = DependencyService.Get<IUIHelper>();
			screenheight = hlp.ReturnScreenHeight();

			for (int i = 0; i < 25; i++)
			{

				var wv = new WebView();
				wv.AutomationId = "wv" + i / 2 + "_" + i % 2;
				wv.Source = "https://uwaterloo.ca/events/events/bookstore-concourse-sale-1";
				wv.Focused += webviewfocused;
				wv.WidthRequest = 200;
				wv.HeightRequest = 200;

				Point p = new Point();
				p.Y = 0;
				p.X = 200 * i;

				rltv.Children.Add(wv, p);
			}
		}

		void SearchGifs(object sender, EventArgs e)
		{
			
			if (giphy_search.Text != null)
			{
				string search_input = giphy_search.Text.Trim();
				if (search_input != null)
				{
					bigger_giphy.IsVisible = false;
					rltv.IsVisible = true;
					abslt_prnt.IsVisible = true;
					search_input = search_input.Replace(' ', '+');
					IUIHelper hlp = DependencyService.Get<IUIHelper>();
					hlp.metodo(search_input, test);
				}
			}
		}

		void ShowOrHideGifSearchTool(object sender, EventArgs e)
		{
			giphy_search.IsVisible = !giphy_search.IsVisible;
			emoji_button.IsVisible = !emoji_button.IsVisible;
			search_button.IsVisible = !search_button.IsVisible;
			if (giphy_search.IsVisible)
			{
				emoji_table.IsVisible = false;
				input_emoji.IsVisible = false;
				input_text.IsVisible = false;
				giphy_search.Focus();
			}
			else {
				input_text.IsVisible = true;
				input_text.Focus();
			}
		}

		void teste_prop_changed(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "Text")
			{
				Label lb = (Label)sender;
				if (lb.Text != null && lb.Text.Contains("/"))
				{
					if (lb.BackgroundColor == Color.Green)
						lb.BackgroundColor = Color.Yellow;
					else
						lb.BackgroundColor = Color.Green;
					string[] sources = lb.Text.Split('\t');

					int i = 0;
					foreach (WebView view in rltv.Children)
					{
						string url = sources[i];
						view.Source = url;
						view.ClassId = url.Split('/')[4];
						i++;
						view.WidthRequest = Convert.ToInt32(sources[i]);
						i++;
					}
				}

			}
		}

		void webviewfocused(object sender, EventArgs e)
		{
			WebView wb = (WebView)sender;
			rltv.IsVisible = false;
			abslt_prnt.IsVisible = false;
			bigger_giphy.IsVisible = true;
			bigger_giphy.WidthRequest = 500;
			bigger_giphy.HeightRequest = 500;
			bigger_giphy.Source = "http://media2.giphy.com/media/" + wb.ClassId + "/giphy.gif";
		}

	}
}
