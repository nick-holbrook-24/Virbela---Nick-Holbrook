using UnityEngine;

namespace ExerciseOne
{
    public interface INearestObjectSystem
    {
        public IInteractableObject FindNearestInteractable(Vector3 _position);
    }
}