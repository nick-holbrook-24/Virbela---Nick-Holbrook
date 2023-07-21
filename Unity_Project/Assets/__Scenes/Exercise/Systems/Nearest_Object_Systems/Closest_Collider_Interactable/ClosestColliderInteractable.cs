using UnityEngine;

namespace ExerciseOne
{
    public class ClosestColliderInteractable : MonoBehaviour, INearestObjectSystem
    {
        [SerializeField] private ExerciseManagerData exerciseManagerData = null;

        public IInteractableObject FindNearestInteractable(Vector3 _position)
        {
            Collider[] colliders = Physics.OverlapSphere(_position, exerciseManagerData.searchRadius);
            float minDistance = float.MaxValue;
            IInteractableObject closestInteractable = null;

            foreach (Collider col in colliders)
            {
                IInteractableObject interactable = col.GetComponent<IInteractableObject>();
                if (interactable == null)
                {
                    continue;
                }

                float distance = Vector3.Distance(_position, col.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestInteractable = interactable;
                }
            }

            return closestInteractable;
        }
    }
}