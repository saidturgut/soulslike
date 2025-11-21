using System.Collections;
using System.Linq;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController s { get; private set; }

    [Header("- Look -")]

    [SerializeField] private float followSpeed = 0.1f;
    [SerializeField] float lookSpeed;
    [SerializeField] float lookSpeedBl;

    [SerializeField] Vector2 tpsPivot;

    [SerializeField] bool enableCamCol;

    [Header("- CameraCollision -")]

    [SerializeField] float camColOffset = 0.2f;
    [SerializeField] float minColOffset = 0.2f;
    [SerializeField] float camSphereRadius = 0.2f;

    public float speed;

    #region PrivateField

    private float mouseX;
    private float mouseY;

    private float lookAngle;
    private float pivotAngle;

    [HideInInspector] public Transform lockTarget;
    private Transform targetT;
    private Transform pivot;
    private Transform camTransform;
    private float targetPos;
    [SerializeField] private float defPos;

    #endregion

    private void Awake() { s = this; }

    private void Start()
    {
        pivot = transform.GetChild(0);
        pivotAngle = pivot.transform.eulerAngles.x;
        camTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;

        targetT = Player.s.transform;

        Cursor.lockState = CursorLockMode.Locked;

        Cursor.visible = false;

        transform.position = targetT.position;

        transform.rotation = targetT.rotation;
        
        transform.SetParent(null);
    }

    private void Update()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        HandleCollisions();

        if (!MainPanel.s.mainPanelDisabled) { return; }

        HandleRotation();
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, targetT.position, Time.deltaTime / followSpeed);
    }

    public void HandleRotation()
    {
        lookAngle += (mouseX * lookSpeed) / Time.deltaTime;

        pivotAngle += (mouseY * lookSpeed) / Time.deltaTime;
        pivotAngle = Mathf.Clamp(pivotAngle, tpsPivot.x, tpsPivot.y);

        Vector3 euler = Vector3.zero;
        euler.y = lookAngle;
        Quaternion targetRotation = Quaternion.Euler(euler);

        transform.rotation = targetRotation;

        euler = Vector3.zero;
        euler.x = pivotAngle;
        targetRotation = Quaternion.Euler(euler);

        pivot.localRotation = targetRotation;
    }

    private void HandleCollisions()
    {
        if (!enableCamCol) { return; }

        targetPos = defPos;

        RaycastHit hit;
        Vector3 direction = camTransform.position - pivot.position;
        direction.Normalize();

        if (Physics.SphereCast(pivot.position, camSphereRadius, direction, out hit, Mathf.Abs(targetPos), GameManager.s.groundLayer))
        {
            float dis = Vector3.Distance(pivot.position, hit.point);
            targetPos = -(dis - camColOffset);
        }

        if (Mathf.Abs(targetPos) < minColOffset)
        {
            targetPos = minColOffset;
        }

        Vector3 camTPos = Vector3.zero;

        camTPos.z = targetPos;

        camTransform.localPosition = camTPos;

        /*targetPos = defPos;

        RaycastHit hit;
        Vector3 direction = camTransform.position - pivot.position;
        direction.Normalize();

        if (Physics.SphereCast(pivot.position, camSphereRadius, direction, out hit, Mathf.Abs(targetPos), GameManager.s.groundLayer))
        {
            float dis = Vector3.Distance(pivot.position, hit.point);
            targetPos = -(dis - camColOffset);

            pivotAngle += lookSpeedBl / Time.deltaTime;
        }

        if (Mathf.Abs(targetPos) < minColOffset)
        {
            targetPos = minColOffset;
        }

        //camTPos.z = targetPos;

        //camTransform.localPosition = camTPos;*/
    }

    public void ResetCamera()
    {
        transform.position = targetT.position;

        HandleRotation();
    }
}
