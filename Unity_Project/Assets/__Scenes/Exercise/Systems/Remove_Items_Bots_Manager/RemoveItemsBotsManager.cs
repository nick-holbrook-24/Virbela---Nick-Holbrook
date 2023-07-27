using System.Collections.Generic;
using UnityEngine;

namespace ExerciseOne
{
    public class RemoveItemsBotsManager : MonoBehaviour
    {
        [SerializeField] private ExerciseManagerData exerciseManagerData = null;

        private ObjectPool<Item> itemPool = null;
        private ObjectPool<Bot> botPool = null;
        private Transform itemPoolParent = null;
        private Transform botPoolParent = null;
        private SaveLoadManager saveLoadManager = null;
        
        public void Initialize(SaveLoadManager _saveLoadManager, 
            ObjectPool<Item> _itemPool, ObjectPool<Bot> _botPool,
            Transform _itemPoolParent, Transform _botPoolParent)
        {
            saveLoadManager = _saveLoadManager;
            itemPool = _itemPool;
            botPool = _botPool;
            itemPoolParent = _itemPoolParent;
            botPoolParent = _botPoolParent;
        }

        private void Update()
        {
            CheckInputForRemovingItemsBots();
        }

        private void CheckInputForRemovingItemsBots()
        {
            if(Input.GetKeyDown(KeyCode.Delete))
            {
                RemoveRandomItemsBots(exerciseManagerData.numberOfObjectsToRemoveAtATime);
                ReapplyItemsNames();
                ReapplyBotsNames();
            }
        }

        private void RemoveRandomItemsBots(int _randomNumberOfEachItemsBotsPoolToBeRemoved)
        {
            if (_randomNumberOfEachItemsBotsPoolToBeRemoved >= itemPool.GetAllActiveObjects().Count)
            {
                Debug.Log("There are not enough items left to remove this amount of them: "
                   + _randomNumberOfEachItemsBotsPoolToBeRemoved);
                return;
            }

            if (_randomNumberOfEachItemsBotsPoolToBeRemoved >= botPool.GetAllActiveObjects().Count)
            {
                Debug.Log("There are not enough bots left to remove this amount of them: "
                   + _randomNumberOfEachItemsBotsPoolToBeRemoved);
                return;
            }

            for (int i = 0; i < _randomNumberOfEachItemsBotsPoolToBeRemoved; i++)
            {
                RemoveObject<Item>(itemPool.GetARandomActiveObject());
                RemoveObject<Bot>(botPool.GetARandomActiveObject());

                exerciseManagerData.itemCount--;
                exerciseManagerData.botCount--;
            }
        }

        private void RemoveObject<T>(T obj) where T : MonoBehaviour, IInteractableObject, IObjectDataHandler
        {
            if (obj is Item)
            {
                itemPool.ReturnObject(obj as Item);
            }
            else if (obj is Bot)
            {
                botPool.ReturnObject(obj as Bot);
            }

            saveLoadManager.DeregisterObjectDataHandler(obj);

            obj.transform.SetParent(transform);
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