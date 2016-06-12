using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class PmlElement : Dictionary<string, string>
{
	public bool TryGetShortValue(string key, out short value)
	{
		string strValue;
		if(TryGetValue(key.ToLower(), out strValue))
		{
			value = XmlConvert.ToInt16(strValue);
			return true;
		}
		else
		{
			value = 0;
			return false;
		}
	}

	public bool TryGetIntValue(string key, out int value)
	{
		string strValue;
		if(TryGetValue(key.ToLower(), out strValue))
		{
			value = XmlConvert.ToInt32(strValue);
			return true;
		}
		else
		{
			value = 0;
			return false;
		}
	}

	public bool TryGetLongValue(string key, out long value)
	{
		string strValue;
		if(TryGetValue(key.ToLower(), out strValue))
		{
			value = XmlConvert.ToInt64(strValue);
			return true;
		}
		else
		{
			value = 0;
			return false;
		}
	}

	public bool TryGetFloatValue(string key, out float value)
	{
		string strValue;
		if(TryGetValue(key.ToLower(), out strValue))
		{
			value = XmlConvert.ToSingle(strValue);
			return true;
		}
		else
		{
			value = 0;
			return false;
		}
	}

	public bool TryGetDoubleValue(string key, out double value)
	{
		string strValue;
		if(TryGetValue(key.ToLower(), out strValue))
		{
			value = XmlConvert.ToDouble(strValue);
			return true;
		}
		else
		{
			value = 0;
			return false;
		}
	}

	public bool TryGetStringValue(string key, out string value)
	{
		return TryGetValue(key.ToLower(), out value);
	}

	public short GetShortValue(string key)
	{
		string strValue;
		if(TryGetValue(key.ToLower(), out strValue))
			return XmlConvert.ToInt16(strValue);
		else
			throw new ArgumentOutOfRangeException("key", "The key \'" + key + "\' does not exist in the dictionary");
	}

	public int GetIntValue(string key)
	{
		string strValue;
		if(TryGetValue(key.ToLower(), out strValue))
			return XmlConvert.ToInt32(strValue);
		else
			throw new ArgumentOutOfRangeException("key", "The key \'" + key + "\' does not exist in the dictionary");
	}

	public long GetLongValue(string key)
	{
		string strValue;
		if(TryGetValue(key.ToLower(), out strValue))
			return XmlConvert.ToInt64(strValue);
		else
			throw new ArgumentOutOfRangeException("key", "The key \'" + key + "\' does not exist in the dictionary");
	}

	public float GetSingleValue(string key)
	{
		string strValue;
		if(TryGetValue(key.ToLower(), out strValue))
			return XmlConvert.ToSingle(strValue);
		else
			throw new ArgumentOutOfRangeException("key", "The key \'" + key + "\' does not exist in the dictionary");
	}

	public double GetDoubleValue(string key)
	{
		string strValue;
		if(TryGetValue(key.ToLower(), out strValue))
			return XmlConvert.ToDouble(strValue);
		else
			throw new ArgumentOutOfRangeException("key", "The key \'" + key + "\' does not exist in the dictionary");
	}

	public string GetStringValue(string key)
	{
		string strValue;
		if(TryGetValue(key.ToLower(), out strValue))
			return strValue;
		else
			throw new ArgumentOutOfRangeException("key", "The key \'" + key + "\' does not exist in the dictionary");
	}
}
