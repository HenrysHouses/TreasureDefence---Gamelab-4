/*
 * Written by:
 * Henrik
*/

using UnityEngine;

public class PayArcade_Interactable : MonoBehaviour
{
    [SerializeField] ArcadeMachine arcade;
   
        void OnCollisionEnter(Collision collision)
        {
            if(collision.collider.GetComponent<VFX_BulletController>())
            {
                Pay();
            }
        }

        public void Pay()
        {
            arcade.StartGame();
        }
}
