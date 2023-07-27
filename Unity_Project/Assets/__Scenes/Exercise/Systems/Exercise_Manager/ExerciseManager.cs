using System.Collections.Generic;
using UnityEngine;

namespace ExerciseOne
{
    public class ExerciseManager : MonoBehaviour
    {
        [SerializeField] private ExerciseManagerData exerciseManagerData = null;

        private Player player = null;
        private AddItemsBotsManager addItemsBotsManager = null;
        private RemoveItemsBotsManager removeItemsBotsManager = null;
        private ObjectColorChanger objectColorChanger = null;
        private INearestObjectSystem nearestObjectSystem = null;
        private SaveLoadManager saveLoadManager = null;
        private XMLReader xmlReader = null;
        private bool hasInitialized = false;

        private void OnEnable()
        {
            if (hasInitialized == false)
            {
                InitializeEnvironment();
                InitializePlayer();
                InitializeAddInteractableObjects();
                InitializeRemoveInteractableObjects();
                InitializeNearestObjectSystem();
                InitializeObjectColorChanger();
                InitializeXMLItemText();
                InitializeSaveLoadManager();
            }

            hasInitialized = true;
            LoadData();
        }

        private void OnDisable()
        {
            SaveData();
        }

        private void InitializeEnvironment()
        {
            Instantiate(exerciseManagerData.environmentPrefab, transform);
        }

        private void InitializePlayer()
        {
            player = Instantiate(exerciseManagerData.playerPrefab.gameObject, transform).GetComponent<Player>();
        }

        private void InitializeAddInteractableObjects()
        {
            addItemsBotsManager = Instantiate(exerciseManagerData.addItemsBotsManager.gameObject, transform)
                .GetComponent<AddItemsBotsManager>();
        }

        private void InitializeRemoveInteractableObjects()
        {
            removeItemsBotsManager = Instantiate(exerciseManagerData.removeItemsBotsManager.gameObject, transform)
                .GetComponent<RemoveItemsBotsManager>();
        }

        private void InitializeNearestObjectSystem()
        {
            nearestObjectSystem =
                Instantiate(exerciseManagerData.nearestObjectSystem, transform)
                .GetComponent<INearestObjectSystem>();
        }

        private void InitializeObjectColorChanger()
        {
            objectColorChanger = Instantiate(exerciseManagerData.objectColorChanger.gameObject, transform)
                .GetComponent<ObjectColorChanger>();
            objectColorChanger.Initialize(nearestObjectSystem, player.transform,
                exerciseManagerData.secondsBeforeNearestCheck);
        }

        private void InitializeXMLItemText()
        {
            xmlReader = Instantiate(exerciseManagerData.xmlReader, transform).GetComponent<XMLReader>();
            List<List<string>> itemStringLists =
                xmlReader.GetXMLTextLists(exerciseManagerData.itemXMLFileName);
            for (int i = 0; i < itemStringLists.Count; i++)
            {
                for (int j = 0; j < itemStringLists[i].Count; j++)
                {
                    Debug.Log($"{i}-{j}: {itemStringLists[i][j]}");
                }
            }
        }

        private void InitializeSaveLoadManager()
        {
            saveLoadManager = Instantiate(exerciseManagerData.saveLoadManager.gameObject, transform).GetComponent<SaveLoadManager>();
            
            addItemsBotsManager.Initialize(saveLoadManager);
            removeItemsBotsManager.Initialize(saveLoadManager, addItemsBotsManager.GetItemPool(),
                addItemsBotsManager.GetBotPool(), addItemsBotsManager.GetItemPoolParent(), 
                addItemsBotsManager.GetBotPoolParent());
        }

        private void LoadData()
        {
            saveLoadManager.Load(exerciseManagerData.saveFileName);
        }

        private void SaveData()
        {
            saveLoadManager?.Save(exerciseManagerData.saveFileName);
        }
    }
}