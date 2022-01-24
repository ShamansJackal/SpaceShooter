using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public Animator animator;

    public void ShieldDown()
    {
        animator.SetTrigger("ShieldDown");
    }
}
