using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using static SoundManager;

public class Interact : MonoBehaviour
{
    public enum INTERACTTYPE
    {
        DYNAMITE,
        SANDWICH
    }

    [Header("Interaction settings")]
    public INTERACTTYPE interact = INTERACTTYPE.DYNAMITE;
    public ParticleSystem VFX;
    public Animator interactAnim;
    public PlayableDirector cutscene;

    [Header("Game over settings")]
    [TextArea(0, 4)] public string GameOverText;
    [SerializeField] private float deathTimer = 0.0f;
    [SerializeField] private GameManager gm;
    [SerializeField] private SoundManager soundManager;

    private void Start()
    {
        if (gm == null)
        {
            gm = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        }
    }
    virtual public void ActivateInteract()
    {
        switch (interact)
        {
            case INTERACTTYPE.DYNAMITE:
                soundManager.PlaySound(Sounds.Dyanmite);
                interactAnim.SetBool("Play", true);
                break;
            case INTERACTTYPE.SANDWICH:
                soundManager.PlaySound(Sounds.Sandwich);
                cutscene.Play(cutscene.playableAsset);
                interactAnim.SetBool("Play", true);
                print("see");
                break;
        }
        StartCoroutine(OverTimer());

        //Debug.Log("Object interacted");
        //Destroy(gameObject);
    }

    public void PlayVFX()
    {
        VFX.Play();
        
        //Hide the object if its dynamite
        if (interact == INTERACTTYPE.DYNAMITE)
        {
            gameObject.transform.Find("DynamiteStick").gameObject.SetActive(false);
        }
    }

    private IEnumerator OverTimer()
    {

        yield return new WaitForSeconds(deathTimer);

        gm.GameOver(GameOverText);

    }
}
