using System;
using Android.Content;
using Android.Text;
using Android.Util;
using Android.Widget;

using Android.Text.Style;

namespace Keyboard
{
	public class EmojiEditText : EditText
	{
		SpannableString spanstr;
		EmojiHandler eh;
		string prv;

		public EmojiEditText(Context cont) : base(cont)
		{
			_init(cont);
		}

		public EmojiEditText(Context cont, IAttributeSet ias) : base (cont, ias)
		{
			_init(cont);
		}

		private void _init(Context cont)
		{
			spanstr = new SpannableString("");
			prv = "";
			eh = new EmojiHandler(cont);
			this.TextChanged += OnTextChanged;
		}

		public void OnTextChanged(object sender, TextChangedEventArgs e)
		{
			//The condition check should be redundant for ontextchanged event.
			//However, it is not because Xamarin.
			if (prv != Text)
			{
				spanstr = eh.UpdateSpan(prv, Text, spanstr, e);
				prv = Text;
				TextFormatted = spanstr;
				//txt.SetSelection(System.Math.Max(0, index));
				SetSelection(e.Start + e.AfterCount);
			}
		}

	}
}
