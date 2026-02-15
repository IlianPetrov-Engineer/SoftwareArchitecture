using System;
using UnityEngine;

namespace SA_Enemy
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "Scriptable Objects/Enemy")]
    public class EnemyData : ScriptableObject
    {
        [Header("Core properties")]
        public int maxHealth;
        public int attackDamage;
        public int passiveDamage;
        public float speed;
        public int xp;

        public enum EnemyBehaviour
        {
            Melee,
            Range,
            Aura
        }

        public EnemyBehaviour enemyBehaviour;

        public Enemy CreateEnemy()
        {
            return new Enemy(maxHealth, attackDamage, passiveDamage, speed, xp);
        }
    }

    [Serializable]
    public class Enemy
    {
        [Header("Core properties")]
        [SerializeField]
        private int maxHealth;
        public int MaxHealth => maxHealth;

        public int CurrentHealth;

        [SerializeField]
        private int attackDamage;
        public int AttackDamage => attackDamage;

        [SerializeField]
        private int passiveDamage;
        public int PassiveDamage => passiveDamage;

        [SerializeField]
        private float speed;
        public float Speed => speed;

        [SerializeField]
        private int xp;
        public int XP => xp;

        public Enemy(int pmaxHealth, int pattackDamage, int ppassiveDamage, float pspeed, int pxp)
        {
            maxHealth = pmaxHealth;
            CurrentHealth = pmaxHealth;
            attackDamage = pattackDamage;
            passiveDamage = ppassiveDamage;
            speed = pspeed;
            xp = pxp;
        }
    }
}
