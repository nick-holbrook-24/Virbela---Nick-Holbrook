using UnityEngine;

namespace ExerciseOne
{
    public class AddItemsBotsUnitTest : MonoBehaviour
    {
        [SerializeField] private ExerciseManagerData exerciseManagerData = null;

        private ObjectPool<Item> itemPool = null;
        private ObjectPool<Bot> botPool = null;

        private void Start()
        {
            SpawnItems();
            SpawnBots();
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
    }
}