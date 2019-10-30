using UnityEngine;

namespace Packing_3D.Interfaces
{
    public abstract class Builder<T> : MonoBehaviour
    {
        public abstract GameObject Build(T product);
    }
}
