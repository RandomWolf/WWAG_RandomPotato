using BepInEx;
using System;
using Unity;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Wizgun;
using System.Collections.Generic;
using UI;
using Skins;
using Inventory;
using Inventory.Unity;
namespace WWAG_RandomPotato
{
	[BepInPlugin("org.NikoTheFox.RandomPotato", "WWAG Random Main Menu Wizard", "0.0.3")]
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
				// This adds my mod and a BoxCollider to the main menu wizard, so it can be clicked
				Debug.Log($"Found scene, randomizing Wiz");
				GameObject spine = GameObject.Find("background-canvas/player-animation");
				spine.AddComponent<PluginRandomWiz>();
				spine.AddComponent<BoxCollider>();

			}
			if (scene.name == "core-simple-all")
			{

				// This code adds the new randomize Button by copying the Confirm button
				Debug.Log($"Found customizestuff");
				GameObject obj = GameObject.Find("ui/cross-world-ui(Clone)/customize-wizard-ui/window/item-selection-panel/finalize-selectable-button");
				if (obj == null)
				{
					Debug.Log("Couldnt find the object to copy");
					return;
				}
				obj.transform.position = new Vector3(490, 150, 0);
				obj.transform.localPosition = new Vector3(-500, -390, 0);
				RectTransform rectTransform = obj.GetComponent<RectTransform>();
				rectTransform.sizeDelta = new Vector2(200, rectTransform.sizeDelta.y);

				GameObject copiedObj = Instantiate(obj);

				copiedObj.transform.SetParent(obj.transform.parent, false);
				copiedObj.name = "randomize";
				copiedObj.transform.localPosition = new Vector3(-290, -390, 0);
				RectTransform randTransform = copiedObj.GetComponent<RectTransform>();
				randTransform.sizeDelta = new Vector2(220, rectTransform.sizeDelta.y);

				GameObject randoText = copiedObj.transform.Find("Text").gameObject;
				TMPro.TextMeshProUGUI textmesh = randoText.GetComponent<TMPro.TextMeshProUGUI>();
				textmesh.text = "Randomize";
				UI.SelectableButton button = copiedObj.GetComponent<UI.SelectableButton>();
				WWAG_RandomPotato.PluginRandomCustomizeWiz plugin = copiedObj.AddComponent<WWAG_RandomPotato.PluginRandomCustomizeWiz>();
				button.SetClickAction(new UnityEngine.Events.UnityAction(() => plugin.ChangeWizard()));


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

			// This function gets a list of all "early-game" outfits from all SkinGroup objects that are loaded
			// and then equips the Main Menu wizard with those objects
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

	public class PluginRandomCustomizeWiz : MonoBehaviour
	{
		private PlayerSpawnGameData _spawnGameData;
		private Dictionary<ItemType, ItemAssetHandle> _currentEquipmentItems = new Dictionary<ItemType, ItemAssetHandle>();
		private SkinGroup _currentEyeShape;
		private Color _currentEyeColor;
		public PlayerSkins wizardPreview;

		void Awake()
        {

			// I have to redo the text here, for some reason otherwise it doesnt stick?
			GameObject randoText = this.transform.Find("Text").gameObject;
			if (randoText == null)
			{
				Debug.Log("Couldnt find the text object.");
				return;
			}
			TMPro.TextMeshProUGUI textmesh = randoText.GetComponent<TMPro.TextMeshProUGUI>();
			textmesh.text = "Randomize";
		}

		public void ChangeWizard()
        {

			// MOST OF THIS FUNCTION IS BY GALVANIC, I will rewrite it with my own code if they tell me to
			// pls dont sue me

			Debug.Log($"Randomizing Wiz");
			GameObject customWizard = GameObject.Find("ui/cross-world-ui(Clone)/customize-wizard-ui/window/item-selection-panel/player-animation");
			GameObject mainUI = GameObject.Find("ui/cross-world-ui(Clone)/customize-wizard-ui");
			UI.CustomizeWizardUI wizardUI = mainUI.GetComponent<UI.CustomizeWizardUI>();

			
			wizardPreview = customWizard.GetComponent<PlayerSkins>();
			this._spawnGameData = GameDataCache.GetGameData<PlayerSpawnGameData>();

			Unity.Mathematics.Random random = new Unity.Mathematics.Random();

			// This was modified from the Galvanic Version from Seconds to Milliseconds
			random.InitState((uint)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());
			if (this._spawnGameData.eyeShapeOptions.Length != 0)
			{
				SkinGroup eyeShapeOption = this._spawnGameData.eyeShapeOptions[random.NextInt(0, this._spawnGameData.eyeShapeOptions.Length)];
				this.wizardPreview.SetEquipSlot(eyeShapeOption, EquipSlot.Eyes);
				this._currentEyeShape = eyeShapeOption;
			}
			if (this._spawnGameData.eyeColorOptions.Length != 0)
			{
				Color eyeColorOption = this._spawnGameData.eyeColorOptions[random.NextInt(0, this._spawnGameData.eyeColorOptions.Length)];
				this.wizardPreview.SetSlotColor(ColorSlot.Eyes, eyeColorOption);
				this.wizardPreview.SetAllColors();
				this._currentEyeColor = eyeColorOption;
			}

			foreach (PlayerSpawnGameData.EquipmentOptions equipmentOption in this._spawnGameData.equipmentOptions)
			{
				if (equipmentOption.options.Length != 0)
				{
					int index = random.NextInt(0, equipmentOption.options.Length);
					GameObject option = equipmentOption.options[index];
					StaticItemEquipmentComponent component = option.GetComponent<StaticItemEquipmentComponent>();
					EquipSlot slot;
					if (SkinsUtil.TryGetEquipSlot(equipmentOption.itemType, out slot))
					{
						this.wizardPreview.SetEquipSlot(component.skinGroup, slot);
						this._currentEquipmentItems[equipmentOption.itemType] = new ItemAssetHandle(option);
					}
				}
			}

			// This snippet here is not part of the Galvanic Code
			System.Reflection.FieldInfo eyeShapeField = typeof(CustomizeWizardUI).GetField("_currentEyeShape", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
			eyeShapeField.SetValue(wizardUI, _currentEyeShape);

			System.Reflection.FieldInfo equipment = typeof(CustomizeWizardUI).GetField("_currentEquipmentItems", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
			equipment.SetValue(wizardUI, _currentEquipmentItems);

		}
	}
}