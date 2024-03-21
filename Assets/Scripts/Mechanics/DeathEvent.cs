using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace DesignPatterns.ObjectPool
{
    public class DeathEvent : MonoBehaviour
    {
        [Tooltip("Ragdoll to spawn")]
        [SerializeField] private RagdollPool ragdollObject;

        // stack-based ObjectPool available with Unity 2021 and above
        private IObjectPool<RagdollPool> objectPool;

        // throw an exception if we try to return an existing item, already in the pool
        [SerializeField] private bool collectionCheck = true;

        // extra options to control the pool capacity and maximum size
        [SerializeField] private int defaultCapacity = 20;
        [SerializeField] private int maxSize = 100;

        UnitManager unit;
        [SerializeField] UnitList list;
        [SerializeField] UIUnitCount count;

        private void Awake()
        {
            objectPool = new ObjectPool<RagdollPool>(CreateRagdoll,
                    OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject,
                    collectionCheck, defaultCapacity, maxSize);
        }

        private void Start()
        {

            unit = GetComponentInParent<UnitManager>();
            count = FindAnyObjectByType<UIUnitCount>();
            if (unit.teamAffliation == Team.TeamA)
                list = GameObject.FindWithTag(TeamCount.TeamACount.ToString()).GetComponent<UnitList>();
            if (unit.teamAffliation == Team.TeamB)
                list = GameObject.FindWithTag(TeamCount.TeamBCount.ToString()).GetComponent<UnitList>();

        }

        // invoked when creating an item to populate the object pool
        private RagdollPool CreateRagdoll()
        {
            RagdollPool ragdollInstance = Instantiate(ragdollObject);
            ragdollInstance.ObjectPool = objectPool;
            return ragdollInstance;
        }

        // invoked when returning an item to the object pool
        private void OnReleaseToPool(RagdollPool pooledObject)
        {
            pooledObject.gameObject.SetActive(false);
        }

        // invoked when retrieving the next item from the object pool
        private void OnGetFromPool(RagdollPool pooledObject)
        {
            pooledObject.gameObject.SetActive(true);
        }

        // invoked when we exceed the maximum number of pooled items (i.e. destroy the pooled object)
        private void OnDestroyPooledObject(RagdollPool pooledObject)
        {
            Destroy(pooledObject.gameObject);
        }

        public void SpawnRagdoll() {
            count.UpdateCount(-1, unit.returnTeamAffliation);

            list.unitsInATeam.Remove(this.gameObject);

            if (ragdollObject == null)
            {
                unit.gameObject.SetActive(false);
                return;
            }

            // get a pooled object instead of instantiating
            RagdollPool ragdoll = objectPool.Get();

            if (ragdoll == null)
                return;

            // place the ragdoll to the unit
            ragdoll.transform.SetPositionAndRotation(unit.transform.position, unit.transform.rotation);

            // disable the main unit
            gameObject.SetActive(false);
        }
    }
}