// Copyright (c) 2014 Mattias Wargren
using NUnit.Framework;

namespace tilemap.tests
{
	[TestFixture()]
	public class TestBasics
	{
		[Test()]
		public void TestTileMapCreation()
		{
			var tileMap = new TileMap<int>(10, 10);
			Assert.IsNotNull(tileMap);
		}

		[Test()]
		public void TestTileMapCreationDefaultList()
		{
			var tileMap = new TileMap<int>(3, 1, new int[]{ 1, 2, 3 });
			Assert.AreEqual(3, tileMap.Count);
		}

		[Test()]
		public void TestTileMapSize()
		{
			var tileMap = new TileMap<int>(3, 10);

			Assert.AreEqual(3, tileMap.width);
			Assert.AreEqual(10, tileMap.height);
			Assert.AreEqual(30, tileMap.Count);
		}

		[Test()]
		public void TestTileMapGetTileIndex()
		{
			var tileMap = new TileMap<string>(3, 3);
			var tileIndex = 0;

			for (var y = 0; y < tileMap.height; y++)
			{
				for (var x = 0; x < tileMap.width; x++)
				{
					Assert.AreEqual(tileIndex, tileMap.GetTileIndex(x, y));
					tileIndex++;
				}
			}
		}

		[Test()]
		public void TestTileMapSetAndGetByCoordinates()
		{
			var tileMap = new TileMap<string>(3, 1);

			tileMap.SetTileByCoordinates(0, 0, "0-0");
			tileMap.SetTileByCoordinates(1, 0, "1-0");
			tileMap.SetTileByCoordinates(2, 0, "2-0");

			Assert.AreEqual("0-0", tileMap.GetTileByCoordinates(0, 0));
			Assert.AreEqual("1-0", tileMap.GetTileByCoordinates(1, 0));
			Assert.AreEqual("2-0", tileMap.GetTileByCoordinates(2, 0));
		}

		[Test()]
		public void TestTileMapSetAndGetByIndex()
		{
			var tileMap = new TileMap<int>(10, 3);

			for (var i = 0; i < tileMap.Count; i++)
			{
				tileMap.SetTileByIndex(i, i);
			}

			for (var i = 0; i < tileMap.Count; i++)
			{
				Assert.AreEqual(i, tileMap.GetTileByIndex(i));
			}
		}

		[Test()]
		public void TestTileMapFillByIndex()
		{
			var tileMap = new TileMap<string>(3, 3);
			tileMap.Fill((pIndex) => string.Format("index-{0}", pIndex));

			for (var i = 0; i < tileMap.Count; i++)
			{
				Assert.AreEqual(string.Format("index-{0}", i), tileMap.GetTileByIndex(i));
			}
		}

		[Test()]
		public void TestTileMapFillByCoordinates()
		{
			var tileMap = new TileMap<string>(3, 3);
			tileMap.Fill((pX, pY) => string.Format("x{0} y{1}", pX, pY));
		}

		[Test()]
		public void TestTileMapNeighbours()
		{
			var tileMap = new TileMap<string>(3, 3);

			tileMap.Fill((pIndex) => string.Format("index-{0}", pIndex));

			var tileNeighboursTL = tileMap.GetTileNeighboursByCoordinates(0, 0);
			var tileNeighboursML = tileMap.GetTileNeighboursByCoordinates(0, 1);
			var tileNeighboursMC = tileMap.GetTileNeighboursByCoordinates(1, 1);
			var tileNeighboursMR = tileMap.GetTileNeighboursByCoordinates(2, 1);
			var tileNeighboursBR = tileMap.GetTileNeighboursByCoordinates(2, 2);

			Assert.AreEqual(3, tileNeighboursTL.Count);
			Assert.AreEqual(5, tileNeighboursML.Count);
			Assert.AreEqual(8, tileNeighboursMC.Count);
			Assert.AreEqual(5, tileNeighboursMR.Count);
			Assert.AreEqual(3, tileNeighboursBR.Count);

			Assert.AreEqual("index-1", tileNeighboursTL[0]);
			Assert.AreEqual("index-3", tileNeighboursTL[1]);
			Assert.AreEqual("index-4", tileNeighboursTL[2]);
		}

	}
}

