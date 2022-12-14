using Com.Donut.BattleSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.InputSystem;
using DG.Tweening;
using TMPro;
using Cinemachine;

public class Player : Character  {

    [SerializeField] private CinemachineImpulseSource impulseSource;
    [SerializeField] private GameObject powFx;
    [SerializeField] private VisualEffect smokeFollowShell;
    [SerializeField] private Tween tweenPow;
    private VisualEffect smokeShellVFX;
    public BattleSystem battleSystem;
    public Vector2 movement;
    private Rigidbody2D rb;
    public float speed;
    private float initialSpeed;
    public List<IFollowable> followers;

    private int lastFollowersSize = -1;

    public GameObject shellToTake;
    public bool hasCarapace;
    public bool isTransformed;
    public bool isMooving;
    public bool hasKey;

    public GameObject arrow;
    [SerializeField] public PlayerInput playerInput;

    private Vector3 direction;
    private Vector3 lastVelocity;

    [Header("UI Transformation GameFeel")]
    [SerializeField] private GameObject exploUI;
    public GameObject ExploUI => exploUI;
    [SerializeField] private GameObject abilityImg;
    [SerializeField] private GameObject playerImg;
    [SerializeField] private TextMeshProUGUI textInput;

    public override void Awake() {
        base.Awake();

        DontDestroyOnLoad(gameObject);
        rb = GetComponent<Rigidbody2D>();
        followers = new List<IFollowable>();

        exploUI.SetActive(false);
        playerImg.SetActive(false);
        smokeShellVFX = GetComponent<VisualEffect>();
        smokeShellVFX.enabled = false;
        if (shellToTake != null)
            shellToTake.SetActive(false);

        initialSpeed = speed;
        powFx.GetComponent<SpriteRenderer>().enabled = false;
    }


    public override void Update() {
        base.Update();

        //  if(lastFollowersSize != followers.Count)
        //    ManageFollowers(followers.Count > lastFollowersSize); // To Modify : probably doesn't work with follower remove

        lastFollowersSize = followers.Count;
    }
    
    protected override void Move() {
      //  if (MinimapController.instance.isInMap)
        //    return;
        
        if (!hasCarapace)
        {
            if (movement == Vector2.zero)
                SwitchAnimState(IDLES[0]);
            else {
                string anim_id = movement.x != 0 && movement.y == 0 ? WALKS[2] : movement.y > 0 && movement.x == 0 ? WALKS[1] : WALKS[0];
                SwitchAnimState(anim_id);
            }
        }
        else if(!isTransformed)
        {
            if (movement == Vector2.zero)
                SwitchAnimState(IDLES[0]);
            else {
                string anim_id = movement.x != 0 && movement.y == 0 ? WALKS_CARAPACE[2] : movement.y > 0 && movement.x == 0 ? WALKS_CARAPACE[1] : WALKS_CARAPACE[0];
                SwitchAnimState(anim_id);
            }
        }

        rb.velocity = movement * speed;

        if (isTransformed) {
            rb.velocity = direction * speed;
            //play vfx
            if (smokeFollowShell != null)
                smokeFollowShell.Play();
        }
        else
        {
            if(smokeFollowShell != null)
                smokeFollowShell.Stop();
        }

        spriteRenderer.flipX = movement.x < 0 && movement.y == 0;
        lastVelocity = rb.velocity;
    }

    private void ManageFollowers(bool add) {
        int followerIndex = add ? followers.Count - 1 : 0; // To Modify with remove
        
        followers[followerIndex].target = this;
    }

    public void OnMove(InputAction.CallbackContext e) {
        if (e.performed)
            movement = e.ReadValue<Vector2>();
        if(e.canceled)
            movement = Vector2.zero;
    }

    public void OnTransformation(InputAction.CallbackContext e) {
        if (hasCarapace)
        {
            if (e.performed) {
                AudioManager.Instance.ShellSpin();
                textInput.color = new Color(0.65f, 0.4f, 0, 1);
                SwitchAnimState("WC_Run");
                isTransformed = !isTransformed;
                //UI Gamefeel
                if(isTransformed == true)
                {
                    smokeShellVFX.enabled = true;
                    direction = Vector3.zero;
                    playerImg.SetActive(true);
                    abilityImg.SetActive(false);
                    //abilityImg.transform.DOMoveY(abilityImg.GetComponent<RectTransform>().rect.position.y - 15f, 0.5f).SetEase(Ease.InElastic).SetEase(HideImg);
                    //playerImg.transform.DOMoveY(playerImg.GetComponent<RectTransform>().rect.position.y + 15f, 0.5f).SetEase(Ease.InElastic);
                }
                else
                {
                    smokeShellVFX.enabled = false;
                    AudioManager.Instance.StopShellSpin();
                    abilityImg.SetActive(true);
                    playerImg.SetActive(false);
                    //playerImg.transform.DOMoveY(playerImg.GetComponent<RectTransform>().rect.position.y - 15f, 0.5f).SetEase(Ease.InElastic).SetEase(HideImg);
                    //abilityImg.transform.DOMoveY(abilityImg.GetComponent<RectTransform>().rect.position.y + 15f, 0.5f).SetEase(Ease.InElastic);
                }
            }
            if(e.canceled)
                textInput.color = new Color(1, 0.55f, 0.04f, 1);
        }
    }

