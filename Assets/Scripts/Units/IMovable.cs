

using Mapbox.Utils;
using UnityEngine;

public interface IMovable
{
    void Move(Vector3 target);

    Vector2d GetLocation();
}