using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Utils
{
	public static class CollectionExtensions
	{
		public static T GetRandom<T>(this T[] arr)
		{
			return arr[Random.Range(0, arr.Length)];
		}

		public static T GetRandom<T>(this IList<T> list)
		{
			return list[Random.Range(0, list.Count)];
		}
		
		public static T Last<T>(this IList<T> list) => list[list.Count - 1];

		public static T First<T>(this IEnumerable<T> enumerable, Func<T, bool> filter)
		{
			foreach (var t in enumerable)
				if (filter(t))
					return t;
			throw new Exception("IEnumerable is Empty!");
		}

		public static T FirstOrDefault<T>(this IEnumerable<T> enumerable)
		{
			foreach (var t in enumerable)
				return t;
			return default;
		}

		public static T FirstOrDefault<T>(this IEnumerable<T> enumerable, Func<T, bool> filter)
		{
			foreach (var t in enumerable)
				if (filter(t))
					return t;
			return default;
		}

		public static bool Any<T>(this IEnumerable<T> self, Func<T, bool> func)
		{
			if (self == null)
				throw new NullReferenceException(nameof(self));
			if (func == null)
				throw new NullReferenceException(nameof(func));
			foreach (var item in self)
				if (func(item))
					return true;
			return false;
		}

		public static bool Any<T>(this T[] self, Func<T, bool> func)
		{
			if (self == null)
				throw new NullReferenceException(nameof(self));
			if (func == null)
				throw new NullReferenceException(nameof(func));
			foreach (var item in self)
				if (func(item))
					return true;
			return false;
		}
		
		public static T GetRandomExcept<T>(this T[] arr, T ignored)
		{
			T random;
			do
				random = arr[Random.Range(0, arr.Length)];
			while (random.Equals(ignored));

			return random;
		}

		public static T GetRandomExcept<T>(this T[] arr, int index)
		{
			T random;
			int randomIndex;
			do
			{
				randomIndex = Random.Range(0, arr.Length);
				random = arr[randomIndex];
			}
			while (randomIndex == index);

			return random;
		}

		public static T GetRandomExcept<T>(this IList<T> list, T ignored)
		{
			T random;
			do
				random = list[Random.Range(0, list.Count)];
			while (random.Equals(ignored));

			return random;
		}
		
		public static T GetRandomExcept<T>(this IList<T> list, List<T> ignored)
		{
			T random;
			do
				random = list[Random.Range(0, list.Count)];
			while (ignored.Contains(random));

			return random;
		}
		
		public static int IndexOf<T>(this T[] list, T element) where T : class
		{
			for (int i = 0; i < list.Length; i++)
			{
				T elem = list[i];
				if (elem == element)
					return i;
			}

			return -1;
		}
	}
}