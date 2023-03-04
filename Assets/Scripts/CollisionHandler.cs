using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Careful!");
                break;
            case "Finish":
                Debug.Log("Level complete!");
                break;
            case "Fuel":
                Debug.Log("Refueled!");
                break;
            default:
                Debug.Log("you crashed!");
                break;
        }
    }
}