using Misc;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Gun Data", menuName = "ScriptableObjects/Gun")]
    public class GunDataSO : ScriptableObject
    {
        public GunType GunType;
        public float AttackCooldown;
        public int AttackDamage;
    }
}