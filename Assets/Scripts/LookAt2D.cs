using UnityEngine;

public static class LookAt2D 
{

    public static void Look(Transform myTransform, Vector3 targetLocation)
    {
        Vector2 direction = new Vector2(targetLocation.x - myTransform.position.x, targetLocation.y - myTransform.position.y);
        float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        myTransform.eulerAngles = new Vector3(0, 0, rotation);
    }
}