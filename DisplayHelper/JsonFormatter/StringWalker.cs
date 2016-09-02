namespace DisplayHelper.JsonFormatter
{
	/// <summary>
	/// For stepping through a string, one character at a time.
	/// </summary>
	/// <remarks>This code copied verbatim from http://www.limilabs.com/blog/json-net-formatter
	/// </remarks>
	public class StringWalker
	{
		string _s;
		public int Index { get; set; }

		public StringWalker(string s)
		{
			_s = s;
			Index = -1;
		}

		public bool MoveNext()
		{
			if (Index == _s.Length - 1)
				return false;
			Index++;
			return true;
		}

		public char CharAtIndex()
		{
			return _s[Index];
		}
	}
}
