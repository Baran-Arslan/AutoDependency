using UnityEngine;

namespace iCare.Di {
    public static class AddComponentExtensions {
        public static T AddMono<T, TArg1>(this GameObject gameObject, TArg1 arg1) where T : Mono<TArg1> {
            var wasObjectActive = gameObject.activeSelf;
            gameObject.SetActive(false);
            var component = gameObject.AddComponent<T>();
            component.Construct(arg1);
            gameObject.SetActive(wasObjectActive);
            return component;
        }

        public static T AddMono<T, TArg1, TArg2>(this GameObject gameObject, TArg1 arg1, TArg2 arg2)
            where T : Mono<TArg1, TArg2> {
            var wasObjectActive = gameObject.activeSelf;
            gameObject.SetActive(false);
            var component = gameObject.AddComponent<T>();
            component.Construct(arg1, arg2);
            gameObject.SetActive(wasObjectActive);
            return component;
        }

        public static T AddMono<T, TArg1, TArg2, TArg3>(this GameObject gameObject, TArg1 arg1, TArg2 arg2, TArg3 arg3)
            where T : Mono<TArg1, TArg2, TArg3> {
            var wasObjectActive = gameObject.activeSelf;
            gameObject.SetActive(false);
            var component = gameObject.AddComponent<T>();
            component.Construct(arg1, arg2, arg3);
            gameObject.SetActive(wasObjectActive);
            return component;
        }

        public static T AddMono<T, TArg1, TArg2, TArg3, TArg4>(this GameObject gameObject, TArg1 arg1, TArg2 arg2,
            TArg3 arg3, TArg4 arg4) where T : Mono<TArg1, TArg2, TArg3, TArg4> {
            var wasObjectActive = gameObject.activeSelf;
            gameObject.SetActive(false);
            var component = gameObject.AddComponent<T>();
            component.Construct(arg1, arg2, arg3, arg4);
            gameObject.SetActive(wasObjectActive);
            return component;
        }

        public static T AddMono<T, TArg1, TArg2, TArg3, TArg4, TArg5>(this GameObject gameObject, TArg1 arg1,
            TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5) where T : Mono<TArg1, TArg2, TArg3, TArg4, TArg5> {
            var wasObjectActive = gameObject.activeSelf;
            gameObject.SetActive(false);
            var component = gameObject.AddComponent<T>();
            component.Construct(arg1, arg2, arg3, arg4, arg5);
            gameObject.SetActive(wasObjectActive);
            return component;
        }

        public static T AddMono<T, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(this GameObject gameObject,
            TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6)
            where T : Mono<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6> {
            var wasObjectActive = gameObject.activeSelf;
            gameObject.SetActive(false);
            var component = gameObject.AddComponent<T>();
            component.Construct(arg1, arg2, arg3, arg4, arg5, arg6);
            gameObject.SetActive(wasObjectActive);
            return component;
        }

        public static T AddMono<T, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(this GameObject gameObject,
            TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7)
            where T : Mono<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7> {
            var wasObjectActive = gameObject.activeSelf;
            gameObject.SetActive(false);
            var component = gameObject.AddComponent<T>();
            component.Construct(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
            gameObject.SetActive(wasObjectActive);
            return component;
        }

        public static T AddMono<T, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(this GameObject gameObject,
            TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8)
            where T : Mono<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8> {
            var wasObjectActive = gameObject.activeSelf;
            gameObject.SetActive(false);
            var component = gameObject.AddComponent<T>();
            component.Construct(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
            gameObject.SetActive(wasObjectActive);
            return component;
        }

        public static T AddMono<T, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(
            this GameObject gameObject,
            TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9)
            where T : Mono<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9> {
            var wasObjectActive = gameObject.activeSelf;
            gameObject.SetActive(false);
            var component = gameObject.AddComponent<T>();
            component.Construct(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
            gameObject.SetActive(wasObjectActive);
            return component;
        }

        public static T AddMono<T, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10>(
            this GameObject gameObject,
            TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9,
            TArg10 arg10)
            where T : Mono<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10> {
            var wasObjectActive = gameObject.activeSelf;
            gameObject.SetActive(false);
            var component = gameObject.AddComponent<T>();
            component.Construct(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
            gameObject.SetActive(wasObjectActive);
            return component;
        }
    }
}