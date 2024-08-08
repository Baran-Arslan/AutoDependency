using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace iCare.Di {
    [Service(typeof(ICoroutineManager), true)]
    internal sealed class LoopProvider : MonoBehaviour, ICoroutineManager {
        int _awakeCount;
        IListenAwake[] _awakeListeners = new IListenAwake[64];
        HashSet<IListenAwake> _awakeSet = new();
        int _destroyCount;
        IListenOnDestroy[] _destroyListeners = new IListenOnDestroy[64];
        HashSet<IListenOnDestroy> _destroySet = new();
        int _fixedTickCount;

        IListenFixedTick[] _fixedTickListeners = new IListenFixedTick[64];
        HashSet<IListenFixedTick> _fixedTickSet = new();
        int _lateTickCount;
        IListenLateTick[] _lateTickListeners = new IListenLateTick[128];
        HashSet<IListenLateTick> _lateTickSet = new();
        int _onDisableCount;
        IListenOnDisable[] _onDisableListeners = new IListenOnDisable[64];
        HashSet<IListenOnDisable> _onDisableSet = new();
        int _onEnableCount;
        IListenOnEnable[] _onEnableListeners = new IListenOnEnable[64];
        HashSet<IListenOnEnable> _onEnableSet = new();
        int _sceneLoadCount;
        IListenSceneLoad[] _sceneLoadListeners = new IListenSceneLoad[64];
        HashSet<IListenSceneLoad> _sceneLoadSet = new();
        int _startCount;
        IListenStart[] _startListeners = new IListenStart[64];
        HashSet<IListenStart> _startSet = new();
        int _tickCount;
        IListenTick[] _tickListeners = new IListenTick[128];
        HashSet<IListenTick> _tickSet = new();

        public static LoopProvider Instance { get; private set; }


        void Awake() {
            for (var i = 0; i < _awakeCount; i++)
                _awakeListeners[i].OnLoopAwake();

            _awakeListeners = null;
            _awakeSet = null;

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void Start() {
            for (var i = 0; i < _startCount; i++)
                _startListeners[i].OnLoopStart();

            _startListeners = null;
            _startSet = null;
        }

        void Update() {
            for (var i = 0; i < _tickCount; i++)
                _tickListeners[i].OnLoopTick();
        }

        void FixedUpdate() {
            for (var i = 0; i < _fixedTickCount; i++)
                _fixedTickListeners[i].OnLoopFixedTick();
        }

        void LateUpdate() {
            for (var i = 0; i < _lateTickCount; i++)
                _lateTickListeners[i].OnLoopLateTick();
        }

        void OnEnable() {
            for (var i = 0; i < _onEnableCount; i++)
                _onEnableListeners[i].OnLoopEnable();

            _onEnableListeners = null;
            _onEnableSet = null;
        }

        void OnDisable() {
            for (var i = 0; i < _onDisableCount; i++)
                _onDisableListeners[i].OnLoopDisable();

            _onDisableListeners = null;
            _onDisableSet = null;
        }

        void OnDestroy() {
            for (var i = 0; i < _destroyCount; i++)
                _destroyListeners[i].OnLoopDestroy();

            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        internal static void Create() {
            var obj = new GameObject("LoopProvider");
            obj.SetActive(false);
            var provider = obj.AddComponent<LoopProvider>();
            Instance = provider;
        }

        internal static void Activate() {
            Instance.gameObject.SetActive(true);
        }

        void OnSceneLoaded(Scene arg0, LoadSceneMode arg1) {
            for (var i = 0; i < _sceneLoadCount; i++)
                _sceneLoadListeners[i].OnSceneLoad(arg0, arg1);
        }

        static void InternalRegister<T>(T listener, ref T[] listeners, ref HashSet<T> listenerSet, ref int listenerCount)
            where T : class {
            if (!listenerSet.Add(listener)) {
                Debug.LogWarning($"Listener of type {typeof(T).Name} is already registered.");
                return;
            }

            if (listenerCount == listeners.Length)
                Array.Resize(ref listeners, listeners.Length * 2);
            listeners[listenerCount++] = listener;
        }

        static void InternalUnregister<T>(T listener, ref T[] listeners, ref HashSet<T> listenerSet, ref int listenerCount)
            where T : class {
            if (!listenerSet.Remove(listener)) {
                Debug.LogWarning($"Listener of type {typeof(T).Name} is not registered.");
                return;
            }

            for (var i = 0; i < listenerCount; i++)
                if (listeners[i] == listener) {
                    listeners[i] = listeners[--listenerCount];
                    listeners[listenerCount] = null;
                    return;
                }
        }


        public void AddLoopListener(object instance) {
            if (instance is IListenTick tick)
                InternalRegister(tick, ref _tickListeners, ref _tickSet, ref _tickCount);
            if (instance is IListenOnEnable onEnable)
                InternalRegister(onEnable, ref _onEnableListeners, ref _onEnableSet, ref _onEnableCount);
            if (instance is IListenStart start)
                InternalRegister(start, ref _startListeners, ref _startSet, ref _startCount);
            if (instance is IListenFixedTick fixedTick)
                InternalRegister(fixedTick, ref _fixedTickListeners, ref _fixedTickSet, ref _fixedTickCount);
            if (instance is IListenLateTick lateTick)
                InternalRegister(lateTick, ref _lateTickListeners, ref _lateTickSet, ref _lateTickCount);
            if (instance is IListenOnDestroy destroy)
                InternalRegister(destroy, ref _destroyListeners, ref _destroySet, ref _destroyCount);
            if (instance is IListenOnDisable onDisable)
                InternalRegister(onDisable, ref _onDisableListeners, ref _onDisableSet, ref _onDisableCount);
            if (instance is IListenSceneLoad sceneLoad)
                InternalRegister(sceneLoad, ref _sceneLoadListeners, ref _sceneLoadSet, ref _sceneLoadCount);
            if (instance is IListenAwake awake) InternalRegister(awake, ref _awakeListeners, ref _awakeSet, ref _awakeCount);
        }

        public void RemoveLoopListener(object instance) {
            if (instance is IListenTick tick)
                InternalUnregister(tick, ref _tickListeners, ref _tickSet, ref _tickCount);
            if (instance is IListenOnEnable onEnable)
                InternalUnregister(onEnable, ref _onEnableListeners, ref _onEnableSet, ref _onEnableCount);
            if (instance is IListenStart start)
                InternalRegister(start, ref _startListeners, ref _startSet, ref _startCount);
            if (instance is IListenFixedTick fixedTick)
                InternalUnregister(fixedTick, ref _fixedTickListeners, ref _fixedTickSet, ref _fixedTickCount);
            if (instance is IListenLateTick lateTick)
                InternalUnregister(lateTick, ref _lateTickListeners, ref _lateTickSet, ref _lateTickCount);
            if (instance is IListenOnDestroy destroy)
                InternalUnregister(destroy, ref _destroyListeners, ref _destroySet, ref _destroyCount);
            if (instance is IListenOnDisable onDisable)
                InternalUnregister(onDisable, ref _onDisableListeners, ref _onDisableSet, ref _onDisableCount);
            if (instance is IListenSceneLoad sceneLoad)
                InternalUnregister(sceneLoad, ref _sceneLoadListeners, ref _sceneLoadSet, ref _sceneLoadCount);
            if (instance is IListenAwake awake) InternalUnregister(awake, ref _awakeListeners, ref _awakeSet, ref _awakeCount);
        }
    }
}