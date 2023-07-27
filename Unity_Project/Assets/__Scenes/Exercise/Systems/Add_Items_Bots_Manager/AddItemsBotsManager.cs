using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

namespace ExerciseOne
{
    public class AddItemsBotsManager : MonoBehaviour
    {
        [SerializeField] private ExerciseManagerData exerciseManagerData = null;

        private ObjectPool<Item> itemPool = null;
        private ObjectPool<Bot> botPool = null;
        private Transform itemPoolParent = null;
        private Transform botPoolParent = null;
        private SaveLoadManager saveLoadManager = null;

        public void Initialize(SaveLoadManager _saveLoadManager)
        {
            saveLoadManager = _saveLoadManager;
        }

        public ObjectPool<Item> GetItemPool()
        {
            return itemPool;
        }

        public ObjectPool<Bot> GetBotPool()
        {
            return botPool;
        }

        public Transform GetItemPoolParent()
        {
            return itemPoolParent;
        }

        public Transform GetBotPoolParent()
        {
            return botPoolParent;
        }

        private void Awake()
        {
            SpawnStartingItems();
            SpawnStartingBots();
        }

        private void Update()
        {
            CheckInputForAdditionalItemsBots();
        }

        private void CheckInputForAdditionalItemsBots()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ExpandItems(exerciseManagerData.numberOfObjectsToAddAtATime);
                ExpandBots(exerciseManagerData.numberOfObjectsToAddAtATime);
            }
        }

        private void SpawnStartingItems()
        {
            itemPoolParent = new GameObject("Item_Pool_Parent").transform;
            itemPoolParent.SetParent(transform);

            itemPool = new ObjectPool<Item>(exerciseManagerData.itemPrefab,
                exerciseManagerData.itemCount, itemPoolParent);
            SpawnObjectsAroundPlayer(itemPool, exerciseManagerData.itemCount);
            ReapplyItemsNames();
        }

        private void SpawnStartingBots()
        {
            botPoolParent = new GameObject("Bot_Pool_Parent").transform;
            botPoolParent.SetParent(transform);

            botPool = new ObjectPool<Bot>(exerciseManagerData.botPrefab,
                exerciseManagerData.botCount, botPoolParent);
            SpawnObjectsAroundPlayer(botPool, exerciseManagerData.botCount);
            ReapplyBotsNames();
        }

        private void ExpandItems(int _numberToAdd)
        {
            for (int i = 0; i < _numberToAdd; i++)
            {
                Item newItem = itemPool.GetObject();
                SpawnObjectAroundPlayer(newItem);
                newItem.transform.name = "Item #" + itemPool.GetAllActiveObjects().Count;
                newItem.transform.SetParent(itemPoolParent);
                saveLoadManager.RegisterObjectDataHandler(newItem);
                ReapplyItemsNames();
                exerciseManagerData.itemCount++;
            }
        }

        private void ExpandBots(int _numberToAdd)
        {
            for (int i = 0; i < _numberToAdd; i++)
            {
                Bot newBot = botPool.GetObject();
                SpawnObjectAroundPlayer(newBot);
                newBot.transform.name = "Bot #" + botPool.GetAllActiveObjects().Count;
                newBot.transform.SetParent(botPoolParent);
                saveLoadManager.RegisterObjectDataHandler(newBot);
                ReapplyBotsNames();
                exerciseManagerData.botCount++;
            }
        }

        private void SpawnObjectAroundPlayer<T>(T _object)
                where T : MonoBehaviour, IInteractableObject
        {
            _object.transform.position = GetRandomPositionAroundOrigin();
            _object.gameObject.SetActive(true);
        }

        private void SpawnObjectsAroundPlayer<T>(ObjectPool<T> _objectPool, int _count)
                where T : MonoBehaviour, IInteractableObject
        {
            for (int i = 0; i < _count; i++)
            {
                T obj = _objectPool.GetObject();
                obj.transform.position = GetRandomPositionAroundOrigin();
                obj.gameObject.SetActive(true);
            }
        }

        private Vector3 GetRandomPositionAroundOrigin()
        {
            Vector2 randomCirclePoint =
                Random.insideUnitCircle * exerciseManagerData.spawnRadiusAroundPlayer;
            Vector3 randomPosition = new Vector3(randomCirclePoint.x,
                Random.Range(0.0f, exerciseManagerData.spawnCeilingFromPlayer), randomCirclePoint.y);

            return randomPosition;
        }
    
        private void ReapplyItemsNames()
        {
            List<Item> items = itemPool.GetAllActiveObjects();

            for (int i = 0; i < itemPoolParent.childCount; i++)
            {
                items[i].gameObject.name = "Item #" + i;
            }
        }

        private void ReapplyBotsNames()
        {
            List<Bot> bots = botPool.GetAllActiveObjects();

            for (int i = 0; i < botPoolParent.childCount; i++)
            {
                bots[i].gameObject.name = "Bot #" + i;
            }
        }
    }
}