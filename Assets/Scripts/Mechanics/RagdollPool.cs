using System.Collections;
using System.Collections.Generic;
using DesignPatterns.ObjectPool;
using UnityEngine;
using UnityEngine.Pool;


namespace DesignPatterns.ObjectPool
{
    public class RagdollPool : MonoBehaviour
    {
        private IObjectPool<RagdollPool> objectPool;

        public IObjectPool<RagdollPool> ObjectPool { set => objectPool = value; }
    }
}
