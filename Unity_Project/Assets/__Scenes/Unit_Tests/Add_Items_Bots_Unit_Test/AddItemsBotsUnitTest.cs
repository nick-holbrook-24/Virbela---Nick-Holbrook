using UnityEngine;

namespace ExerciseOne
{
    public class AddItemsBotsUnitTest : MonoBehaviour
    {
        [SerializeField] private ExerciseManagerData exerciseManagerData = null;
        [SerializeField] private SaveLoadManager saveLoadManager = null;

        private AddItemsBotsManager addItemsBotsManager = null;

        private void Awake()
        {
            addItemsBotsManager = 
                Instantiate(exerciseManagerData.addItemsBotsManager).GetComponent<AddItemsBotsManager>();
            addItemsBotsManager.Initialize(saveLoadManager);
        }
    }
}