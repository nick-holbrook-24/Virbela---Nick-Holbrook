using System.Collections.Generic;
using UnityEngine;

namespace ExerciseOne
{
    public class ExerciseManager : MonoBehaviour
    {
        [SerializeField] private ExerciseManagerData exerciseManagerData = null;

        private Player player = null;
        private ObjectPool<Item> itemPool = null;
        private ObjectPool<Bot> botPool = null;
        private ObjectColorChanger objectColorChanger = null;
        private INearestObjectSystem nearestObjectSystem = null;
        private SaveLoadManager saveLoadManager = null;
        private XMLReader xmlReader = null;

        private void OnEnable()
        {
            SpawnEnvironment();
            SpawnPlayer();
            SpawnItems();
            SpawnBots();
            SpawnNearestObjectSystem();
            SpawnObjectColorChanger();
            InitiateLoadManager();
            ReadInXMLItemText();
        }

        private void Update()
        {
            CheckInputForAdditionalItemsBots();
        }

        private void OnDisable()
        {
            SaveData();
        }

        private void SpawnEnvironment()
        {
            Instantiate(exerciseManagerData.environmentPrefab, transform);
        }

        private void SpawnPlayer()
        {
            player = Instantiate(exerciseManagerData.playerPrefab.gameObject, transform).GetComponent<Player>();
        }

        private void SpawnItems()
        {
            itemPool = new ObjectPool<Item>(exerciseManagerData.itemPrefab,
                exerciseManagerData.initialItemCount, transform);
            SpawnObjectsAroundPlayer(itemPool, exerciseManagerData.initialItemCount);
        }

        private void SpawnBots()
        {
            botPool = new ObjectPool<Bot>(exerciseManagerData.botPrefab,
                exerciseManagerData.initialBotCount, transform);
            SpawnObjectsAroundPlayer(botPool, exerciseManagerData.initialBotCount);
        }

        private void SpawnNearestObjectSystem()
        {
            nearestObjectSystem =
                Instantiate(exerciseManagerData.nearestObjectSystem, transform)
                .GetComponent<INearestObjectSystem>();
        }

        private void SpawnObjectColorChanger()
        {
            objectColorChanger = Instantiate(exerciseManagerData.objectColorChanger.gameObject, transform)
                .GetComponent<ObjectColorChanger>();
            objectColorChanger.Initialize(nearestObjectSystem, player.transform,
                exerciseManagerData.secondsBeforeNearestCheck);
        }

        private void InitiateLoadManager()
        {
            saveLoadManager = Instantiate(exerciseManagerData.saveLoadManager.gameObject, transform).GetComponent<SaveLoadManager>();
            saveLoadManager.Load(exerciseManagerData.saveFileName);
        }

        private void ReadInXMLItemText()
        {
            xmlReader = Instantiate(exerciseManagerData.xmlReader).GetComponent<XMLReader>();
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

        private void CheckInputForAdditionalItemsBots()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ExpandItems(exerciseManagerData.numberOfObjectsToAddAtATime);
                ExpandBots(exerciseManagerData.numberOfObjectsToAddAtATime);
            }
        }

        private void SaveData()
        {
            saveLoadManager?.Save(exerciseManagerData.saveFileName);
        }

        private void ExpandItems(int _numberToAdd)
        {
            itemPool.CreateInstances(_numberToAdd);
            SpawnObjectsAroundPlayer(itemPool, _numberToAdd);
        }

        private void ExpandBots(int _numberToAdd)
        {
            botPool.CreateInstances(_numberToAdd);
            SpawnObjectsAroundPlayer(botPool, _numberToAdd);
        }

        private void SpawnObjectsAroundPlayer<T>(ObjectPool<T> _objectPool, int _count) 
            where T : MonoBehaviour, IInteractableObject
        {
            for (int i = 0; i < _count; i++)
            {
                T obj = _objectPool.GetObject();
                obj.transform.position = GetRandomPositionAroundPlayer();
                obj.gameObject.SetActive(true);
            }
        }

        private Vector3 GetRandomPositionAroundPlayer()
        {
            Vector2 randomCirclePoint = 
                Random.insideUnitCircle * exerciseManagerData.spawnRadiusAroundPlayer;
            Vector3 randomPosition = new Vector3(randomCirclePoint.x, 
                Random.Range(0.0f, exerciseManagerData.spawnCeilingFromPlayer), randomCirclePoint.y);

            return player.transform.position + randomPosition;
        }
    }
}