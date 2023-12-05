using System;
using System.IO;
using AoC2023.Core;
using NUnit.Framework;

namespace AoC2023.Tests
{
	public class Day5Tests
	{
		[SetUp]
		public void Setup()
		{
		}


		[Test]
		public void Puzzle1_ExampleInput()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day5_example.txt");
			Assert.IsTrue(input.Count>0);
			Assert.AreEqual(35, Day5.Solve1(input));
		}

		[Test]
		public void Puzzle1_Solver()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day5.txt");
			Assert.IsTrue(input.Count>0);
			Console.WriteLine(Day5.Solve1(input));
		}
		
		
		[Test]
		public void Puzzle2_ExampleInput()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day5_example.txt");
			Assert.IsTrue(input.Count>0);
			Assert.AreEqual(46, Day5.Solve2(input));
		}
		
		
		[Test]
		public void Puzzle2_Solver()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day5.txt");
			Assert.IsTrue(input.Count>0);
			Console.WriteLine(Day5.Solve2(input));
		}
	}
}