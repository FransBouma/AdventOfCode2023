using System;
using System.IO;
using AoC2023.Core;
using NUnit.Framework;

namespace AoC2023.Tests
{
	public class Day6Tests
	{
		[SetUp]
		public void Setup()
		{
		}


		[Test]
		public void Puzzle1_ExampleInput()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day6_example.txt");
			Assert.IsTrue(input.Count>0);
			Assert.AreEqual(288, Day6.Solve1(input));
		}

		[Test]
		public void Puzzle1_Solver()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day6.txt");
			Assert.IsTrue(input.Count>0);
			Console.WriteLine(Day6.Solve1(input));
		}
		
		
		[Test]
		public void Puzzle2_ExampleInput()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day6_example.txt");
			Assert.IsTrue(input.Count>0);
			Assert.AreEqual(71503, Day6.Solve2(input));
		}
		
		
		[Test]
		public void Puzzle2_Solver()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day6.txt");
			Assert.IsTrue(input.Count>0);
			Console.WriteLine(Day6.Solve2(input));
		}
	}
}