using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.IO;

public class MenuManager : Singleton<MenuManager> {

	private GameObject activeMenu;

	public GameObject menuMain;
	public GameObject menuSingleplayerSavegames;

	private Menus_e menu = Menus_e.MAIN;

	public enum Menus_e
	{
		MAIN,
		SPSAVEGAMES
	}

	private void Start()
	{
		DontDestroyOnLoad(GameObject.Find("_"));

		if (activeMenu == null) activeMenu = menuMain;
	}

	public void ToggleSingleplayerSavegameMenu()
	{
		switch(menu)
		{
			case Menus_e.MAIN:
				activeMenu.SetActive(false);
				menuSingleplayerSavegames.SetActive(true);
				activeMenu = menuSingleplayerSavegames;
				menu = Menus_e.SPSAVEGAMES;

				LoadSavegamesToList();
				break;
			case Menus_e.SPSAVEGAMES:
				activeMenu.SetActive(false);
				menuMain.SetActive(true);
				activeMenu = menuMain;
				menu = Menus_e.MAIN;
				break;

			default:
				activeMenu.SetActive(false);
				menuMain.SetActive(true);
				activeMenu = menuMain;
				menu = Menus_e.MAIN;
				break;
		}

		//SceneManager.LoadScene("world");
	}

	public void LoadSingpleplayerSavegame(string savegame)
	{
		GlobalManager.I.savegame = savegame;
		SceneManager.LoadScene("world");
	}

	private void LoadSavegamesToList()
	{
		DirectoryInfo di = new DirectoryInfo(Application.persistentDataPath);

		if (!di.Exists)
		{
			return;
		}


		//foreach (System.IO.DirectoryInfo d in directory.GetDirectories())
		//{
		//	lbFiles.Items.Add("Datei: " + f.Name);
		//}

	}

	//public FileInfo[] GetDirectoryList()
	//{

	//	string path = Application.persistentDataPath + savePath;
	//	DirectoryInfo info = new DirectoryInfo(path);
	//	FileInfo[] fileInfo = info.getdfi();

	//	return fileInfo;
	//}

	IEnumerator AsynchronousLoad(string scene)
	{
		yield return null;

		AsyncOperation ao = SceneManager.LoadSceneAsync(scene);
		ao.allowSceneActivation = false;

		while (!ao.isDone)
		{
			// [0, 0.9] > [0, 1]
			float progress = Mathf.Clamp01(ao.progress / 0.9f);
			Debug.Log("Loading progress: " + (progress * 100) + "%");

			// Loading completed
			if (ao.progress == 0.9f)
			{
				Debug.Log("Press a key to start");
				if (Input.anyKey)
					ao.allowSceneActivation = true;
			}

			yield return null;
		}
	}

}
