using System.Collections.Generic;
using UnityEngine;

using MIST.EQ.Items;

namespace MIST.Managers
{
    public class ItemManager : MonoBehaviour
    {
        public static ItemManager Instance { get; private set; }

        [SerializeField] private Weapon defaultWeapon;
        [SerializeField] private List<Item> allItems;
        [SerializeField] private List<Weapon> weaponItems;
        [SerializeField] private List<Ammo> ammoItems;

        private void Awake()
        {
            Instance = this;
        }

        public List<Item> GetAllItems() => allItems;
        public Item GetRandomItem()
        {
            return allItems[Random.Range(0, allItems.Count)];
        }

        public Weapon GetDefaultWeapon() => defaultWeapon;

        public Weapon GetWeapon(int index)
        {
            if (index < 0 || index >= weaponItems.Count)
                return null;

            Weapon weapon = weaponItems[index];
            return weapon;
        }

        public Weapon GetWeapon(string weaponName)
        {
            foreach (Weapon weapon in weaponItems)
                if (weapon.Name == weaponName)
                    return weapon;

            return null;
        }

        public List<Weapon> GetAllWeapons() => weaponItems;

        public List<MeleeWeapon> GetAllMeleeWeapons()
        {
            List<MeleeWeapon> meleeWeapons = new List<MeleeWeapon>();

            foreach (var weapon in weaponItems)
                if (weapon.GetType() == typeof(MeleeWeapon))
                    meleeWeapons.Add((MeleeWeapon)weapon);

            return meleeWeapons;
        }

        public List<RangeWeapon> GetAllRangeWeapons()
        {
            List<RangeWeapon> rangeWeapons = new List<RangeWeapon>();

            foreach (var weapon in weaponItems)
                if (weapon.GetType() == typeof(RangeWeapon))
                    rangeWeapons.Add((RangeWeapon)weapon);

            return rangeWeapons;
        }

        public Weapon GetRandomWeapon() => weaponItems[Random.Range(0, weaponItems.Count)];

        public Ammo GetRandomAmmo() => ammoItems[Random.Range(0, ammoItems.Count)];
    }
}