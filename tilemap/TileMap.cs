// Copyright (c) 2014 Mattias Wargren
using System;
using System.Collections.Generic;

namespace tilemap
{
	public class TileMap<T>
	{
		T[] _tiles;
		int _width;
		int _height;

		public TileMap(int pWidth, int pHeight, T[] pTiles = null)
		{
			_width = pWidth;
			_height = pHeight;

			if (pTiles == null)
			{
				_tiles = new T[_width * _height];
			}
			else
			{
				if (pTiles.Length == (_width * _height))
				{
					_tiles = pTiles;
				}
				else
				{
					throw new Exception(string.Format("pTiles should have {0} items, found {1}", (_width * _height), pTiles.Length));
				}
			}
		}

		public int Count {
			get {
				return _tiles.Length;
			}
		}

		public int width {
			get {
				return _width;
			}
		}

		public int height {
			get {
				return _height;
			}
		}

		public int GetTileIndex(int pX, int pY)
		{
			return (pY * _width) + pX;
		}

		bool ValidCoordinates(int pX, int pY)
		{
			return (pX >= 0 && pX < _width && pY >= 0 && pY < _height) ? true : false;
		}

		public T GetTileByCoordinates(int pX, int pY)
		{
			if (ValidCoordinates(pX, pY))
			{
				var index = GetTileIndex(pX, pY);
				return _tiles[index];
			}
			else
			{
				return default(T);
			}
		}

		public T SetTileByCoordinates(int pX, int pY, T pTile)
		{
			var tileIndex = GetTileIndex(pX, pY);
			return _tiles[tileIndex] = pTile;
		}

		public void SetTileByIndex(int pIndex, T pTile)
		{
			_tiles[pIndex] = pTile;
		}

		public T GetTileByIndex(int pIndex)
		{
			return _tiles[pIndex];
		}

		public void Fill(Func<int, T> pFunc)
		{
			for (var i = 0; i < _tiles.Length; i++)
			{
				SetTileByIndex(i, pFunc.Invoke(i));
			}
		}

		public void Fill(Func<int, int, T> pFunc)
		{
			for (var y = 0; y < _height; y++)
			{
				for (var x = 0; x < _width; x++)
				{
					SetTileByCoordinates(x, y, pFunc.Invoke(x, y));
				}
			}
		}

		public List<T> GetTileNeighboursByCoordinates(int pX, int pY)
		{
			var result = new List<T>();

			for (var y = 0; y < _height; y++)
			{
				for (var x = 0; x < _width; x++)
				{
					if (!(x == pX && y == pY) && Math.Abs(pX - x) <= 1 && Math.Abs(pY - y) <= 1)
					{
						result.Add(GetTileByCoordinates(x, y));
					}
				}
			}

			return result;
		}
	}
}