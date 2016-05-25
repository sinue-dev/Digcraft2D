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
		SaveChunks();
		SavePlayer();
	}

	public void LoadSaveGame()
	{
		LoadChunks();
		LoadPlayer();
	}

	public void SaveChunks()
	{
		//savedWorlds.Add(WorldManager.I.currentWorld);
		cWorld world = new cWorld(WorldManager.I.SerializableChunks(WorldManager.I.chunks));

		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/test.world");
		bf.Serialize(file, world);
		file.Close();
	}

	public void LoadChunks()
	{
		if (File.Exists(Application.persistentDataPath + "/test.world"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/test.world", FileMode.Open);

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
		else
		{
			WorldManager.I.SpawnPlayer(7, 65, 0);
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
