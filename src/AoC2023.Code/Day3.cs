using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;

namespace AoC2023.Core
{
	public static class Day3
	{
		public class SymbolInfo
		{
			public Point Location { get; set; } = Point.Empty;
			public bool IsGear { get; set; }
		}
		
		public static int Solve1(List<string> input)
		{
			int toReturn = 0;
			
			for(int y = 0; y < input.Count; y++)
			{
				int x = 0;
				while(x < input[y].Length)
				{
					if(Char.IsDigit(input[y][x]))
					{
						var xNumberStart = x;
						var number = ScanNumber(input[y], ref x);
						// check if there's a symbol adjacent to this number.
						if(CheckIfSymbol(input, xNumberStart, y, x-xNumberStart, out SymbolInfo symbolInfo))
						{
							toReturn += number;
						}
					}
					x++;
				}
			}
				
			return toReturn;
		}


		public static int Solve2(List<string> input)
		{
			int toReturn = 0;

			var gearWithNumbers = new Dictionary<Point, int>();
			for(int y = 0; y < input.Count; y++)
			{
				int x = 0;
				while(x < input[y].Length)
				{
					if(Char.IsDigit(input[y][x]))
					{
						var xNumberStart = x;
						var number = ScanNumber(input[y], ref x);
						// check if there's a symbol adjacent to this number.
						if(CheckIfSymbol(input, xNumberStart, y, x-xNumberStart, out SymbolInfo symbolInfo))
						{
							if(symbolInfo.IsGear)
							{
								if(gearWithNumbers.TryGetValue(symbolInfo.Location, out int connectedNumber))
								{
									toReturn += (number * connectedNumber);
									gearWithNumbers.Remove(symbolInfo.Location);
								}
								else
								{
									gearWithNumbers[symbolInfo.Location] = number;
								}
							}
						}
					}
					x++;
				}
			}

			return toReturn;
		}


		private static bool CheckIfSymbol(List<string> input, int xNumber, int yNumber, int length, out SymbolInfo symbolInfo)
		{
			// scan the box, including the digits. If we find a char that's not a digit and not a '.' it's a symbol
			for(int y = Math.Max(yNumber - 1, 0); y < Math.Min(yNumber+2, input.Count); y++)
			{
				for(int x = Math.Max(xNumber - 1, 0); x < Math.Min(xNumber+length+1, input[y].Length); x++)
				{
					if(!Char.IsDigit(input[y][x]) && input[y][x] != '.')
					{
						symbolInfo = new SymbolInfo() { Location = new Point(x, y), IsGear = input[y][x] == '*' };
						return true;
					}
				}
			}

			symbolInfo = new SymbolInfo();
			return false;
		}


		private static int ScanNumber(string s, ref int x)
		{
			int toReturn = 0;
			while(x < s.Length)
			{
				if(!Char.IsDigit(s[x]))
				{
					break;
				}
				toReturn = 10 * toReturn + (s[x] - '0');
				x++;
			}

			return toReturn;
		}
	}
}
