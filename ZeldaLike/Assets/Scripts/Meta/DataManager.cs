using UnityEngine;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using Game;
using UnityEngine.SceneManagement;

namespace Management
{
	[XmlRoot("Data")]
	public class DataManager : MonoBehaviour
	{
		[SerializeField] GameObject avatar;
		[SerializeField] Scr_FormeHandler forme;
		[SerializeField] PlayerLife playerLifeSyst;


		public static DataManager instance;
		public string path;

		private XmlSerializer serializer = new XmlSerializer(typeof(Data));
		private Encoding encoding = Encoding.GetEncoding("UTF-8");

		public void Awake()
		{
			instance = this;
			SetPaths();
		}

		public void Save(int saveNumber, bool heavyFormUnlock, int lifePoint, int maxLifePoint)
		{
			heavyFormUnlock = forme._heavyFormeUnlock;
			lifePoint = playerLifeSyst.life;
			maxLifePoint = playerLifeSyst.maxlife;
			int sceneAct = SceneManager.GetActiveScene().buildIndex;
			Vector3 plPos = avatar.transform.position;

			StreamWriter streamWriter = new StreamWriter(path, false, encoding);
			Data data = new Data
			{
				_heavyFormUnlock = heavyFormUnlock,

				_lifePoint = lifePoint,
				_maxLifePoint = maxLifePoint,
				_sceneAct = sceneAct,
				_plPos = plPos,
			};

			serializer.Serialize(streamWriter, data);
		}

		public void Load()
		{
			Data data;

			if (File.Exists(path))
			{
				FileStream fileStream = new FileStream(path, FileMode.Open);

				data = serializer.Deserialize(fileStream) as Data;

				forme._heavyFormeUnlock = data._heavyFormUnlock;

				playerLifeSyst.life = data._lifePoint;
				playerLifeSyst.maxlife = data._maxLifePoint;

				SceneManager.LoadScene(data._sceneAct);
				
				avatar.transform.position = data._plPos;
			}

		}



		public void SetPaths()
		{
			path = Path.Combine(Application.persistentDataPath, "Data0.xml");
		}
	}
}