using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace iCare.Di.LoopSystem {
    [Service(typeof(ICoroutineManager))]
    internal sealed class LoopProvider : MonoBehaviour, ICoroutineManager {
        int _destroyCount;
        IListenDestroy[] _destroyListeners = new IListenDestroy[64];
        HashSet<IListenDestroy> _destroySet = new();
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
            if (Instance != null) {
                Debug.LogWarning("LoopProvider already exists. Destroying duplicate.");
                Destroy(gameObject);
                return;
            }


            Instance = this;
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void Start() {
            for (var i = 0; i < _startCount; i++)
                _startListeners[i].OnLoopStart();
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
        }

        void OnDisable() {
            for (var i = 0; i < _onDisableCount; i++)
                _onDisableListeners[i].OnLoopDisable();
        }

        void OnDestroy() {
            for (var i = 0; i < _destroyCount; i++)
                _destroyListeners[i].OnLoopDestroy();

            SceneManager.sceneLoaded -= OnSceneLoaded;
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


        public void TryRegister(object instance) {
            switch (instance) {
                case IListenTick tick:
                    InternalRegister(tick, ref _tickListeners, ref _tickSet, ref _tickCount);
                    break;
                case IListenOnEnable onEnable:
                    InternalRegister(onEnable, ref _onEnableListeners, ref _onEnableSet, ref _onEnableCount);
                    break;
                case IListenStart start:
                    InternalRegister(start, ref _startListeners, ref _startSet, ref _startCount);
                    break;
                case IListenFixedTick fixedTick:
                    InternalRegister(fixedTick, ref _fixedTickListeners, ref _fixedTickSet, ref _fixedTickCount);
                    break;
                case IListenLateTick lateTick:
                    InternalRegister(lateTick, ref _lateTickListeners, ref _lateTickSet, ref _lateTickCount);
                    break;
                case IListenDestroy destroy:
                    InternalRegister(destroy, ref _destroyListeners, ref _destroySet, ref _destroyCount);
                    break;
                case IListenOnDisable onDisable:
                    InternalRegister(onDisable, ref _onDisableListeners, ref _onDisableSet, ref _onDisableCount);
                    break;
                case IListenSceneLoad sceneLoad:
                    InternalRegister(sceneLoad, ref _sceneLoadListeners, ref _sceneLoadSet, ref _sceneLoadCount);
                    break;
            }
        }

        public void TryUnregister(object instance) {
            switch (instance) {
                case IListenTick tick:
                    InternalUnregister(tick, ref _tickListeners, ref _tickSet, ref _tickCount);
                    break;
                case IListenOnEnable onEnable:
                    InternalUnregister(onEnable, ref _onEnableListeners, ref _onEnableSet, ref _onEnableCount);
                    break;
                case IListenStart start:
                    InternalRegister(start, ref _startListeners, ref _startSet, ref _startCount);
                    break;
                case IListenFixedTick fixedTick:
                    InternalUnregister(fixedTick, ref _fixedTickListeners, ref _fixedTickSet, ref _fixedTickCount);
                    break;
                case IListenLateTick lateTick:
                    InternalUnregister(lateTick, ref _lateTickListeners, ref _lateTickSet, ref _lateTickCount);
                    break;
                case IListenDestroy destroy:
                    InternalUnregister(destroy, ref _destroyListeners, ref _destroySet, ref _destroyCount);
                    break;
                case IListenOnDisable onDisable:
                    InternalUnregister(onDisable, ref _onDisableListeners, ref _onDisableSet, ref _onDisableCount);
                    break;
                case IListenSceneLoad sceneLoad:
                    InternalUnregister(sceneLoad, ref _sceneLoadListeners, ref _sceneLoadSet, ref _sceneLoadCount);
                    break;
            }
        }
    }
}