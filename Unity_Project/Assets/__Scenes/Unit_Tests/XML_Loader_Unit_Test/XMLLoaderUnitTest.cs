using System.Collections.Generic;
using UnityEngine;

namespace ExerciseOne
{
    public class XMLReaderUnitTest : MonoBehaviour
    {
        [SerializeField] private ExerciseManagerData exerciseManagerData = null;

        private XMLReader xmlReader = null;

        private void Awake()
        {
            xmlReader = Instantiate(exerciseManagerData.xmlReader).GetComponent<XMLReader>();
        }

        private void OnEnable()
        {
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
    }
}