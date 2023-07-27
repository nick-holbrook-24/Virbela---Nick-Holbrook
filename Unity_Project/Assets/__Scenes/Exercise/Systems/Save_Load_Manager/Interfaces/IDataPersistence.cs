using System.Collections.Generic;

namespace ExerciseOne
{
    public interface IDataPersistence<T>
    {
        void Save(string _fileName, List<T> _data);
        List<T> Load(string _fileName);
    }
}