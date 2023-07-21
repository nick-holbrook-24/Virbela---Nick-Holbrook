using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ExerciseOne
{
    [System.Serializable]
    public class DataListWrapper<T>
    {
        public List<T> dataList;
    }

    public class JsonFilePersistence<T> : IDataPersistence<T> where T : class
    {
        public void Save(string _fileName, List<T> _data)
        {
            DataListWrapper<T> dataListWrapper = new DataListWrapper<T> { dataList = _data };
            string json = JsonUtility.ToJson(dataListWrapper);
            Debug.Log(json);
            File.WriteAllText(Path.Combine(Application.persistentDataPath, _fileName), json);
        }

        List<T> IDataPersistence<T>.Load(string _fileName)
        {
            string filePath = Path.Combine(Application.persistentDataPath, _fileName);

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                DataListWrapper<T> dataListWrapper = JsonUtility.FromJson<DataListWrapper<T>>(json);
                return dataListWrapper.dataList;
            }
            else
            {
                Debug.LogError("Save file not found.");
                return default;
            }
        }
    }
}