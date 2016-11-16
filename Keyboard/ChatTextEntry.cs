using System.ComponentModel;
using Xamarin.Forms;

namespace Keyboard
{
	public class ChatTextEntry : Entry
	{
		private int focusOnPosition;
		public bool WasPreviouslyFocused;

		public ChatTextEntry()
		{
			WasPreviouslyFocused = false;
		}

		public int FocusOnPosition
		{
			set { focusOnPosition = value; OnPropertyChanged("FocusOnPosition"); }
			get { return focusOnPosition; }
		}
	}
}