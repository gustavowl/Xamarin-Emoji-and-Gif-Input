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

	}
}
