using UnityEngine;

public class PlaySoundEnter : StateMachineBehaviour
{
    
    [SerializeField]private AudioClip audioClip;
    [SerializeField] float Volume;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        GameController.MainAudioSource.PlayOneShot(audioClip, Volume);
    }
}
