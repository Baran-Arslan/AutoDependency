using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace iCare.Di.LoopSystem
{
    public interface IListenLoop
    {
        
    }
    
    public interface IListenTick : IListenLoop
    {
        void OnLoopTick();
    }

    public interface IListenOnEnable : IListenLoop
    {
        void OnLoopEnable();
    }

    public interface IListenStart : IListenLoop
    {
        void OnLoopStart();
    }

    public interface IListenFixedTick : IListenLoop
    {
        void OnLoopFixedTick();
    }

    public interface IListenLateTick : IListenLoop
    {
        void OnLoopLateTick();
    }

    public interface IListenDestroy : IListenLoop
    {
        void OnLoopDestroy();
    }

    public interface IListenOnDisable : IListenLoop
    {
        void OnLoopDisable();
    }

    public interface IListenSceneLoad : IListenLoop
    {
        void OnSceneLoad(Scene scene, LoadSceneMode mode);
    }

    public interface ICoroutineManager
    {
        Coroutine StartCoroutine(IEnumerator routine);
        void StopCoroutine(Coroutine coroutine);
        void StopAllCoroutines();
    }
}