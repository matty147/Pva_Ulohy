using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Uloha_1___Instalateri;

namespace Uloha1InstalateriTest
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestParseInput_Valid()
		{
			Assert.AreEqual((300, new Point(100, 100, 0), new Point(20, 0, 200)), Program.ParseInput("300,100,100,0,20,0,200"));
		}

		[TestMethod]
		public void TestParseInput_Invalid()
		{
			Assert.ThrowsException<ArgumentException>(() => Program.ParseInput("a"));
			Assert.ThrowsException<ArgumentException>(() => Program.ParseInput(""));
			Assert.ThrowsException<ArgumentException>(() => Program.ParseInput("1,2"));
			Assert.ThrowsException<ArgumentException>(() => Program.ParseInput("1,2,3,a,5,6,7"));
			Assert.ThrowsException<ArgumentException>(() => Program.ParseInput("1,2,3,4,5,6,7,8"));
			Assert.ThrowsException<ArgumentException>(() => Program.ParseInput("1.2.3.a.5.6.7"));
			Assert.ThrowsException<ArgumentException>(() => Program.ParseInput("300,100,400,0"));
			Assert.ThrowsException<ArgumentException>(() => Program.ParseInput("300,100,100,0,10,100,300"));
			Assert.ThrowsException<ArgumentException>(() => Program.ParseInput("300,100,0,0,20,100,300"));
			Assert.ThrowsException<ArgumentException>(() => Program.ParseInput("300,100,25,0,10,100,400"));
			Assert.ThrowsException<ArgumentException>(() => Program.ParseInput("1,100,25,0,10,100,400"));
			Assert.ThrowsException<ArgumentException>(() => Program.ParseInput("250,-1,25,0,10,100,-400"));
			Assert.ThrowsException<ArgumentException>(() => Program.ParseInput("1,1,25,0,10,100,400"));
			Assert.ThrowsException<ArgumentException>(() => Program.ParseInput("-100,1,25,0,10,100,400"));
			Assert.ThrowsException<ArgumentException>(() => Program.ParseInput("300,40,25,40,10,100,200"));

		}

		[TestMethod]
		public void TestParseInput_Invalid_Teacher()
		{
			Assert.ThrowsException<ArgumentException>(() => Program.ParseInput("300, 100, 400, 0, "));
			Assert.ThrowsException<ArgumentException>(() => Program.ParseInput("300, 100, 400, 0, 100, 100, 100"));

			Assert.ThrowsException<ArgumentException>(() => Program.ParseInput("300, 100, 100, 0, 10, 100, 300"));
			Assert.ThrowsException<ArgumentException>(() => Program.ParseInput("300, 100, 100, 0, 50, 50, test"));
		}
	}
}
