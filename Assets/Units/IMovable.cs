

using Mapbox.Utils;
using UnityEngine;

public interface IUnit
{
    void Move(Vector3 target);

    Vector2d GetLocation();
}