    private float HideImg(float time, float duration, float overshootOrAmplitude, float period)
    {
        if (time >= 0.5f) {
            if (isTransformed)
            {
                abilityImg.SetActive(false);
            }
            else
            {
                playerImg.SetActive(false);
            }
        }
        return 0; 
    }

    public void OnThrow(InputAction.CallbackContext e) {
        if (e.performed && hasCarapace) {
            if (GameManager.Instance.tutoText != null)
            {
                GameManager.Instance.tutoText.transform.parent.gameObject.SetActive(false);
                GameManager.Instance.tutoText = null;
            }
            direction = (arrow.transform.position - transform.position).normalized;
        }
    }

    public void OnSetBattlePhaseDebug(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            battleSystem.SetState(new Init(battleSystem));
        }
    }


    public void OnChangedMap(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            Debug.Log("The Default map has been changed !!");
        }
    }

    private void OnCollisionEnter2D(Collision2D col) 
    {
        if (col.gameObject.CompareTag("Destructible") && isTransformed)
        {
            if(col.gameObject.TryGetComponent(out VisualEffect vfx))
            {
                AudioManager.Instance.ShellHitBarrel();
                StartCoroutine(VFX(vfx));
            }
        }else if (col.gameObject.CompareTag("Enemy"))
        {
            direction = Vector3.zero;
            Destroy(col.gameObject);
            battleSystem.SetState(new Init(battleSystem));
        }
        else if(isTransformed)
        {
            AudioManager.Instance.ShellHitRock();
            Vector3 reflectVec = Vector3.Reflect(lastVelocity.normalized,col.contacts[0].normal);
            direction = reflectVec;
        }
        if (isTransformed && !col.gameObject.CompareTag("Destructible") && !col.gameObject.CompareTag("Enemy"))
        {
            impulseSource.GenerateImpulse();
            powFx.transform.position = col.GetContact(0).point;
            StartCoroutine(PowEffect());
        }
    }

    private IEnumerator PowEffect()
    {
        if(tweenPow != null && tweenPow.IsPlaying())
            tweenPow.Kill();

        powFx.GetComponent<SpriteRenderer>().enabled = true;
        powFx.GetComponent<SpriteRenderer>().color = Color.white;
        powFx.transform.localScale = Vector3.zero;
        tweenPow = powFx.transform.DOScale(0.2f, 0.2f);
        yield return new WaitForSeconds(0.2f);
        tweenPow = powFx.GetComponent<SpriteRenderer>().DOFade(0, 0.5f);

        //powFx.GetComponent<SpriteRenderer>().enabled = false;

        /*if (powFx.transform.GetChild(0).GetComponent<VisualEffect>().aliveParticleCount > 0)
        {
            newVFX = Instantiate(powFx.transform.GetChild(0).gameObject);
            newVFX.transform.parent = powFx.transform;
            newVFX.GetComponent<VisualEffect>().Play();
        }
        else
        {
            powFx.transform.GetChild(0).GetComponent<VisualEffect>().Play();
        }*/

        //yield return new WaitForSeconds(powFx.transform.GetChild(0).GetComponent<VisualEffect>().GetFloat("Lifetime"));

        /*if (newVFX != null && newVFX.GetComponent<VisualEffect>().aliveParticleCount > 0)
        {
            Destroy(newVFX);    
        }
        else if(powFx.transform.GetChild(0).GetComponent<VisualEffect>().aliveParticleCount > 0)
        {
            powFx.transform.GetChild(0).GetComponent<VisualEffect>().Stop();
        }*/
    }

    private IEnumerator VFX(VisualEffect vfxToPlay)
    {
        vfxToPlay.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        vfxToPlay.gameObject.GetComponent<Collider2D>().enabled = false;
        vfxToPlay.Play();
        yield return new WaitForSeconds(vfxToPlay.GetFloat("Lifetime"));
        Destroy(vfxToPlay.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.TryGetComponent<ShakeBehaviour>(out ShakeBehaviour shakeBehaviour)) {
            if (shakeBehaviour.shakeType == ShakeBehaviour.ShakeObjectType.TREE && isTransformed) {
                shakeBehaviour.canShake = true;
                shakeBehaviour.Shake(0.5f);
            }
        }

        if (col.gameObject.CompareTag("ShellToTake") && !hasCarapace)
        {
            GameManager.Instance.tutoText.text = GameManager.Instance.shellInput;
            shellToTake.SetActive(true);
            exploUI.SetActive(true);
            hasCarapace = true;
            Destroy(col.gameObject);
        }

    }

    private void OnTriggerStay2D(Collider2D col) {
        if (col.gameObject.TryGetComponent<ShakeBehaviour>(out ShakeBehaviour shakeBehaviour)) {
            if (shakeBehaviour.shakeType == ShakeBehaviour.ShakeObjectType.BUSH) {
                shakeBehaviour.canShake = true;
                shakeBehaviour.Shake(0.5f);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.TryGetComponent<ShakeBehaviour>(out ShakeBehaviour shakeBehaviour)) {
            shakeBehaviour.canShake = false;
        }

    }
}
