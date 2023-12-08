using System;
using System.IO;
using AoC2023.Core;
using NUnit.Framework;

namespace AoC2023.Tests
{
	public class Day8Tests
	{
		[SetUp]
		public void Setup()
		{
		}


		[Test]
		public void Puzzle1_ExampleInput()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day8_example.txt");
			Assert.IsTrue(input.Count>0);
			Assert.AreEqual(2, Day8.Solve1(input));
		}
		
		[Test]
		public void Puzzle1_ExampleInput2()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day8_example2.txt");
			Assert.IsTrue(input.Count>0);
			Assert.AreEqual(6, Day8.Solve1(input));
		}
		
		[Test]
		public void Puzzle1_Solver()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day8.txt");
			Assert.IsTrue(input.Count>0);
			Console.WriteLine(Day8.Solve1(input));
		}
		
		
		[Test]
		public void Puzzle2_ExampleInput()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day8_example3.txt");
			Assert.IsTrue(input.Count>0);
			Assert.AreEqual(6, Day8.Solve2(input));
		}
		
		
		[Test]
		public void Puzzle2_Solver()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day8.txt");
			Assert.IsTrue(input.Count>0);
			Console.WriteLine(Day8.Solve2(input));
		}
	}
}