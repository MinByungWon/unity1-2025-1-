using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Assertions;
using Mirror;
using Random = UnityEngine.Random;
public class PlayerController : NetworkBehaviour
{
    PlayerInputAction playerInput;
    SceneScript sceneScript;

    public TextMesh playerNameText;
    public GameObject floatingInfo;

    Vector3 lastSyncPosition;
    float syncThreshold = 0.01f; // 동기화 임계값

    Weapon activeWeapon;
    float weaponCooldownTime;

    [SyncVar(hook = nameof(OnNameChange))]
    public string playerName;

    [SyncVar(hook = nameof(OnColorChanged))]
    public Color playerColor = Color.white;

    [Command]
    public void CmdSendPlayerMessage()
    {
        if (sceneScript)
            sceneScript.statusText = $"{playerName} say hello {Random.Range(10, 99)}";
    }

    Material playerMaterialClone;

    [SerializeField]
    Rigidbody rb;

    [SyncVar] float movementX;
    [SyncVar] float movementY;

    const float MOVE_FORCE = 100f;
    const float WALKING_SPEED = 100f;

    void OnNameChange(string _old, string _new)
    {
        playerNameText.text = playerName;
    }

    #region Unity Callback
    void Awake()
    {
        foreach (var item in weaponArray)
        {
            if (item != null)
                item.SetActive(false);
        }

        sceneScript = GameObject.Find("SceneRefer").GetComponent<SceneRefer>().sceneScript;
        Debug.Log(sceneScript);

        Assert.IsNotNull(sceneScript);

        activeWeapon = weaponArray[selectedWeaponLocal].GetComponent<Weapon>();

        if (selectedWeaponLocal < weaponArray.Length && weaponArray[selectedWeaponLocal] != null)
        {
            sceneScript.UIAmmo(activeWeapon.weaponAmmo);
        }
    }

