using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class responsible for managing game saves.
/// </summary>
public class SaveManager : MonoBehaviour
{
	/// <summary>
	/// Class representing a saved spawn item.
	/// </summary>
	[Serializable] private class SpawnItemSave
	{
		public SpawnType type;
		public Vector3 position;

		public SpawnItemSave(HighlightItem hItem)
		{
			position = hItem.gameObject.transform.position;
			type = hItem.GetSpawnType();
		}
	};

	/// <summary>
	/// Class representing the structure of a save file.
	/// </summary>
	[Serializable] private class SaveFile
	{
		public Vector3 playerPos;
		public List<SpawnItemSave> spawnItems = new List<SpawnItemSave>();
	};

	private static string filePath;
	private const string saveFile = "virbela.json";

	void Start()
	{
		filePath = $"{Application.persistentDataPath}/{saveFile}";
		Debug.Log($"Save file path set to {filePath}");

		Load();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.S)) {
			TriggerSave();
		}
	}

	/// <summary>
	/// Saves the game state to a file.
	/// </summary>
	static void Save()
	{
		// Fill out structure
		SaveFile file = new SaveFile();

		Player player = FindObjectOfType<Player>();
		if (player != null) {
			file.playerPos = player.transform.position;
		} else {
			Debug.LogError("Couldn't find player, unable to save player data.");
		}

		List<GameObject> allSpawned = SpawnManager.GetAllSpawned();
		foreach (GameObject obj in allSpawned) {
			HighlightItem hItem = obj.GetComponent<HighlightItem>();
			if (hItem != null) {
				SpawnItemSave itemSave = new SpawnItemSave(hItem);
				file.spawnItems.Add(itemSave);
			}
		}

		// TODO use newtonsoft or .NET json utilities. They're better than Unity's.
		string asJson = JsonUtility.ToJson(file);
		// TODO write this async
		// TODO encrypt save file so players don't manually change it
		File.WriteAllText(filePath, asJson);
		Debug.Log($"Save file {filePath}");
	}

	/// <summary>
	/// Loads the game state from a file.
	/// </summary>
	static void Load()
	{
		if (File.Exists(filePath)) {
			Debug.Log($"Loading save file from {filePath}");
			string jsonData = File.ReadAllText(filePath);

			SaveFile file = JsonUtility.FromJson<SaveFile>(jsonData);

			Player player = FindObjectOfType<Player>();
			if (player != null) {
				player.transform.position = file.playerPos;
			}

			GameObject holder = new GameObject("LoadedSpawnItems");
			foreach (SpawnItemSave savedItem in file.spawnItems) {
				SpawnManager.Spawn(savedItem.type, savedItem.position, holder.transform);
			}
		}
	}

	/// <summary>
	/// Triggers the game to save its state.
	/// </summary>
	public static void TriggerSave()
	{
		Save();
	}
}
