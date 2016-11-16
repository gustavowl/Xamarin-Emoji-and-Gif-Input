using System.ComponentModel;

namespace Keyboard
{
	public class ChatEmojiEntry : ChatTextEntry
	{
		private bool cursorFocus;
		public event PropertyChangedEventHandler ev;

		public ChatEmojiEntry()
		{
			this.Keyboard = null;
			this.InputTransparent = true;
		}

		public bool CursorFocus
		{
			set { cursorFocus = value; OnPropertyChanged("CursorFocus"); }
			get { return cursorFocus; }
		}
	}
}