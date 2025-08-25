using System;
using Abstract;
using UnityEngine;

namespace Guns
{
    public class Knife : BaseGun
    {
        public override void Attack()
        {
            Debug.Log("Knife Attack");
        }
    }
}