using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ExerciseOne
{
    public class SaveLoadManager : MonoBehaviour
    {
        public IDataPersistence<ObjectData> dataPersistence = null;
        public List<IObjectDataHandler> objectDataHandlerList = null;

        public void Save(string _saveFileName)
        {
            List<ObjectData> objectData = new List<ObjectData>();
            foreach(IObjectDataHandler objectDataHandler in objectDataHandlerList)
            {
                if (objectDataHandler == null)
                {
                    continue;
                }

                objectData.Add(objectDataHandler.ExtractObjectData());
            }

            dataPersistence.Save(_saveFileName, objectData);

            Debug.Log("Data Saved");
        }

        public void Load(string _loadFileName)
        {
            List<ObjectData> objectDataList = dataPersistence.Load(_loadFileName);

            if (objectDataList == null)
            {
                Debug.LogWarning("Save file not found or failed to load.");
                return;
            }

            foreach (ObjectData data in objectDataList)
            {
                IObjectDataHandler matchingHandler = objectDataHandlerList.FirstOrDefault(handler =>
                    handler.GetObjectType() == data.objectType && handler.GetObjectName() == data.objectName);

                if (matchingHandler != null)
                {
                    matchingHandler.ApplyObjectData(data);
                }
                else
                {
                    Debug.LogWarning($"No matching object found for type: {data.objectType} and name: {data.objectName}");
                }
            }

            Debug.Log("Data Loaded");
        }

        public void RegisterObjectDataHandler(IObjectDataHandler objectDataHandler)
        {
            if (!objectDataHandlerList.Contains(objectDataHandler))
            {
                objectDataHandlerList.Add(objectDataHandler);
            }
        }

        public void DeregisterObjectDataHandler(IObjectDataHandler objectDataHandler)
        {
            if (objectDataHandlerList.Contains(objectDataHandler))
            {
                objectDataHandlerList.Remove(objectDataHandler);
            }
        }

        private void Awake()
        {
            dataPersistence = new JsonFilePersistence<ObjectData>();
            objectDataHandlerList = new List<IObjectDataHandler>();

            IObjectDataExtractor[] objectDataHandlers = FindObjectsOfType<MonoBehaviour>().OfType<IObjectDataExtractor>().ToArray();
            for (int i = 0; i < objectDataHandlers.Length; i++)
            {
                objectDataHandlerList.Add((IObjectDataHandler)objectDataHandlers[i]);
            }
        }
    }
}