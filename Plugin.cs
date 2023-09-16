using BepInEx;
using System;
using Unity;
using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;
using Wizgun;
using System.Collections.Generic;

namespace WWAG_RandomPotato
{
	[BepInPlugin("org.NikoTheFox.RandomPotato", "WWAG Random Main Menu Wizard", "0.0.1")]
	public class Plugin : BaseUnityPlugin
	{
		private void Awake()
		{
			Debug.Log($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
		}
		void OnSceneLoaded(Scene scene, LoadSceneMode mode)
		{
			if (scene.name == "title-screen")
			{

				Debug.Log($"Found scene, randomizing Wiz");
				GameObject spine = GameObject.Find("background-canvas/player-animation");
				spine.AddComponent<PluginRandomWiz>();
				spine.AddComponent<BoxCollider>();

			}
		}
		void OnEnable()
		{
			Debug.Log($"Plugin enabled");
			SceneManager.sceneLoaded += OnSceneLoaded;
		}
	}
	public class PluginRandomWiz : MonoBehaviour
	{
		private void Awake()
        {
			ChangeWizard();
		}

		void ChangeWizard()
        {
			GameObject spine = GameObject.Find("background-canvas/player-animation");
			Skins.PlayerSkins skin = spine.GetComponent<Skins.PlayerSkins>();
			Skins.SkinGroup[] allSkinGroups = Resources.FindObjectsOfTypeAll<Skins.SkinGroup>();

			List<Skins.SkinGroup> applicableHats = new List<Skins.SkinGroup>();
			List<Skins.SkinGroup> applicableGloves = new List<Skins.SkinGroup>();
			List<Skins.SkinGroup> applicableRobes = new List<Skins.SkinGroup>();
			List<Skins.SkinGroup> applicableBoots = new List<Skins.SkinGroup>();
			List<Skins.SkinGroup> applicableEyes = new List<Skins.SkinGroup>();

			System.Random rand = new System.Random();

			string[] values = {
					"crimson",
					"warlock",
					"mustard",
					"engineer",
					"magus",
					"oakley",
					"azure"
				};

			string[] valuesEyes = {
					"alchemist",
					"warlock",
					"mustard",
					"engineer",
					"magus",
					"oakley",
					"azure",
					"calamity",
					"corsair",
					"crimson",
					"farmhand",
					"necromancer",
					"occultist",
					"maninblack"
				};

			foreach (Skins.SkinGroup skinGroup in allSkinGroups)
			{
				foreach (string val in values)
				{
					if (skinGroup.name == "sg-hat-" + val)
					{
						applicableHats.Add(skinGroup);
					}
					if (skinGroup.name == "sg-robes-" + val)
					{
						applicableRobes.Add(skinGroup);
					}
					if (skinGroup.name == "sg-gloves-" + val)
					{
						applicableGloves.Add(skinGroup);
					}
					if (skinGroup.name == "sg-boots-" + val)
					{
						applicableBoots.Add(skinGroup);
					}

				}

				foreach (string valEye in valuesEyes)
				{
					if (skinGroup.name == "sg-eyes-" + valEye)
					{
						applicableEyes.Add(skinGroup);
					}
				}

			}

			// Get a random hat
			if (applicableHats.Count > 0)
			{
				Skins.SkinGroup randomHat = applicableHats[rand.Next(applicableHats.Count)];
				skin.SetEquipSlot(randomHat, Skins.EquipSlot.Hat);
			}
			// Get a random glove
			if (applicableGloves.Count > 0)
			{
				Skins.SkinGroup randomGlove = applicableGloves[rand.Next(applicableGloves.Count)];
				skin.SetEquipSlot(randomGlove, Skins.EquipSlot.Gloves);
			}

			// Get a random robe
			if (applicableRobes.Count > 0)
			{
				Skins.SkinGroup randomRobe = applicableRobes[rand.Next(applicableRobes.Count)];
				skin.SetEquipSlot(randomRobe, Skins.EquipSlot.Robes);
			}
			// Get a random boot
			if (applicableBoots.Count > 0)
			{
				Skins.SkinGroup randomBoot = applicableBoots[rand.Next(applicableBoots.Count)];
				skin.SetEquipSlot(randomBoot, Skins.EquipSlot.Boots);
			}

			// Get a random boot
			if (applicableEyes.Count > 1)
			{

				Skins.SkinGroup randomEyes = applicableEyes[rand.Next(applicableEyes.Count)];
				skin.SetEquipSlot(randomEyes, Skins.EquipSlot.Eyes);
			}
		}
		// Update is called once per frame
		void Update()
		{
			// Check if the mouse was clicked over the GameObject with this script
			if (Input.GetMouseButtonDown(0))
			{  // 0 is for the left mouse button
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit))
				{
					if (hit.transform == transform)
					{
						// The object was clicked! Do something!
						OnObjectClicked();
					}
				}
			}
		}

		void OnObjectClicked()
		{
			ChangeWizard();
		}

	}
}