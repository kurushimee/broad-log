using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{

    public Vector2 sightDir;
    private IActivable lastActive;

    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - .5f), sightDir, 1f);

        if (hit.collider != null)
        {
            if (hit.transform.TryGetComponent<IActivable>(out IActivable activable))
            {
                if(activable != lastActive)
                {
                    lastActive = activable;
                    activable.ShowMe();
                }
                

                if (Input.GetKey(KeyCode.E))
                {
                    Debug.Log("Õ¿∆¿À!");
                    activable.Interact();
                }
            }
        }
        else
        {
            if (lastActive != null)
            {
                lastActive.DontShowMe();
                lastActive = null;
            }
        }
    }
}
