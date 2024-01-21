using Microsoft.VisualStudio.TestTools.UnitTesting;
using Uloha_1___Instalateri;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uloha_1___Instalateri.Tests
{
	[TestClass()]
	public class RoomTests
	{
		[TestMethod()]
		public void calculatePipeLengthTest_SameWall()
		{
			Room room = new Room(100);

			Assert.AreEqual(0, room.calculatePipeLength(new Point(20, 20, 100), new Point(20, 20, 100)));   // the same place
			Assert.AreEqual(10, room.calculatePipeLength(new Point(10, 20, 100), new Point(20, 20, 100)));  // one dimension diff
			Assert.AreEqual(20, room.calculatePipeLength(new Point(30, 30, 100), new Point(20, 20, 100)));  // two dimension diff
		}

		[TestMethod()]
		public void calculatePipeLengthTest_AdjacentWall()
		{
			Room room = new Room(300);

			Assert.AreEqual(380, room.calculatePipeLength(new Point(100, 100, 0), new Point(20, 0, 200))); //from teacher
			Assert.AreEqual(380, room.calculatePipeLength(new Point(100, 100, 0), new Point(0, 20, 200)));	
			Assert.AreEqual(280, room.calculatePipeLength(new Point(100, 100, 300), new Point(20, 0, 200)));	
		}

		[TestMethod()]
		public void calculateHoseLengthTest()
		{
			Room room = new Room(100);

			Assert.AreEqual(0.0, room.calculateHoseLength(new Point(20, 20, 100), new Point(20, 20, 100)));   // the same place
			Assert.AreEqual(10.0, room.calculateHoseLength(new Point(10, 20, 100), new Point(20, 20, 100)));  // one dimension diff
			Assert.AreEqual(Math.Sqrt(200), room.calculateHoseLength(new Point(30, 30, 100), new Point(20, 20, 100)));  // two dimension diff
		}

		[TestMethod()]
		public void calculatePipeLengthTest_TeacherTests() //from teacher
		{
			Room room = new Room(300);
			Room smallerroom = new Room(184);

			Assert.AreEqual(380, room.calculatePipeLength(new Point(100, 100, 0), new Point(20, 0, 200))); 
			Assert.AreEqual(400, room.calculatePipeLength(new Point(100, 100, 0), new Point(300, 100, 200)));
			// Assert.AreEqual(590, room.calculatePipeLength(new Point(130, 100, 0), new Point(200, 280, 300)));  // opposite 
			// Assert.AreEqual(319, smallerroom.calculatePipeLength(new Point(21, 37, 0), new Point(96, 55, 184)));  // opposite 
		}

		[TestMethod()]
		public void calculateHoseLengthTest_TeacherTests()
		{
			Room room = new Room(300);
			Room smallerroom = new Room(184);

			// Assert.AreEqual(310.483494, room.calculateHoseLength(new Point(100, 100, 0), new Point(20, 0, 200)));		// adjacent
			// Assert.AreEqual(400.000000, room.calculateHoseLength(new Point(100, 100, 0), new Point(300, 100, 200)));        // adjacent
			// Assert.AreEqual(524.690385, room.calculateHoseLength(new Point(130, 100, 0), new Point(200, 280, 300)));  // opposite 
			// Assert.AreEqual(286.008741, smallerroom.calculateHoseLength(new Point(21, 37, 0), new Point(96, 55, 184)));  // opposite 

		}

	}
}