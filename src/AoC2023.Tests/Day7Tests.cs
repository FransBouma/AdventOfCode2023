using System;
using System.IO;
using AoC2023.Core;
using NUnit.Framework;

namespace AoC2023.Tests
{
	public class Day7Tests
	{
		[SetUp]
		public void Setup()
		{
		}


		[Test]
		public void Puzzle1_ExampleInput()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day7_example.txt");
			Assert.IsTrue(input.Count>0);
			Assert.AreEqual(6440, Day7.Solve1(input));
		}

		[Test]
		public void Puzzle1_Solver()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day7.txt");
			Assert.IsTrue(input.Count>0);
			Console.WriteLine(Day7.Solve1(input));
		}
		
		
		[Test]
		public void Puzzle2_ExampleInput()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day7_example.txt");
			Assert.IsTrue(input.Count>0);
			Assert.AreEqual(5905, Day7.Solve2(input));
		}
		
		
		[Test]
		public void Puzzle2_Solver()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day7.txt");
			Assert.IsTrue(input.Count>0);
			Console.WriteLine(Day7.Solve2(input));
		}
	}
}