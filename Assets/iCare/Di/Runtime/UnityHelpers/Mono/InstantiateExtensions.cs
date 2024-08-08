using UnityEngine;

namespace iCare.Di {
    public static class InstantiateExtensions {
        public static T Instantiate<T, TArg1>(this T prefab, TArg1 arg1) where T : Mono<TArg1> {
            var setActiveState = prefab.gameObject.activeSelf;
            prefab.gameObject.SetActive(false);
            var instance = Object.Instantiate(prefab);
            instance.Construct(arg1);
            instance.gameObject.SetActive(setActiveState);
            prefab.gameObject.SetActive(setActiveState);
            return instance;
        }

        public static T Instantiate<T, TArg1, TArg2>(this T prefab, TArg1 arg1, TArg2 arg2) where T : Mono<TArg1, TArg2> {
            var setActiveState = prefab.gameObject.activeSelf;
            prefab.gameObject.SetActive(false);
            var instance = Object.Instantiate(prefab);
            instance.Construct(arg1, arg2);
            instance.gameObject.SetActive(setActiveState);
            prefab.gameObject.SetActive(setActiveState);

            return instance;
        }

        public static T Instantiate<T, TArg1, TArg2, TArg3>(this T prefab, TArg1 arg1, TArg2 arg2, TArg3 arg3)
            where T : Mono<TArg1, TArg2, TArg3> {
            var setActiveState = prefab.gameObject.activeSelf;
            prefab.gameObject.SetActive(false);
            var instance = Object.Instantiate(prefab);
            instance.Construct(arg1, arg2, arg3);
            instance.gameObject.SetActive(setActiveState);
            prefab.gameObject.SetActive(setActiveState);

            return instance;
        }

        public static T Instantiate<T, TArg1, TArg2, TArg3, TArg4>(this T prefab, TArg1 arg1, TArg2 arg2, TArg3 arg3,
            TArg4 arg4) where T : Mono<TArg1, TArg2, TArg3, TArg4> {
            var setActiveState = prefab.gameObject.activeSelf;
            prefab.gameObject.SetActive(false);
            var instance = Object.Instantiate(prefab);
            instance.Construct(arg1, arg2, arg3, arg4);
            instance.gameObject.SetActive(setActiveState);
            prefab.gameObject.SetActive(setActiveState);

            return instance;
        }

        public static T Instantiate<T, TArg1, TArg2, TArg3, TArg4, TArg5>(this T prefab, TArg1 arg1, TArg2 arg2,
            TArg3 arg3, TArg4 arg4, TArg5 arg5) where T : Mono<TArg1, TArg2, TArg3, TArg4, TArg5> {
            var setActiveState = prefab.gameObject.activeSelf;
            prefab.gameObject.SetActive(false);
            var instance = Object.Instantiate(prefab);
            instance.Construct(arg1, arg2, arg3, arg4, arg5);
            instance.gameObject.SetActive(setActiveState);
            prefab.gameObject.SetActive(setActiveState);

            return instance;
        }

        public static T Instantiate<T, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(this T prefab, TArg1 arg1, TArg2 arg2,
            TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6) where T : Mono<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6> {
            var setActiveState = prefab.gameObject.activeSelf;
            prefab.gameObject.SetActive(false);
            var instance = Object.Instantiate(prefab);
            instance.Construct(arg1, arg2, arg3, arg4, arg5, arg6);
            instance.gameObject.SetActive(setActiveState);
            prefab.gameObject.SetActive(setActiveState);

            return instance;
        }

        public static T Instantiate<T, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(this T prefab, TArg1 arg1,
            TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7)
            where T : Mono<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7> {
            var setActiveState = prefab.gameObject.activeSelf;
            prefab.gameObject.SetActive(false);
            var instance = Object.Instantiate(prefab);
            instance.Construct(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
            instance.gameObject.SetActive(setActiveState);
            prefab.gameObject.SetActive(setActiveState);

            return instance;
        }

        public static T Instantiate<T, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(this T prefab,
            TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8)
            where T : Mono<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8> {
            var setActiveState = prefab.gameObject.activeSelf;
            prefab.gameObject.SetActive(false);
            var instance = Object.Instantiate(prefab);
            instance.Construct(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
            instance.gameObject.SetActive(setActiveState);
            prefab.gameObject.SetActive(setActiveState);
            return instance;
        }

        public static T Instantiate<T, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(this T prefab,
            TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9)
            where T : Mono<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9> {
            var setActiveState = prefab.gameObject.activeSelf;
            prefab.gameObject.SetActive(false);
            var instance = Object.Instantiate(prefab);
            instance.Construct(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
            instance.gameObject.SetActive(setActiveState);
            prefab.gameObject.SetActive(setActiveState);

            return instance;
        }

        public static T Instantiate<T, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10>(
            this T prefab, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7,
            TArg8 arg8, TArg9 arg9, TArg10 arg10)
            where T : Mono<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10> {
            var setActiveState = prefab.gameObject.activeSelf;
            prefab.gameObject.SetActive(false);
            var instance = Object.Instantiate(prefab);
            instance.Construct(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
            instance.gameObject.SetActive(setActiveState);
            prefab.gameObject.SetActive(setActiveState);

            return instance;
        }
    }
}