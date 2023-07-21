using UnityEngine;

namespace ExerciseOne
{
    public class SaveLoadUnitTest : MonoBehaviour
    {
        [SerializeField] private ExerciseManagerData exerciseManagerData = null;

        private SaveLoadManager saveLoadManager = null;

        private void Awake()
        {
            saveLoadManager = 
                Instantiate(exerciseManagerData.saveLoadManager).GetComponent<SaveLoadManager>();
        }

        private void OnEnable()
        {
            saveLoadManager.Load(exerciseManagerData.unitTestSaveFileName);
        }

        private void OnDisable()
        {
            saveLoadManager.Save(exerciseManagerData.unitTestSaveFileName);
        }
    }
}