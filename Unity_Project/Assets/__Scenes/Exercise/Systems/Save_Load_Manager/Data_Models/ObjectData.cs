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
}