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
			emoji_button.Click += delegate {
				//int pos = txt.SelectionStart;
				txt.Text = txt.Text.Insert(txt.SelectionStart, ":flag-br:");
				//txt.SetSelection(pos + 9);
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

