using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using SD.Tools.Algorithmia.GeneralDataStructures;
using SD.Tools.BCLExtensions.CollectionsRelated;

namespace AoC2023.Core
{
	public static class Day13
	{
		public class Patterns
		{
			public Patterns(List<string> input)
			{
				List<string> patternContent = new();
				foreach(var s in input)
				{
					if(string.IsNullOrWhiteSpace(s))
					{
						PatternContents.Add(patternContent);
						patternContent = new List<string>();
					}
					else
					{
						patternContent.Add(s);
					}
				}
				PatternContents.Add(patternContent);
			}


			public long CalculateValue()
			{
				long toReturn = 0;
				foreach(var p in this.PatternContents)
				{
					bool b = false;
					toReturn += CalculateHorizontalReflectionValue(p, false, -1, ref b) * 100;
					toReturn += CalculateVerticalReflectionValue(p, false, -1, ref b);
				}
				return toReturn;
			}


			public long CalculateValue2()
			{
				long toReturn = 0;
				int patternIndex = 0;
				foreach(var p in this.PatternContents)
				{
					// the joy of having a wrong answer with the full input but not with the example input... luckily it was detectable with a couple of checks...
					bool smudgeFixed = false;
					var pOrig = p.ToList();
					var answerWithSmudgeH = CalculateHorizontalReflectionValue(p, false, -1, ref smudgeFixed);
					var answerWithoutSmudgeH = CalculateHorizontalReflectionValue(p, true, answerWithSmudgeH, ref smudgeFixed);
					if(answerWithoutSmudgeH > 0 && answerWithoutSmudgeH == answerWithSmudgeH)
					{
						throw new Exception(string.Format("H: Answers are equal for pattern {0}: smudge: {1}. without: {2}", patternIndex, answerWithSmudgeH, answerWithoutSmudgeH));
					}
					toReturn += answerWithoutSmudgeH * 100;
					var answerWithSmudgeV = CalculateVerticalReflectionValue(p, false, -1, ref smudgeFixed);
					var answerWithoutSmudgeV = CalculateVerticalReflectionValue(p, true, answerWithSmudgeV, ref smudgeFixed);
					if(answerWithoutSmudgeV > 0 && answerWithoutSmudgeV == answerWithSmudgeV)
					{
						throw new Exception(string.Format("V: Answers are equal for pattern {0}: smudge: {1}. without: {2}", patternIndex, answerWithSmudgeV, answerWithoutSmudgeV));
					}
					toReturn += answerWithoutSmudgeV;
					if(answerWithoutSmudgeV != 0 && answerWithoutSmudgeH != 0)
					{
						throw new Exception(string.Format(" Pattern {0} has 2 reflection lines: H: {1}. V: {2}", patternIndex, answerWithoutSmudgeH, answerWithoutSmudgeV));
					}
					if(answerWithoutSmudgeV == 0 && answerWithoutSmudgeH == 0)
					{
						Console.WriteLine("Faulty pattern: ");
						foreach(var c in pOrig)
						{
							Console.WriteLine(c);
						}
						throw new Exception(string.Format(" Pattern {0} has 0 reflection lines", patternIndex));
					}
					patternIndex++;
				}
				return toReturn;
			}


			private long CalculateVerticalReflectionValue(List<string> p, bool puzzle2, long answerWithSmudge, ref bool smudgeFixed)
			{
				// rotate 90 degrees to the right, starting at the bottom left corner
				var rotatedP = new List<string>();
				var copiedP = p.ToList();
				copiedP.Reverse();
				for(int i = 0; i < p[0].Length; i++)
				{
					rotatedP.Add(string.Join("", (from s in copiedP select s[i]).ToArray()));
				}
				
				return CalculateHorizontalReflectionValue(rotatedP, puzzle2, answerWithSmudge, ref smudgeFixed);
			}


			private long CalculateHorizontalReflectionValue(List<string> original, bool puzzle2, long answerWithSmudge, ref bool smudgeFixed)
			{
				int rowBeforeReflectionLine = 0;
				for(int i = 0; i < original.Count-1; i++)
				{
					var p = original.ToList();
					bool smudgeFixedThisPass = smudgeFixed;
					if(puzzle2)
					{
						if(p[i + 1] != p[i] && !smudgeFixedThisPass && (answerWithSmudge-1!=i))
						{
							// check if the diff is just 1 char. if so, fix it, and proceed as normal
							var diffOffset = GetDiffOffset(p[i], p[i + 1]);
							if(diffOffset>=0)
							{
								var sb = new StringBuilder(p[i]);
								sb[diffOffset] = p[i + 1][diffOffset];
								p[i] = sb.ToString();
								smudgeFixedThisPass = true;
							}
						}
					}
					if(p[i + 1] == p[i] && (!puzzle2 || (answerWithSmudge-1!=i)))
					{
						// check reflection line
						bool reflectionOk = true;
						int delta = 1;
						while(reflectionOk)
						{
							if(i - delta < 0 || i + delta + 1 >= p.Count)
							{
								// done
								break;
							}
							if(puzzle2)
							{
								if(p[i - delta] != p[i + delta + 1] && !smudgeFixedThisPass)
								{
									// check if the diff is just 1 char. if so, fix it, and proceed as normal
									var diffOffset = GetDiffOffset(p[i - delta], p[i + delta + 1]);
									if(diffOffset>=0)
									{
										var sb = new StringBuilder(p[i - delta]);
										sb[diffOffset] = p[i + delta + 1][diffOffset];
										p[i - delta] = sb.ToString();
										smudgeFixedThisPass = true;
									}
								}
							}
							reflectionOk &= (p[i - delta] == p[i + delta + 1]);
							delta++;
						}
						if(reflectionOk)
						{
							rowBeforeReflectionLine = i+1;
							if(puzzle2)
							{
								smudgeFixed |= smudgeFixedThisPass;
								// replace original with new contents if puzzle 2
								original.Clear();
								original.AddRange(p);
							}
							break;
						}
					}
				}
				return rowBeforeReflectionLine;
			}


			private int GetDiffOffset(string s1, string s2)
			{
				int toReturn = -1;
				for(int i = 0; i < s1.Length; i++)
				{
					if(s1[i] != s2[i])
					{
						if(toReturn < 0)
						{
							toReturn = i;
						}
						else
						{
							// already found a difference, 2 differences, not fixable
							return -1;
						}
					}
				}
				return toReturn;
			}


			public List<List<string>> PatternContents { get; } = new();
		}
		
		
		public static long Solve1(List<string> input)
		{
			var p = new Patterns(input);
			return p.CalculateValue();
		}


		public static long Solve2(List<string> input)
		{
			var p = new Patterns(input);
			return p.CalculateValue2();
		}
	}
}
