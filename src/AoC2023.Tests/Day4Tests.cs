using System;
using System.IO;
using AoC2023.Core;
using NUnit.Framework;

namespace AoC2023.Tests
{
	public class Day4Tests
	{
		[SetUp]
		public void Setup()
		{
		}


		[Test]
		public void Puzzle1_ExampleInput()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day4_example.txt");
			Assert.IsTrue(input.Count>0);
			Assert.AreEqual(13, Day4.Solve1(input));
		}

		[Test]
		public void Puzzle1_Solver()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day4.txt");
			Assert.IsTrue(input.Count>0);
			Console.WriteLine(Day4.Solve1(input));
		}
		
		
		[Test]
		public void Puzzle2_ExampleInput()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day4_example.txt");
			Assert.IsTrue(input.Count>0);
			Assert.AreEqual(30, Day4.Solve2(input));
		}
		
		
		[Test]
		public void Puzzle2_Solver()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day4.txt");
			Assert.IsTrue(input.Count>0);
			Console.WriteLine(Day4.Solve2(input));
		}
	}
}