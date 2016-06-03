using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveLoadManager : Singleton<SaveLoadManager> {

	public static List<cWorld> savedWorlds = new List<cWorld>();

	public void CreateSaveGame()
	{
		SaveChunks(string.Format("{0}/{1}/{1}.world", Application.persistentDataPath, GlobalManager.I.savegame));
		SavePlayer();
	}

	public void LoadSaveGame()
	{
		if (!System.IO.Directory.Exists(Application.persistentDataPath + "/" + GlobalManager.I.savegame)) return;

		LoadChunks(string.Format("{0}/{1}/{1}.world", Application.persistentDataPath, GlobalManager.I.savegame));
		LoadPlayer();
	}

	public void SaveChunks(string sFile)
	{
		cWorld world = new cWorld(WorldManager.I.SerializableChunks(WorldManager.I.chunks));

		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(sFile);
		bf.Serialize(file, world);
		file.Close();
	}

	public void LoadChunks(string sFile)
	{
		if (File.Exists(sFile))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(sFile, FileMode.Open);

			cWorld world = (cWorld)bf.Deserialize(file);
			file.Close();

			WorldManager.I.PopulateLoadedChunks(world.chunks);
		}
	}

	public void SavePlayer()
	{
		cPlayer player = new cPlayer(WorldManager.I.player.transform.position.x, WorldManager.I.player.transform.position.y, WorldManager.I.player.GetComponent<Player>().direction);

		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/test.player");
		bf.Serialize(file, player);
		file.Close();
	}

	public void LoadPlayer()
	{
		if (File.Exists(Application.persistentDataPath + "/test.player"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/test.player", FileMode.Open);

			cPlayer player = (cPlayer)bf.Deserialize(file);
			file.Close();

			if(WorldManager.I.player != null)
			{
				GameObject.Destroy(WorldManager.I.player);
			}

			WorldManager.I.SpawnPlayer(player.PositionX, player.PositionY, player.direction);

		}
	}

	[System.Serializable]
	public class cWorld
	{
		public List<cChunk> chunks { get; set; }

		public cWorld(List<cChunk> chunks)
		{
			this.chunks = chunks;
		}
	}
}
