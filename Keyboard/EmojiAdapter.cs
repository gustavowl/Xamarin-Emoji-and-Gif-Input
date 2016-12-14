
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
	[Activity(Label = "EmojiAdapter")]
	public class EmojiAdapter : ArrayAdapter<Emoji>
	{
		OnEmojiClickedListener emojiClickListener;

		public EmojiAdapter(Context context, Emoji[] data) : base(context, Resource.Layout.TextViewItem, data)
		{
		}

		public void setEmojiClickListener(OnEmojiClickedListener listener)
		{
			this.emojiClickListener = listener;
		}

		public override View GetView(int position, View convertView, ViewGroup parent){
			var v = convertView;
			ViewHolder holder;

			if (v == null) {
				v = View.Inflate(base.Context, Resource.Layout.TextViewItem, null);
				holder = new ViewHolder();
				holder.icon = v.FindViewById<TextView>(Resource.Id.textItem);
				v.Tag = holder;
			}

			Emoji emoji = base.GetItem(position);
			holder = v.Tag as ViewHolder;
			holder.icon.Text = emoji.getEmoji();
			//holder.icon.Click += delegate{
			//	emojiClickListener.onEmojiClicked(emoji);
			//};
			return v;
		}

		class ViewHolder : Java.Lang.Object {
			public TextView icon;
		}
	}
}
