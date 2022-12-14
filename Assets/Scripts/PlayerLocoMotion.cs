using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocoMotion : MonoBehaviour
{

    Transform cameraObject;
    InputHandler inputHandler;
    Vector3 moveDirection;

    [HideInInspector]
    public Transform myTransform;
    [HideInInspector]
    public AnimationHandler animHandler;
    public new Rigidbody rigidbody;
    public GameObject normalCamera;

    [Header("STATS")]
    [SerializeField]
    private float moveSpeed = 5;
    [SerializeField]
    private float rotationSpeed = 10;


    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        animHandler = GetComponentInChildren<AnimationHandler>();
        inputHandler = GetComponent<InputHandler>();
        cameraObject = Camera.main.transform;
        myTransform = transform;
        animHandler.Initialize();
    }

    private void Update()
    {
        float delta = Time.deltaTime;

        inputHandler.TickInput(delta);

        moveDirection = cameraObject.forward * inputHandler.vertical;
        moveDirection += cameraObject.right * inputHandler.horizontal;
        moveDirection.Normalize();

        float speed = moveSpeed;
        moveDirection *= speed;

        Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
        rigidbody.velocity = projectedVelocity;

        animHandler.UpdateAnimatorValues(inputHandler.moveAmount,0.5f);

        if(animHandler.canRotate)
        {
            HandleRotation(delta);
        }


    }

    #region Movement

    Vector3 normalVector;
    Vector3 targetPosition;

    private void HandleRotation(float delta)
    {
        Vector3 targetDir = Vector3.zero;
        float moveOverride = inputHandler.moveAmount;

        targetDir = cameraObject.forward * inputHandler.vertical;
        targetDir += cameraObject.right * inputHandler.horizontal;

        targetDir.Normalize();
        targetDir.y = 0;

        if(targetDir == Vector3.zero)
        {
            targetDir = myTransform.forward;
        }

        float rs = rotationSpeed;

        Quaternion tr = Quaternion.LookRotation(targetDir);
        Quaternion targetRotation = Quaternion.Slerp(myTransform.rotation,tr,rs * delta);

        myTransform.rotation = targetRotation;
    }

    #endregion
}
