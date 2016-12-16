using System;
using Android.Content;
using Android.Text;
using Android.Text.Style;
using Android.Util;

namespace Keyboard
{
	public class EmojiHandler
	{
		private Context Cont;
		private static SparseArray<Emoji> EmojioneMap = new SparseArray<Emoji>(10);
		private static bool Started = false;
		private static ArrayMap asdf = new ArrayMap(10);

		public EmojiHandler(Context cont)
		{
			Cont = cont;
			if (!Started)
			{
				//asdf.Put(
				EmojioneMap.Put(Resource.Drawable.emojione_1f600, new Emoji("flag-br"));
				EmojioneMap.Put(Resource.Drawable.emojione_1f601, "grin");
				EmojioneMap.Put(Resource.Drawable.emojione_1f602, "joy");
				EmojioneMap.Put(Resource.Drawable.emojione_1f603, "smiley");
				EmojioneMap.Put(Resource.Drawable.emojione_1f923, "rofl");
				EmojioneMap.Put(Resource.Drawable.emojione_1f1e71f1f7, "flag-br");
			}
		}

		//prv = previous text (before changes)
		//curr = current text (after change/new typo is applied)
		//ss = reference of SpannableString from EmojiEditText input
		public SpannableString UpdateSpan(string prv, string curr, SpannableString ss, 
		                                  TextChangedEventArgs e)
		{
			//finds index where the two strings first differ
			int index = 0;
			while (index < System.Math.Min(prv.Length, curr.Length) && prv[index] == curr[index])
				index++;

			//finds change length. It may be the case that a user corrects a word of length 4 to another one
			//e.g. (work and word). The total string length will be the same

			//creates list with all existing and previous spans
			Java.Lang.Object[] old_spans = ss.GetSpans(0, prv.Length, Java.Lang.Class.FromType(typeof(ImageSpan)));
			SpannableString newSpan = new SpannableString(curr);

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
				else if (!(e.BeforeCount + e.Start >= ss.GetSpanEnd(old_spans[i]) &&
						e.AfterCount + e.Start <= ss.GetSpanStart(old_spans[i])))
				{
					newSpan.SetSpan(old_spans[i], ss.GetSpanStart(old_spans[i]) - prv.Length + curr.Length,
									ss.GetSpanEnd(old_spans[i]) - prv.Length + curr.Length,
									SpanTypes.InclusiveInclusive);
				}
			}

			//Identifies if new span was inserted
			//Compares string where the change is happening: between last span that did not change position
			//and first span that changed position
			if (prv.Length < curr.Length)
			{
				old_spans = newSpan.GetSpans(0, curr.Length, Java.Lang.Class.FromType(typeof(ImageSpan)));
				index = 0;
				int length = curr.Length;
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

				string[] split = curr.Substring(index, length).Split(':');
				Emoji em = new Emoji("");
				foreach (string str in split)
				{
					em.setEmojiShortname(str);
					if (EmojioneMap.IndexOfValue(em) >= 0)
					{
						//Doesn't this consume too much memory?
						Android.Graphics.Drawables.Drawable d = Cont.GetDrawable(EmojioneMap.IndexOfValue(str));
						d.SetBounds(0, 0, d.IntrinsicWidth, d.IntrinsicHeight);
						ImageSpan span = new ImageSpan(d,SpanAlign.Baseline);
						newSpan.SetSpan(span, index - 1, index + str.Length + 1, SpanTypes.InclusiveInclusive);
					}
					index += str.Length + 1;
				}


			}

			//index--;
			return newSpan;
			//currFormatted = ss;
			//txt.SetSelection(System.Math.Max(0, index));
			//txt.SetSelection(e.Start + e.AfterCount);
		}
	}
}
