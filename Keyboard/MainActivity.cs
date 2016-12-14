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

		//Emoji[] data = { new Emoji("aaa"), new Emoji("bbb"), new Emoji("ccc"), new Emoji("ddd"), new Emoji("eee"), new Emoji("fff"),
		//	new Emoji("ggg"), new Emoji("hhh"), new Emoji("iii"), new Emoji("jjj"), new Emoji("lll"), new Emoji("mmm"), new Emoji("nnn"),
		//	new Emoji("ooo"), new Emoji("ppp"), new Emoji("qqq"), new Emoji("rrr"), new Emoji("sss"), new Emoji("ttt"), new Emoji("uuu"),
		//	new Emoji("vvv"), new Emoji("xxx"), new Emoji("zzz")};

		Emoji[] data = PopulateData(-1);

		//string[] data = { "aaa", "bbb", "ccc" };

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button emoji_button = FindViewById<Button>(Resource.Id.emoji_button);
			emoji_button.Click += ShowEmojisTable;

			var txt = FindViewById<EditText>(Resource.Id.input_text);
			SpannableString ss = new SpannableString(txt.Text);
			emoji_button = FindViewById<Button>(Resource.Id.teste);
			Android.Graphics.Drawables.Drawable d = GetDrawable(Resource.Drawable._1f1e71f1f7);
			d.SetBounds(0, 0, d.IntrinsicWidth, d.IntrinsicHeight);
			emoji_button.Click += delegate {
				int pos = txt.SelectionStart;
				/*txt.Text = txt.Text.Substring(0, txt.SelectionStart) + ":flag-br:" +
					txt.Text.Substring(txt.SelectionStart, txt.Text.Length - txt.SelectionStart);*/
				txt.Text = txt.Text.Insert(txt.SelectionStart, ":flag-br:");

				txt.SetSelection(pos + 9);
				/*
				ss = new SpannableString(txt.Text);
				string[] splitted = txt.Text.Split(':');
				int index = 0;

				foreach (string str in splitted)
				{
					if (str == "flag-br")
					{
						ImageSpan spam = new ImageSpan(d, SpanAlign.Baseline);
						ss.SetSpan(spam, index - 1, index + 8, SpanTypes.InclusiveInclusive);
					}
					index += str.Length + 1;
				}
				txt.TextFormatted = ss;
				txt.SetSelection(pos + 10);
				/*ImageSpan spam = new ImageSpan(d, SpanAlign.Baseline);
				ImageSpan spam2 = new ImageSpan(d, SpanAlign.Bottom);
				ss.SetSpan(spam, 0, 9, SpanTypes.InclusiveInclusive);
				ss.SetSpan(spam2, 9, 18, SpanTypes.InclusiveInclusive);
				txt.TextFormatted = ss;
				txt.SetSelection(txt.Text.Length);*/

			};

			string prv = "";
			txt.TextChanged += (sender, e) =>
			{
				//The condition check should be redundant, but it is not. This happens because Xamarin.
				if (prv != txt.Text)
				{
					//finds index where the two strings first differ
					int index = 0;
					while (index < System.Math.Min(prv.Length, txt.Text.Length) && prv[index] == txt.Text[index])
						index++;

					//finds change length. It may be the case that a user corrects a word of length 4 to another one
					//e.g. (work and word). The total string length will be the same

					//creates list with all existing and previous spans
					Java.Lang.Object[] old_spans = ss.GetSpans(0, prv.Length, Java.Lang.Class.FromType(typeof(ImageSpan)));
					SpannableString newSpan = new SpannableString(txt.Text);

					int last_not_changed_span = -1;

					for (int i = 0; i < old_spans.Length; i++)
					{
						//saves existing spans that did not change position/index
						if (ss.GetSpanEnd(old_spans[i]) <= index)
						{
							newSpan.SetSpan(old_spans[i], ss.GetSpanStart(old_spans[i]),
											ss.GetSpanEnd(old_spans[i]), SpanTypes.InclusiveInclusive);
							last_not_changed_span = i;
						}
						//saves existing spans that changed position
						//DOES NOT WORK IF A SPAN WAS REMOVED
						else if( !(e.BeforeCount + e.Start >= ss.GetSpanEnd(old_spans[i]) &&
						        e.AfterCount + e.Start <= ss.GetSpanStart(old_spans[i]))) {
							newSpan.SetSpan(old_spans[i], ss.GetSpanStart(old_spans[i]) - prv.Length + txt.Text.Length,
											ss.GetSpanEnd(old_spans[i]) - prv.Length + txt.Text.Length,
							                SpanTypes.InclusiveInclusive);							
						}
					}

					//Identifies if new span was inserted
					//Compares string where the change is happening: between last span that did not change position
					//and first span that changed position
					if (prv.Length < txt.Text.Length)
					{
						old_spans = newSpan.GetSpans(0, txt.Text.Length, Java.Lang.Class.FromType(typeof(ImageSpan)));
						index = 0;
						int length = txt.Text.Length;
						//verifies if there exists any span
						if (old_spans.Length > 0)
						{
							//changes happening after at least one existing span
							if (last_not_changed_span >= 0)
							{
								index = newSpan.GetSpanEnd(old_spans[last_not_changed_span]);
								length -= index;
							}
							//changes happening at least before one existing span
							if (last_not_changed_span < old_spans.Length - 1)
								length = newSpan.GetSpanStart(old_spans[last_not_changed_span + 1]) - index;
						}

						string[] split = txt.Text.Substring(index, length).Split(':');
						foreach (string str in split)
						{
							if (str == "flag-br")
							{
								ImageSpan span = new ImageSpan(d, SpanAlign.Baseline);
								newSpan.SetSpan(span, index - 1, index + 8, SpanTypes.InclusiveInclusive);
							}
							index += str.Length + 1;
						}


					}

					//index--;
					prv = txt.Text;
					ss = newSpan;
					txt.TextFormatted = ss;
					//txt.SetSelection(System.Math.Max(0, index));
					txt.SetSelection(e.Start + e.AfterCount);
				}
			};
		}

		public void ShowEmojisTable(object sender, System.EventArgs e)
		{
			var txt = FindViewById<EditText>(Resource.Id.input_text);
			//txt.Text = "test";
			txt.SetSelection(txt.Text.Length);
			InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
			imm.HideSoftInputFromWindow(txt.WindowToken, 0);



			var grid = FindViewById<GridView>(Resource.Id.emoji_gridview);
			grid.Adapter = new EmojiAdapter(this, data, txt);
			//grid.Adapter = new ArrayAdapter(this, Resource.Layout.TextViewItem, data);
			grid.ItemClick += (Sender, args) => {
				var temp = (TextView)args.View;
				txt.Text = txt.Text.Insert(txt.SelectionStart, temp.Text);
			};

			grid.Visibility = Android.Views.ViewStates.Visible;

		}

		//Temporary mechanism for populating data with emojis
		public static Emoji[] PopulateData(int size)
		{
			string[] codes = { ":grinning:", ":grin:", ":joy:", ":rofl:", ":smiley:", ":smile:", ":sweat_smile:", ":laughing:",
				":wink:", ":blush:", ":yum:", ":sunglasses:", ":heart_eyes:", ":kissing_heart:", ":kissing:", ":kissing_smiling_eyes:",
				":kissing_closed_eyes:", ":relaxed:", ":slight_smile:", ":hugging:", ":thinking:", ":neutral_face:", ":expressionless:",
				":no_mouth:", ":rolling_eyes:", ":smirk:", ":persevere:", ":disappointed_relieved:", ":open_mouth:", ":zipper_mouth:",
				":hushed:", ":sleepy:", ":tired_face:", ":sleeping:", ":relieved:", ":nerd:", ":stuck_out_tongue:",
				":stuck_out_tongue_winking_eye:", ":stuck_out_tongue_closed_eyes:", ":drooling_face:" };

			if (size <= 0 || size >= codes.Length)
				size = codes.Length;

			Emoji[] emojiRay = new Emoji[size];

			for (int i = 0; i < size;i++){
				emojiRay[i] = new Emoji(codes[i]);
			}
			return emojiRay;
		}
	}
}