    public void UpdateFloatingInfoPosition(Vector3 pos)
    {
        floatingInfo.transform.position = pos + Vector3.up * 1.5f;
        floatingInfo.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!isLocalPlayer) return;
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("카리나 Rocket Puncher");
        }
    }

    void Update()
    {
        Vector3 pos = this.transform.position;
        if (!isLocalPlayer)
        {
            if (!isServer)
            {
                if (Vector3.Distance(pos, lastSyncPosition) > syncThreshold)
                {
                    lastSyncPosition = pos;
                }
                else
                {
                    transform.position = lastSyncPosition;
                }
            }
            UpdateFloatingInfoPosition(pos);
            UpdateWeaponPosition();
            return;
        }

        Vector3 currentVelocity = rb.linearVelocity;
        if (currentVelocity.magnitude > WALKING_SPEED)
        {
            currentVelocity = currentVelocity.normalized * WALKING_SPEED;
            rb.linearVelocity = currentVelocity;
        }

        Vector3 movement = new Vector3(movementX, 0f, movementY);
        if (movement != Vector3.zero)
        {
            // 물리 기반 이동을 velocity로 직접 제어
            Vector3 targetVelocity = movement * WALKING_SPEED;
            rb.linearVelocity = new Vector3(targetVelocity.x, rb.linearVelocity.y, targetVelocity.z);
        }
        else
        {
            // 입력 없을 때 수평속도만 즉시 멈춤
            rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
        }

        UpdateFloatingInfoPosition(pos);
        UpdateWeaponPosition();

        if (Input.GetButtonDown("Fire2"))
        {
            var weapon = selectedWeaponLocal + 1;

            if (weapon > weaponArray.Length)
                weapon = 1;

            selectedWeaponLocal = weapon;

            CmdChangeActiveWeapon(selectedWeaponLocal);

        }
        if (Input.GetButtonDown("Fire1"))
        {
            if (activeWeapon && activeWeapon.gameObject.activeSelf && Time.time > weaponCooldownTime && activeWeapon.weaponAmmo > 0)
            {
                weaponCooldownTime = Time.time + activeWeapon.weaponCooldown;
                activeWeapon.weaponAmmo -= 1;
                sceneScript.UIAmmo(activeWeapon.weaponAmmo);
                CmdShootRay();
            }
        }
    }
    #endregion

    #region other
    void OnColorChanged(Color oldColer, Color newColor)
    {
        Renderer currRender = this.GetComponent<Renderer>();
        playerMaterialClone = new Material(currRender.material);
        playerMaterialClone.color = newColor;
        currRender.material = playerMaterialClone;
    }
    public override void OnStartLocalPlayer()
    {
        Debug.Log("Start Local Player...");

        sceneScript.playerScript = this;

        Camera mainCam = Camera.main;

        var camController = mainCam.GetComponent<CameraController>();
        camController.SetupPlayer(this.gameObject);

        playerInput = new PlayerInputAction();
        playerInput.Player.Enable();
        playerInput.Player.Move.performed += ctx => this.OnMove(ctx);
        playerInput.Player.Move.canceled += ctx => this.OnMove(ctx);

        string name = $"Player {Random.Range(100, 1000)}";

        Color color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        CmdSetupPlayer(name, color);

        floatingInfo.transform.parent = null;
        ClearWeaponRootParent();
    }
    public override void OnStartClient()
    {
        if (!isLocalPlayer)
        {
            floatingInfo.transform.parent = null;
            ClearWeaponRootParent();
        }
    }

    void ClearWeaponRootParent()
    {
        WeaponRoot.transform.parent = null;

    }
    [Command]
    public void CmdSetupPlayer(string name, Color color)
    {
        playerName = name;
        playerColor = color;
        sceneScript.statusText = $"{playerName} joined...";
    }

    [Command]
    void CmdShootRay()
    {
        RpcFireWeapon();
    }

    [ClientRpc]
    void RpcFireWeapon()
    {
        activeWeapon.MakeFireSound();
        GameObject bullet = Instantiate(
            activeWeapon.weaponBullet,
            activeWeapon.weaponFirePos.position,
            activeWeapon.weaponFirePos.rotation);
        bullet.GetComponent<Rigidbody>().linearVelocity =
            bullet.transform.forward * activeWeapon.weaponSpeed;
        Destroy(bullet, activeWeapon.weaponLife);
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        if (!isLocalPlayer) return;

        if (ctx.performed)
        {
            Vector2 movementVector = ctx.ReadValue<Vector2>();
            CmdUpdateMovement(movementVector.x, movementVector.y);
            Debug.Log($"{movementX}, {movementY}");
        }

        if (ctx.canceled)
        {
            CmdUpdateMovement(0f, 0f);
        }
    }

    [Command]
    void CmdUpdateMovement(float x, float y)
    {
        movementX = x;
        movementY = y;
    }
    #endregion

    #region Weapon

    int selectedWeaponLocal = 1;

    public GameObject WeaponRoot;
    public GameObject[] weaponArray;

    [SyncVar(hook = nameof(OnWeaponChanged))]
    public int activeWeaponSynced = 1;

    void OnWeaponChanged(int _Old, int _New)
    {
        Debug.Log("tst");

        if (0 < _Old && _Old < weaponArray.Length && weaponArray[_Old] != null)
            weaponArray[_Old].SetActive(false);

        if (0 < _New && _New < weaponArray.Length && weaponArray[_New] != null)
        {
            weaponArray[_New].SetActive(true);
            activeWeapon = weaponArray[activeWeaponSynced].GetComponent<Weapon>();
            if (isLocalPlayer)
            {
                sceneScript.UIAmmo(activeWeapon.weaponAmmo);
            }
        }
    }

    [Command]
    public void CmdChangeActiveWeapon(int newIndex)
    {
        activeWeaponSynced = newIndex;
    }

    #endregion

    void UpdateWeaponPosition()
    {
        WeaponRoot.transform.position = this.transform.position;
        var rot = this.transform.rotation.eulerAngles * Mathf.Rad2Deg;

        float targetAngle = WeaponRoot.transform.rotation.eulerAngles.y;

        if (movementX != 0 || movementY != 0)
        {
            targetAngle = Mathf.Atan2(movementX, movementY) * Mathf.Rad2Deg;
        }

        float currnetAngle = WeaponRoot.transform.rotation.eulerAngles.y;
        float smoothAngle = Mathf.LerpAngle(currnetAngle, targetAngle, Time.deltaTime * 15f);
        WeaponRoot.transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);
    }
}