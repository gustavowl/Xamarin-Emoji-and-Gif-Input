using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views.InputMethods;
using Android.Content;
using Android.Text;
using Android.Text.Style;

namespace Keyboard
{
	[Activity(Label = "Keyboard", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		int count = 1;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button emoji_button = FindViewById<Button>(Resource.Id.emoji_button);
			emoji_button.Click += ShowEmojisTable;

			emoji_button = FindViewById<Button>(Resource.Id.teste);
			emoji_button.Click += delegate {
				var txt = FindViewById<EditText>(Resource.Id.input_text);
				string k = txt.Text;
				SpannableString ss = new SpannableString(txt.Text);
				Android.Graphics.Drawables.Drawable d = GetDrawable(Resource.Drawable._1f1e71f1f7);
				d.SetBounds(0, 0, d.IntrinsicWidth, d.IntrinsicHeight);
				ImageSpan spam = new ImageSpan(d, SpanAlign.Baseline);
				ss.SetSpan(spam, 3, 5, SpanTypes.InclusiveInclusive);
				txt.TextFormatted = ss;
				//txt.SetText(ss);
				//txt.SetSelection(txt.Text.Length);

			};
		}

		public void ShowEmojisTable(object sender, System.EventArgs e)
		{
			var txt = FindViewById<EditText>(Resource.Id.input_text);
			txt.Text = "test";
			txt.SetSelection(txt.Text.Length);
			InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
			imm.HideSoftInputFromWindow(txt.WindowToken, 0);

		}
	}
}

