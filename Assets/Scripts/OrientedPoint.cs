using UnityEngine;

public struct OrientedPoint
{
    public Vector3 Position;
    public Quaternion Rotation;

    public OrientedPoint(Vector3 position, Quaternion rotation)
    {
        Position = position;
        Rotation = rotation;
    }

    public OrientedPoint(Vector3 position, Vector3 forward)
    {
        Position = position;
        Rotation = Quaternion.LookRotation(forward);
    }

    public Vector3 LocalToWorldPosition(Vector3 localSpacePoint)
    {
        // Rotation * localSpacePoint => Local To World conversation for a rotation.
        return Position + Rotation * localSpacePoint;
    }
    
    public Vector3 LocalToWorldVector(Vector3 localSpacePoint)
    {
        return Rotation * localSpacePoint;
    }
}