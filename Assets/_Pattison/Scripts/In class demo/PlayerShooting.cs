﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pattison
{
    public class PlayerShooting : MonoBehaviour
    {

        public enum WeaponType
        {
            PeaShooter, // 0 
            AutoRifle,  // 1
            TripleShot  // 2
        }


        public GameObject basicBullet;
        public Transform projectileSpawnPoint;
        public WeaponType currentWeapon = WeaponType.PeaShooter;


        float cooldownUntilNextBullet = 0;
        /// <summary>
        /// Whatever our "CycleWeapon" axis value was one frame ago.
        /// </summary>
        int previousCycleDir = 0;


        void Start() {

        }

        
        void Update() {

            CycleWeapons();

            if (cooldownUntilNextBullet > 0) cooldownUntilNextBullet -= Time.deltaTime;
            if (Input.GetButton("Fire1")) Shoot();
        }

        private void CycleWeapons() {

            float cycleInput = Input.GetAxisRaw("CycleWeapons");

            int cycleDir = 0;
            if (cycleInput < 0) cycleDir = -1;
            if (cycleInput > 0) cycleDir = 1;


            if (previousCycleDir == 0) { // only change weapons if we WEREN'T trying to change weapons last frame

                int index = (int)currentWeapon + cycleDir;

                int max = System.Enum.GetNames(typeof(WeaponType)).Length - 1;

                if (index < 0) index = max;
                if (index > max) index = 0;

                currentWeapon = (WeaponType)index;
            }

            previousCycleDir = cycleDir;
        }

        void Shoot() {
            switch (currentWeapon) {
                case WeaponType.PeaShooter: ShootPeaShooter(); break;
                case WeaponType.AutoRifle:  ShootAutoRifle();  break;
                case WeaponType.TripleShot: ShootTripleShot(); break;
            }
        }

        private void ShootPeaShooter() {

            if (!Input.GetButtonDown("Fire1")) return;

            Instantiate(basicBullet, projectileSpawnPoint.position, transform.rotation);

        }
        private void ShootAutoRifle() {

            if (cooldownUntilNextBullet > 0) return;

            Instantiate(basicBullet, projectileSpawnPoint.position, transform.rotation);
            cooldownUntilNextBullet = 0.1f;
        }
        private void ShootTripleShot() {
            if (!Input.GetButtonDown("Fire1")) return; // must release "Fire1" to keep shooting

            float yaw = transform.eulerAngles.y;

            float spread = 10;

            Instantiate(basicBullet, projectileSpawnPoint.position, transform.rotation);
            Instantiate(basicBullet, projectileSpawnPoint.position, Quaternion.Euler(0, yaw - spread, 0));
            Instantiate(basicBullet, projectileSpawnPoint.position, Quaternion.Euler(0, yaw + spread, 0));
        }


    }
}