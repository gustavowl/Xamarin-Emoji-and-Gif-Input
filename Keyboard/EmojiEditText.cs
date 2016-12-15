using System;
using Android.Content;
using Android.Text;
using Android.Widget;

namespace Keyboard
{
	public class EmojiEditText : EditText
	{
		SpannableString spanstr;

		public EmojiEditText(Context cont) : base(cont)
		{
			spanstr = new SpannableString("");
			this.TextChanged += OnTextChanged;
		}

		public void OnTextChanged(object sender, TextChangedEventArgs e)
		{
			EmojiHandler.UpdateSpan();
		}

	}
}
