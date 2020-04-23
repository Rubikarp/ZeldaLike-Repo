using UnityEngine;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Text;

namespace Management
{
	[XmlRoot("Data")]
	public class DataManager : MonoBehaviour
	{
		public static DataManager instance;
		public string path;

		private XmlSerializer serializer = new XmlSerializer(typeof(Data));
		private Encoding encoding = Encoding.GetEncoding("UTF-8");

		public void Awake()
		{
			instance = this;
			SetPath();
		}
		public void Save(int numberPoint)
		{
			StreamWriter streamWriter = new StreamWriter(path, false, encoding);
			Data data = new Data
			{

			};

			serializer.Serialize(streamWriter, data);
		}
		public Data Load()
		{
			if (File.Exists(path))
			{
				FileStream fileStream = new FileStream(path, FileMode.Open);

				return serializer.Deserialize(fileStream) as Data;
			}

			return null;
		}

		public void SetPath()
		{
			path = Path.Combine(Application.persistentDataPath, "Data.xml");
		}
	}
}