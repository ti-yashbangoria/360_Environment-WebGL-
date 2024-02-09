using UnityEngine;

public class DistanceTracker : MonoBehaviour
{
    #region Variables

    [Header("{Enums}")]
    public PositionState positionState;
    [Space]
    public ActiveAxisState activeAxisState;

    [Header("{Scripts}")]
    [SerializeField] Inputs inputs;

    [Header("{Componens}")]
    [SerializeField] Transform targetTrans;

    [Header("{Values}")]
    [SerializeField] float differenceValue = 0.2f;

    float distanceToTarget;
    float currentPos;
    float targetPos;
    float dotValue;

    #endregion


    #region Main Methods

    void Update()
    {
        ChangeActiveAxisState();
        GetTargetDis();
        StopPCamVelocity();
    }

    #endregion


    #region Helper Methods

    private void ChangePositionState(PositionState state)
    {
        positionState = state;
        switch (state)
        {
            case PositionState.NotNearby:
                break;

            case PositionState.Nearby:
                break;
        }
    }

    private void ChangeActiveAxisState()
    {
        switch (activeAxisState)
        {
            case ActiveAxisState.X_Axis:

                currentPos = transform.position.x;
                targetPos = targetTrans.position.x;
                break;

            case ActiveAxisState.Y_Axis:

                currentPos = transform.position.y;
                targetPos = targetTrans.position.y;
                break;

            case ActiveAxisState.Z_Axis:

                currentPos = transform.position.z;
                targetPos = targetTrans.position.z;
                break;
        }
    }

    private void GetTargetDis()
    {
        distanceToTarget = currentPos - targetPos;

        if(Mathf.Abs(distanceToTarget) <= differenceValue)
        {
            ChangePositionState(PositionState.Nearby);
        }
        else
        {
            ChangePositionState(PositionState.NotNearby);
        }
    }

    private void StopPCamVelocity()
    {
        if(positionState == PositionState.Nearby)
        {
            dotValue = Vector3.Dot(transform.position.normalized, inputs.directionVector.normalized);

            if (dotValue > 0)
            {
                inputs.directionVector = Vector3.zero;
                inputs.canMove = false;
                return;
            }

            if(dotValue <= 0)
            {
                inputs.canMove = true;
            }
        }
    }

    #endregion
}

public enum PositionState
{
    NotNearby,
    Nearby
}

public enum ActiveAxisState
{
    X_Axis,
    Y_Axis,
    Z_Axis
}
