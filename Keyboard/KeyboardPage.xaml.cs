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

			var EmojiCells = EmCat1.Children;
			emoj_categ1.Text = "\uD83D\uDE00";
			char hex1 = (char)int.Parse("D83DDE00".Substring(0, 4), System.Globalization.NumberStyles.HexNumber);
			char hex2 = (char)int.Parse("D83DDE00".Substring(4, 4), System.Globalization.NumberStyles.HexNumber);

			foreach (Button ec in EmojiCells)
			{
				ec.FontFamily = "emojione";
				ec.Clicked += InsertEmoji;
				ec.Text = "" + hex1 + hex2;
				hex2++;
			}

			EmojiCells = EmCat2.Children;
			emoj_categ2.Text = "\uD83D\uDE80";
			hex1 = (char)int.Parse("D83DDE80".Substring(0, 4), System.Globalization.NumberStyles.HexNumber);
			hex2 = (char)int.Parse("D83DDE80".Substring(4, 4), System.Globalization.NumberStyles.HexNumber);

			foreach (Button ec in EmojiCells)
			{
				ec.FontFamily = "emojione";
				ec.Clicked += InsertEmoji;
				ec.Text = "" + hex1 + hex2;
				hex2++;
			}

			/*wv1_1.WidthRequest = wv1_0.WidthRequest = wv0_1.WidthRequest = wv0_0.WidthRequest = 200;
			wv1_1.HeightRequest = wv1_0.HeightRequest = wv0_1.HeightRequest = wv0_0.HeightRequest = 200;*/

			//rltv.HeightRequest = 200 * 25;
			rltv.WidthRequest = hlp.ReturnScreenWidth();


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
				/*
				rltv.Children.Add(wv, Constraint.RelativeToParent((parent) =>
				{
					return (.5 * parent.Width) - 100;
				}),
				Constraint.RelativeToParent((parent) =>
				{
					return (.5 * parent.Height) - 100 + 200 * i;
				}),
                Constraint.Constant(50), Constraint.Constant(50));*/
			}
		}

		void Input_TextChanged(object sender, TextChangedEventArgs e)
		{
			var s1 = e.OldTextValue;
			var s2 = e.NewTextValue;
			string tst = e.NewTextValue.Replace(' ', '\u2000');
			if (e.NewTextValue != tst)
			{
				Entry entry = (Entry)sender;
				entry.Text = tst;
			}
			else 
			{
				LaBela.Text = tst;
				LaBela2.Text = tst;
			}
		}

		void ShowEmojisTable(object sender, EventArgs e)
		{
			emoji_table.IsVisible = !emoji_table.IsVisible;
			input_text.WasPreviouslyFocused = false;
			input_emoji.WasPreviouslyFocused = false;

			if (emoji_table.IsVisible)
			{
				input_text.IsVisible = false;
				input_emoji.IsVisible = true;
				input_emoji.Text = input_text.Text;
				input_emoji.FocusOnPosition = input_text.FocusOnPosition;
				input_emoji.CursorFocus = true;
				scroll.HeightRequest = screenheight / 2;
				emoji_table.HeightRequest = screenheight - scroll.Height - input_emoji.Height;

				if (!ScrollEmCat1.IsVisible && !ScrollEmCat2.IsVisible)
				{
					ShowEmojiCategory((object)emoj_categ1, null);
				}

			}
			else {
				input_emoji.IsVisible = false;
				input_text.IsVisible = true;
				input_text.Text = input_emoji.Text;
				input_text.FocusOnPosition = input_emoji.FocusOnPosition;
				input_text.Focus();
				scroll.HeightRequest = screenheight - input_text.Height;
			}
		}

		void ShowEmojiCategory(object sender, EventArgs e)
		{
			var sndr = (Button)sender;
			if (sndr == emoj_categ1)
			{
				ScrollEmCat1.IsVisible = true;
				ScrollEmCat2.IsVisible = false;
			}
			else if (sndr == emoj_categ2)
			{
				ScrollEmCat1.IsVisible = false;
				ScrollEmCat2.IsVisible = true;
			}
			input_emoji.CursorFocus = true;
		}

		void InsertEmoji(object sender, EventArgs e)
		{
			Button b = (Button)sender;
			char[] inpt = b.Text.ToCharArray();
			int prev_focus = input_emoji.FocusOnPosition;
			string text = input_emoji.Text;
			if (text != null)
			{
				input_emoji.Text = text.Substring(0, input_emoji.FocusOnPosition);
				input_emoji.Text += "" + inpt[0] + "" + inpt[1];
				input_emoji.Text += text.Substring(input_emoji.FocusOnPosition);
			}
			else
				input_emoji.Text += "" + inpt[0] + "" + inpt[1];
			input_emoji.CursorFocus = true;
			input_emoji.FocusOnPosition = prev_focus + 2;
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
					}
					/*
					giphy.WidthRequest = 200;
					giphy.HeightRequest = 200;
					giphy.Source = sources[r.Next(0, sources.Length)];*/
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
			//var split = id.Split('/');
			//giphy.Unfocus();
		}

	}
}
