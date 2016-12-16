using Android.App;

namespace Keyboard
{
	[Activity(Label = "Emoji")]
	public class Emoji
	{
		private string Shortname;
		private int Unicode;
		Android.Graphics.Drawables.Drawable Image;

		public Emoji (string shortname) {
			this.Shortname = shortname;
			this.Unicode = 0;
		}

		public Emoji(string shortname, int unicode, Android.Graphics.Drawables.Drawable d)
		{
			Shortname = shortname;
			Unicode = unicode;
			Image = d;
		}


		public string getEmojiShortname() { return Shortname; }
		/*
		public void setEmojiShortname(string shortname) { Shortname = shortname; }

		public int getEmojiUnicode() { return Unicode; }

		public static bool operator == (Emoji a, Emoji b)
		{
			if (ReferenceEquals(a, null))
				return false;
			if (ReferenceEquals(b, null))
				return false;
			if (ReferenceEquals(a, b))
				return true;
			return (a.Shortname.Equals(b.Shortname) || a.Unicode == b.Unicode);
		}

		public static bool operator !=(Emoji a, Emoji b)
		{
			return !(a == b);
		}

		public bool Equals(Emoji em)
		{
			return this == em;
		}*/

	}
}
