using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ExerciseOne
{
    [System.Serializable]
    public class SerializableVector3
    {
        public float x;
        public float y;
        public float z;

        public SerializableVector3(Vector3 _vector)
        {
            x = _vector.x;
            y = _vector.y;
            z = _vector.z;
        }

        public Vector3 ToVector3()
        {
            return new Vector3(x, y, z);
        }
    }

    [System.Serializable]
    public class SerializableColor
    {
        public float r;
        public float g;
        public float b;
        public float a;

        public SerializableColor(Color _color)
        {
            r = _color.r;
            g = _color.g;
            b = _color.b;
            a = _color.a;
        }

        public Color ToColor()
        {
            return new Color(r, g, b, a);
        }
    }

    [System.Serializable]
    public class ObjectData
    {
        public string objectType;
        public string objectName;
        public SerializableVector3 position;
        public SerializableColor baseColor;
        public SerializableColor highlightColor;
    }

    public class SaveLoadManager : MonoBehaviour
    {
        public IDataPersistence<ObjectData> dataPersistence;
        public List<IObjectDataHandler> objectDataHandlerList;

        private void Awake()
        {
            dataPersistence = new JsonFilePersistence<ObjectData>();
            objectDataHandlerList = new List<IObjectDataHandler>();

            IObjectDataExtractor[] objectDataHandlers = FindObjectsOfType<MonoBehaviour>().OfType<IObjectDataExtractor>().ToArray();
            for(int i = 0; i < objectDataHandlers.Length; i++)
            {
                objectDataHandlerList.Add((IObjectDataHandler)objectDataHandlers[i]);
            }
        }

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
        }

        public void Load(string _saveFileName)
        {
            List<ObjectData> objectDataList = dataPersistence.Load(_saveFileName);

            if (objectDataList == null)
            {
                Debug.LogError("Save file not found or failed to load.");
                return;
            }

            for(int i = 0; i < objectDataList.Count; i++) 
            {
                if (objectDataList[i] == null)
                {
                    return;
                }

                if (i >= objectDataHandlerList.Count)
                {
                    // Need to add additional ObjectDataHandlers before ApplyingObjectData 
                    // when the number of saved objects is greater than the number of initially
                    // created objects.
                    return;
                }

                objectDataHandlerList[i].ApplyObjectData(objectDataList[i]);
            }
        }
    }
}