using System;
using System.IO;
using AoC2023.Core;
using NUnit.Framework;

namespace AoC2023.Tests
{
	public class Day1Tests
	{
		[SetUp]
		public void Setup()
		{
		}


		[Test]
		public void Puzzle1_ExampleInput()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day1_example.txt");
			Assert.IsTrue(input.Count>0);
			Assert.AreEqual(142, Day1.Solve1(input));
		}

		[Test]
		public void Puzzle1_Solver()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day1.txt");
			Assert.IsTrue(input.Count>0);
			Console.WriteLine(Day1.Solve1(input));
		}
		
		
		[Test]
		public void Puzzle2_ExampleInput()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day1_example2.txt");
			Assert.IsTrue(input.Count>0);
			Assert.AreEqual(281, Day1.Solve2(input));
		}
		
		
		[Test]
		public void Puzzle2_Solver()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day1.txt");
			Assert.IsTrue(input.Count>0);
			Console.WriteLine(Day1.Solve2(input));
		}
	}
}