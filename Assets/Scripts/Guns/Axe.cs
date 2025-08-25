using Abstract;
using Interfaces;
using UnityEngine;

namespace Guns
{
    public class Axe : BaseGun
    {
        public override void Attack(IAttackable attackable)
        {
            Debug.Log("Axe Attack");
        }
    }
}