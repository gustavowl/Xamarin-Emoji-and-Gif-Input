
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Keyboard
{
	[Activity(Label = "Emoji")]
	public class Emoji
	{
		private string emoji;

		public Emoji (string emoji) {
			this.emoji = emoji;
		}

		public string getEmoji() { return emoji; }
	}
}
