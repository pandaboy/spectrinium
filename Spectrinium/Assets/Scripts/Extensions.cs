using UnityEngine;
using System;
using System.Collections;

// common class for holding extension methods
public static class ExtensionMethods {

	public static T Next<T>(this T curr) where T : struct
	{
		// only do this for enums
		if(!typeof(T).IsEnum)
			throw new ArgumentException(string.Format("Argument {0} is not an Enum", typeof(T).FullName));
		
		T[] Arr = (T[])Enum.GetValues(curr.GetType());
		int j = Array.IndexOf<T>(Arr, curr) + 1;
		
		return Arr.Length == j ? Arr[0] : Arr[j];
	}
	
	public static T Prev<T>(this T curr) where T : struct
	{
		// only do this for enums
		if(!typeof(T).IsEnum)
			throw new ArgumentException(string.Format("Argument {0} is not an Enum", typeof(T).FullName));
			
		T[] Arr = (T[])Enum.GetValues(curr.GetType());
		int j = Array.IndexOf<T>(Arr, curr) - 1;
		
		// if j is less than 0 (i.e. -1), loop round to the last element of the array
		return (j < 0) ? Arr[(Arr.Length - 1)] : Arr[j];
	}
}
