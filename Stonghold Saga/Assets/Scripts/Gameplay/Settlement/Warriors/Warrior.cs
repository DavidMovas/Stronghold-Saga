using UnityEngine;

namespace Gameplay.Settlement.Warriors
{
    public class Warrior
    {
        public WarriorType Type { get; private set; }

        public int Power
        {
            get
            {
                return _power;
            }
            set
            {
                _power = value;
            }
        }

        public int Armor
        {
            get
            {
                return _armor;
            }
            set
            {
                _armor = value;
            }
        }

        private int _power;
        private int _armor;

        public Warrior(WarriorType type, int power, int armor)
        {
            Type = type;
            _power = power;
            _armor = armor;
        }
    }
}