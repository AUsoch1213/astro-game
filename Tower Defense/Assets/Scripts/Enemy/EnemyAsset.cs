using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace TowerDefense
{
    [CreateAssetMenu]
    public sealed class EnemyAsset : ScriptableObject
    {
        [Header("������� ���")]
        public Color color = Color.white;
        public Vector2 spriteScale = new Vector2(4, 4);
        public RuntimeAnimatorController animations;

        [Header("������� ���������")]
        public float moveSpeed = 1;
        public int hp = 1;
        public int armor = 0;
        public Enemy.ArmorType armorType;
        public int score = 1;
        public float radius = 0.25f;
        public int damage = 1;
        public int gold = 1;
        public int mana = 1;
    }       
}
