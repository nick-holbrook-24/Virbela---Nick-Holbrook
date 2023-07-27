using UnityEngine;

namespace ExerciseOne
{
    [CreateAssetMenu(fileName = "Nick/Exercise_Manager_Data",
    menuName = "Nick/Exercise Manager Data", order = 1)]
    public class ExerciseManagerData : ScriptableObject
    {
        [Header("Environment")]
        public GameObject environmentPrefab = null;

        [Header("Player")]
        public Player playerPrefab;
        public Material playerDefaultMaterial = null;
        public Color playerDefaultColor = Color.clear;
        public float playerMovementSpeed;
        public float playerRotationSpeed;
        public float playerRunSpeed = 0.0f;
        public float playerCurrentRunStamina = 0.0f;
        public float playerRunMaxStamina = 0.0f;
        public float playerRateOfStaminaLoss = 0.0f;
        public float playerRateOfStaminaGain = 0.0f;
        [Range(0f, 90f)] public float playerYRotationLimit = 87f;

        [Header("Interactable Objects")]
        public Item itemPrefab;
        public int itemCount = 5;
        public Color itemBaseColor = Color.white;
        public Color itemHighlightColor = Color.red;
        public Bot botPrefab;
        public int botCount = 5;
        public Color botBaseColor = Color.white;
        public Color botHighlightColor = Color.blue;
        public float spawnRadiusAroundPlayer = 10.0f;
        public float spawnCeilingFromPlayer = 3.0f;
        public int numberOfObjectsToAddAtATime = 5;
        public int numberOfObjectsToRemoveAtATime = 3;

        [Header("Add Item Bots Manager")]
        public AddItemsBotsManager addItemsBotsManager = null;
        
        [Header("Remove Item Bots Manager")]
        public RemoveItemsBotsManager removeItemsBotsManager = null;

        [Header("Object Color Changer")]
        public ObjectColorChanger objectColorChanger = null;

        [Header("Nearest Object System")]
        public GameObject nearestObjectSystem = null;
        public float searchRadius = 5.0f;
        public float secondsBeforeNearestCheck = 5.0f;

        [Header("Save Load System")]
        public SaveLoadManager saveLoadManager = null;
        public string saveFileName = "saveData.json";
        public string unitTestSaveFileName = "unitTestSaveData.json";

        [Header("XML Reader")]
        public XMLReader xmlReader = null;
        public string itemXMLFileName = null;
    }
}