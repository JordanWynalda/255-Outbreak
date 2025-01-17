﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wynalda
{
    public class PlayerShooting : MonoBehaviour
    {
        
        public enum WeaponType // switches between weapons that the player can use
        {
            PeaShooter,
            AutoRifle,
            TripleShot
        }

        public GameObject tripleBullet;
        public GameObject peaBullet;
        public GameObject autoBullet;

        public Transform projectileSpawnPoint;
        public WeaponType currentWeapon = WeaponType.PeaShooter;

        float cooldownUntilNextBullet = 0;
        /// <summary>
        /// Whatever our "CycleWeapon" axis value was one frame ago.
        /// </summary>
        int previousCycleDirection = 0;

        void Start()
        {

        }
        void Update()
        {
            CycleWeapons();

            if (Input.GetButton("Fire1")) Shoot();
            if (cooldownUntilNextBullet > 0) cooldownUntilNextBullet -= Time.deltaTime;
        }

        private void CycleWeapons() // switches between player weapons
        {
            float cycleInput = Input.GetAxisRaw("CycleWeapons");

            int cycleDirection = 0;
            if (cycleInput < 0) cycleDirection = -1;
            if (cycleInput > 0) cycleDirection = 1;

            if (previousCycleDirection == 0) // only change weapons if we WEREN'T trying to change weapons last frame.
            {
                int index = (int)currentWeapon + cycleDirection;

                int max = System.Enum.GetNames(typeof(WeaponType)).Length - 1;

                if (index < 0) index = max;
                if (index > max) index = 0;

                currentWeapon = (WeaponType)index;
            }

            previousCycleDirection = cycleDirection;
        }

        void Shoot() // shoot does the shooting
        {
            switch (currentWeapon)
            {
                case WeaponType.PeaShooter: ShootPeaShooter(); break;
                case WeaponType.AutoRifle:  ShootAutoRifle();   break;
                case WeaponType.TripleShot: ShootTripleShot(); break;
            }
        }

        private void ShootPeaShooter() // basic shooter
        {
            if (!Input.GetButtonDown("Fire1")) return;

            Instantiate(peaBullet, projectileSpawnPoint.position, transform.rotation);
        }
        private void ShootAutoRifle() // auto shooter, nerfed to be less good
        {
            if (cooldownUntilNextBullet > 0) return;
            Instantiate(autoBullet, projectileSpawnPoint.position, transform.rotation);
            cooldownUntilNextBullet = 0.3f;
        }
        private void ShootTripleShot() // triple shot
        {
            if (!Input.GetButtonDown("Fire1")) return; // must release Fire1 to keep shooting

            float yaw = transform.eulerAngles.y;

            float spread = 7;

            Instantiate(tripleBullet, projectileSpawnPoint.position, transform.rotation);
            Instantiate(tripleBullet, projectileSpawnPoint.position, Quaternion.Euler(0, yaw-spread, 0));
            Instantiate(tripleBullet, projectileSpawnPoint.position, Quaternion.Euler(0, yaw+spread, 0));


        }


        void OnGUI()//This draws the weapon being used on the screen
        {
            GUI.Label(new Rect(20, 520, 1000, 1000), "Current Weapon: " + currentWeapon); // this displays the players health and updates in real time on the GUI!       
        }


    }
}
