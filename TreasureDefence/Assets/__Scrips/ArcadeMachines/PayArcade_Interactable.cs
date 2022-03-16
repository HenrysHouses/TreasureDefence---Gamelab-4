/*
 * Written by:
 * Henrik
*/

using UnityEngine;

public class PayArcade_Interactable : TD_Interactable
{
    [SerializeField] ArcadeMachine arcade;
   
        public override void VRInteractionStartTrigger() => Pay();
    	public override void InteractionStartTrigger(object target = null) => Pay();


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
