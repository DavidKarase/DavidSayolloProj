using System;
using System.Xml.Serialization;

[XmlRoot(ElementName = "VAST")]
public class VastData
{
	[XmlElement(ElementName = "Ad")]
	public Ad Ad;
}

public class Ad
{
	[XmlElement(ElementName = "InLine")]
	public InLine InLine;
}

public class InLine
{
	[XmlElement(ElementName = "Error")]
	public object Error;

	[XmlElement(ElementName = "Creatives")]
	public Creatives Creatives;
}

public class Creatives
{
	[XmlElement(ElementName = "Creative")]
	public Creative Creative;
}

public class Creative
{
	[XmlElement(ElementName = "Linear")]
	public Linear Linear;
}

public class Linear
{
	[XmlElement(ElementName = "Duration")]
	public DateTime Duration;

	[XmlElement(ElementName = "MediaFiles")]
	public MediaFiles MediaFiles;
}

public class MediaFiles
{
	[XmlElement(ElementName = "MediaFile")]
	public string MediaFile;
}