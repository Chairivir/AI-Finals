using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace DesignPatterns.ObjectPool {
    public class ProjectileLauncher : MonoBehaviour {
        [Tooltip("Prefab to shoot")]
        [SerializeField] private Projectile projectile;

        [Tooltip("Projectile force")]
        [SerializeField] private float muzzleVelocity = 700f;

        [Tooltip("End point of launcher where shots appear")]
        [SerializeField] private Transform muzzlePosition;
        [SerializeField] private GameObject projectileInHand;

        // stack-based ObjectPool available with Unity 2021 and above
        private IObjectPool<Projectile> objectPool;

        // throw an exception if we try to return an existing item, already in the pool
        [SerializeField] private bool collectionCheck = true;

        // extra options to control the pool capacity and maximum size
        [SerializeField] private int defaultCapacity = 20;
        [SerializeField] private int maxSize = 100;

        UnitManager unit;

        private void Awake()
        {
            objectPool = new ObjectPool<Projectile>(CreateProjectile,
                    OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject,
                    collectionCheck, defaultCapacity, maxSize);
        }

        private void Start()
        {
            unit = GetComponentInParent<UnitManager>();
        }

        // invoked when creating an item to populate the object pool
        private Projectile CreateProjectile()
        {
            Projectile projectileInstance = Instantiate(projectile);
            projectileInstance.ObjectPool = objectPool;
            return projectileInstance;
        }

        // invoked when returning an item to the object pool
        private void OnReleaseToPool(Projectile pooledObject)
        {
            pooledObject.gameObject.SetActive(false);
        }

        // invoked when retrieving the next item from the object pool
        private void OnGetFromPool(Projectile pooledObject)
        {
            pooledObject.gameObject.SetActive(true);
        }

        // invoked when we exceed the maximum number of pooled items (i.e. destroy the pooled object)
        private void OnDestroyPooledObject(Projectile pooledObject)
        {
            Destroy(pooledObject.gameObject);
        }

        void Shoot()
        {
            if (projectileInHand != null)
                projectileInHand.SetActive(false);
            
            // get a pooled object instead of instantiating
            Projectile projectileObject = objectPool.Get();

            if (projectileObject == null)
                return;

            // Set Projectile's data
            projectileObject.damage = Random.Range(unit.unitDamageRange[0], unit.unitDamageRange[1]);//unit.damage;
            projectileObject.enemyTag = unit.enemyTeam[unit.returnTeamAffliation];
            projectileObject.whoShotIt = unit.gameObject;

            // align to gun barrel/muzzle position
            projectileObject.transform.position = (muzzlePosition.position);//unit.cur.rotation);
            projectileObject.transform.LookAt(unit.currentTarget.transform);

            // move projectile forward
            projectileObject.GetComponent<Rigidbody>().AddForce(projectileObject.transform.forward * muzzleVelocity, ForceMode.Acceleration);
        }

        void GetArrow()
        {
            if (projectileInHand != null)
                projectileInHand.SetActive(true);
        }
    }
}
