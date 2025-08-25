using System;
using Abstract;
using Interfaces;
using UnityEngine;

namespace Guns
{
    public class Knife : BaseGun
    {
        public override void Attack(IAttackable attackable)
        {
            Debug.Log("Knife Attack");
        }
    }
}