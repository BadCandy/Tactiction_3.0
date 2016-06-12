using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;

public class PmlLoader : Singleton<PmlLoader>, IDisposable
{
	Dictionary<string, PmlElement> _loadedElements = new Dictionary<string, PmlElement>();

	public PmlElement Load(string path)
	{
		TextAsset xmlFile = Resources.Load<TextAsset>(path);
		PmlElement data;

		if(_loadedElements.TryGetValue(path, out data))
			return data;

		try
		{
			MemoryStream assetStream = new MemoryStream(xmlFile.bytes);
			XmlReader reader = XmlReader.Create(assetStream);
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.Load(reader);

			XmlNodeReader node = new XmlNodeReader(xmlDoc);
			Debug.Log("XML \'" + path + "\' has loaded.");

			// Read the nodes recursively
			data = new PmlElement();
			ReadNodes(node, data);

			node.Close();
			
			_loadedElements.Add(path, data);
		}
		catch(Exception ex)
		{
			Debug.LogError("Error loading " + path + ":\n" + ex);
		}

		return data;
	}

	public void Dispose()
	{
		_loadedElements.Clear();
	}

	public bool Dispose(string path)
	{
		return _loadedElements.Remove(path);
	}

	PmlElement ReadNodes(XmlNodeReader node,
	                    PmlElement data,
	                    string path = "",
	                    string elementName = "")
	{
		while(node.Read())
		{
			if(node.NodeType == XmlNodeType.Element)
			{
				string type = node.GetAttribute("type");
				// Directory element
				if((type ?? string.Empty).Equals("dir", StringComparison.OrdinalIgnoreCase))
					ReadNodes(node, data,
					         (string.IsNullOrEmpty(path) ? string.Empty : (path + "/")) + node.Name,
					         node.Name);
				// Link element
				else if((type ?? string.Empty).Equals("link", StringComparison.OrdinalIgnoreCase))
				{
					string linkPath = node.GetAttribute("path");
					PmlElement linkedElement;
					if(!_loadedElements.TryGetValue(linkPath, out linkedElement))
					{
						linkedElement = Load(linkPath);
					}
					data.Add((path + "/" + node.Name).ToLower(), linkedElement.GetStringValue(node.GetAttribute("data")));
				}
				// Common data element
				else
				{
					data.Add((path + "/" + node.Name).ToLower(), node.ReadString());
				}
			}
			else if(node.NodeType == XmlNodeType.Element)
			{
				if(node.Name.Equals(elementName, StringComparison.OrdinalIgnoreCase))
				   break;
			}
		}

		return data;
	}
}
