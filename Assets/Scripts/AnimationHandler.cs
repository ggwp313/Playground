using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    public Animator anim;
    int vertical;
    int horizontal;
    public bool canRotate;

    public void Initialize()
    {
        anim = GetComponent<Animator>();
        vertical = Animator.StringToHash("Vertical");
        horizontal = Animator.StringToHash("Horizontal");
    }

    public void UpdateAnimatorValues(float p_vertical,float p_horizontal)
    {
        #region Vertical

        float v = 0;

        if( p_vertical > 0 && p_vertical < 0.55f )
        {
            v = 0.5f;
        }else
        if( p_vertical > 0.55f )
        {
            v = 1f;
        }else
        if( p_vertical < 0 && p_vertical > -0.55f )
        {
            v = -0.5f;
        }else
        if( p_vertical < -0.55f )
        {
            v = -1f;
        }else
        {
            v = 0;
        }

        #endregion

        #region Horizontal

        float h = 0;

        if (p_horizontal > 0 && p_horizontal < 0.55f)
        {
            h = 0.5f;
        }
        else
        if (p_horizontal > 0.55f)
        {
            h = 1f;
        }
        else
        if (p_horizontal < 0 && p_horizontal > -0.55f)
        {
            h = -0.5f;
        }
        else
        if (p_horizontal < -0.55f)
        {
            h = -1f;
        }
        else
        {
            h = 0;
        }

        #endregion

        anim.SetFloat(vertical, v, 0.1f, Time.deltaTime);
        anim.SetFloat(horizontal, h, 0.1f, Time.deltaTime);
    }

    public void Rotate()
    {
        canRotate = true;
    }

    public void StopRotation()
    {
        canRotate = false;
    }

}
