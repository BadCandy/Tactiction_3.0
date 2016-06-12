using UnityEngine;
using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;

public class EnumArray<TEnum, T>
	where TEnum: struct, IComparable, IFormattable, IConvertible // it means enum
{
	T[] _array;

	public T[] Array
	{
		get
		{
			return _array;
		}
	}

	public int Length
	{
		get
		{
			return _array.Length;
		}
	}

	public EnumArray()
	{
		_array = new T[Enum.GetNames(typeof(TEnum)).Length];
	}

	public bool IsDefined(string type)
	{
		return Enum.IsDefined(typeof(TEnum), type);
	}

	public T Get(string type)
	{
		return _array[(int)Enum.Parse(typeof(TEnum), type, true)];
	}
	
	public T Get(TEnum type)
	{
		return _array[type.ToInt32(NumberFormatInfo.InvariantInfo)];
	}
	
	public void Set(string type, T value)
	{
		_array[(int)Enum.Parse(typeof(TEnum), type, true)] = value;
	}
	
	public void Set(TEnum type, T value)
	{
		_array[type.ToInt32(NumberFormatInfo.InvariantInfo)] = value;
	}
}
