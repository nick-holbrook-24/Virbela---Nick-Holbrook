using UnityEngine;

namespace ExerciseOne
{
    public class ObjectPoolUnitTest : MonoBehaviour
    {
        [SerializeField] private ExerciseManagerData exerciseManagerData = null;

        private ObjectPool<Item> itemPool = null;
        private ObjectPool<Bot> botPool = null;

        private void Start()
        {
            SpawnItems();
            SpawnBots();
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