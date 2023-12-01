using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace AoC2023.Core
{
	public static class Day1
	{
		private static List<int> CalculateValues(List<string> input)
		{
			var toReturn = new List<int>();
			foreach(var s in input)
			{
				var firstDigit = s.First(a => Char.IsDigit(a));
				var lastDigit = s.Last(a => Char.IsDigit(a));
				var calibrationValue = Convert.ToInt32("" + firstDigit + lastDigit);
				toReturn.Add(calibrationValue);
			}
			return toReturn;
		}

		
		public static int Solve1(List<string> input)
		{
			return CalculateValues(input).Sum();
		}


		public static int Solve2(List<string> input)
		{
			var digitToString = new Dictionary<string, string>() {{"1", "one"}, {"2", "two"}, { "3", "three"}, { "4", "four"}, { "5", "five"}, { "6", "six"}, { "7", "seven"}, { "8", "eight"}, { "9", "nine"}};
			for(int i = 0; i < input.Count; i++)
			{
				var originalString = input[i];
				var intermediateResult = originalString;
				// start scanning from the front, if we find a match, we'll convert the string and then scan from the back.
				// Find the min index so we replace the first occurrence. e.g. eightwo will result in 8wo and not eigh2, as 'eight' occurs before 'two'
				// but 2 is earlier in the lookup so we'll replace that first if we don't use the minimal index
				// We also have to make sure overlaps are honoured correctly. 2eightwo should result in 22 and not 28. because the '2' at the front makes sure the 'eight' is never
				// needed, so the 'two' will have to be transformed into 2. 
				// hacky code below.
				int minIndex = originalString.Length;
				var minDigitIndex = originalString.IndexOf(originalString.First(a => char.IsDigit(a)));
				KeyValuePair<string, string> toUseForReplace = new KeyValuePair<string, string>(string.Empty, string.Empty);
				foreach(var kvp in digitToString)
				{
					var index = intermediateResult.IndexOf(kvp.Value, StringComparison.Ordinal);
					if(index < 0)
					{
						continue;
					}
					if(index < minIndex)
					{
						minIndex = index;
						toUseForReplace = kvp;
					}
				}

				if(minIndex < minDigitIndex && minIndex < originalString.Length)
				{
					intermediateResult = ReplaceDigitName(minIndex, toUseForReplace, intermediateResult);
				}

				int maxIndex = -1;
				var maxDigitIndex = intermediateResult.LastIndexOf(originalString.Last(a => char.IsDigit(a)));
				// then from the back
				foreach(var kvp in digitToString)
				{
					var index = intermediateResult.LastIndexOf(kvp.Value, StringComparison.Ordinal);
					if(index < 0)
					{
						continue;
					}
					if(index > maxIndex)
					{
						maxIndex = index;
						toUseForReplace = kvp;
					}
				}
				if( maxIndex >maxDigitIndex && maxIndex >= 0)
				{
					intermediateResult = ReplaceDigitName(maxIndex, toUseForReplace, intermediateResult);
				}

				//Console.WriteLine("Original: '{0}'. New: '{1}'", originalString, intermediateResult);
				input[i] = intermediateResult;
			}
			return CalculateValues(input).Sum();
		}


		private static string ReplaceDigitName(int index, KeyValuePair<string, string> kvp, string toReplaceIn)
		{
			if(index < 0)
			{
				return toReplaceIn;
			}
			var toReturn = string.Empty;
			if(index > 0)
			{
				// copy prefix
				toReturn = toReplaceIn.Substring(0, index);
			}

			toReturn += kvp.Key;
			if(index < toReplaceIn.Length - 1)
			{
				// copy suffix
				toReturn += toReplaceIn.Substring(index + kvp.Value.Length);
			}

			return toReturn;
		}
	}
}