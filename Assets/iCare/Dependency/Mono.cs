using System;
using UnityEngine;

namespace iCare
{
    public abstract class Mono<T> : MonoBehaviour, IConstruct<T>
    {
        [SerializeField] Dependency<T> value1;

        bool _isConstructed;

        void Awake()
        {
            if (!_isConstructed) Construct(value1.Resolve(this));

            OnAwake();
        }

        public void Construct(T param1)
        {
            if (_isConstructed)
                throw new Exception("Already constructed".Highlight());

            Init(param1);
            _isConstructed = true;
            value1 = null;
        }

        protected abstract void Init(T param1);

        protected virtual void OnAwake()
        {
        }
    }

    public abstract class Mono<T1, T2> : MonoBehaviour, IConstruct<T1, T2>
    {
        [SerializeField] Dependency<T1> value1;
        [SerializeField] Dependency<T2> value2;

        bool _isConstructed;

        void Awake()
        {
            if (!_isConstructed) Construct(value1.Resolve(this), value2.Resolve(this));

            OnAwake();
        }

        public void Construct(T1 param1, T2 param2)
        {
            if (_isConstructed)
                throw new Exception("Already constructed".Highlight());

            Init(param1, param2);
            _isConstructed = true;
            value1 = null;
            value2 = null;
        }

        protected abstract void Init(T1 param1, T2 param2);

        protected virtual void OnAwake()
        {
        }
    }

    public abstract class Mono<T1, T2, T3> : MonoBehaviour, IConstruct<T1, T2, T3>
    {
        [SerializeField] Dependency<T1> value1;
        [SerializeField] Dependency<T2> value2;
        [SerializeField] Dependency<T3> value3;

        bool _isConstructed;

        void Awake()
        {
            if (!_isConstructed) Construct(value1.Resolve(this), value2.Resolve(this), value3.Resolve(this));

            OnAwake();
        }

        public void Construct(T1 param1, T2 param2, T3 param3)
        {
            if (_isConstructed)
                throw new Exception("Already constructed".Highlight());

            Init(param1, param2, param3);
            _isConstructed = true;
            value1 = null;
            value2 = null;
            value3 = null;
        }

        protected abstract void Init(T1 param1, T2 param2, T3 param3);

        protected virtual void OnAwake()
        {
        }
    }

    public abstract class Mono<T1, T2, T3, T4> : MonoBehaviour, IConstruct<T1, T2, T3, T4>
    {
        [SerializeField] Dependency<T1> value1;
        [SerializeField] Dependency<T2> value2;
        [SerializeField] Dependency<T3> value3;
        [SerializeField] Dependency<T4> value4;

        bool _isConstructed;

        void Awake()
        {
            if (!_isConstructed)
                Construct(value1.Resolve(this), value2.Resolve(this), value3.Resolve(this), value4.Resolve(this));

            OnAwake();
        }

        public void Construct(T1 param1, T2 param2, T3 param3, T4 param4)
        {
            if (_isConstructed)
                throw new Exception("Already constructed".Highlight());

            Init(param1, param2, param3, param4);
            _isConstructed = true;
            value1 = null;
            value2 = null;
            value3 = null;
            value4 = null;
        }

        protected abstract void Init(T1 param1, T2 param2, T3 param3, T4 param4);

        protected virtual void OnAwake()
        {
        }
    }

    public abstract class Mono<T1, T2, T3, T4, T5> : MonoBehaviour, IConstruct<T1, T2, T3, T4, T5>
    {
        [SerializeField] Dependency<T1> value1;
        [SerializeField] Dependency<T2> value2;
        [SerializeField] Dependency<T3> value3;
        [SerializeField] Dependency<T4> value4;
        [SerializeField] Dependency<T5> value5;

        bool _isConstructed;

        void Awake()
        {
            if (!_isConstructed)
                Construct(value1.Resolve(this), value2.Resolve(this), value3.Resolve(this), value4.Resolve(this),
                    value5.Resolve(this));

            OnAwake();
        }

        public void Construct(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
        {
            if (_isConstructed)
                throw new Exception("Already constructed".Highlight());

            Init(param1, param2, param3, param4, param5);
            _isConstructed = true;
            value1 = null;
            value2 = null;
            value3 = null;
            value4 = null;
            value5 = null;
        }

        protected abstract void Init(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5);

        protected virtual void OnAwake()
        {
        }
    }

    public abstract class Mono<T1, T2, T3, T4, T5, T6> : MonoBehaviour, IConstruct<T1, T2, T3, T4, T5, T6>
    {
        [SerializeField] Dependency<T1> value1;
        [SerializeField] Dependency<T2> value2;
        [SerializeField] Dependency<T3> value3;
        [SerializeField] Dependency<T4> value4;
        [SerializeField] Dependency<T5> value5;
        [SerializeField] Dependency<T6> value6;

        bool _isConstructed;

        void Awake()
        {
            if (!_isConstructed)
                Construct(value1.Resolve(this), value2.Resolve(this), value3.Resolve(this), value4.Resolve(this),
                    value5.Resolve(this), value6.Resolve(this));

            OnAwake();
        }

        public void Construct(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6)
        {
            if (_isConstructed)
                throw new Exception("Already constructed".Highlight());

            Init(param1, param2, param3, param4, param5, param6);
            _isConstructed = true;
            value1 = null;
            value2 = null;
            value3 = null;
            value4 = null;
            value5 = null;
            value6 = null;
        }

        protected abstract void Init(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6);

        protected virtual void OnAwake()
        {
        }
    }

    public abstract class Mono<T1, T2, T3, T4, T5, T6, T7> : MonoBehaviour, IConstruct<T1, T2, T3, T4, T5, T6, T7>
    {
        [SerializeField] Dependency<T1> value1;
        [SerializeField] Dependency<T2> value2;
        [SerializeField] Dependency<T3> value3;
        [SerializeField] Dependency<T4> value4;
        [SerializeField] Dependency<T5> value5;
        [SerializeField] Dependency<T6> value6;
        [SerializeField] Dependency<T7> value7;

        bool _isConstructed;

        void Awake()
        {
            if (!_isConstructed)
                Construct(value1.Resolve(this), value2.Resolve(this), value3.Resolve(this), value4.Resolve(this),
                    value5.Resolve(this), value6.Resolve(this), value7.Resolve(this));

            OnAwake();
        }

        public void Construct(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7)
        {
            if (_isConstructed)
                throw new Exception("Already constructed".Highlight());

            Init(param1, param2, param3, param4, param5, param6, param7);
            _isConstructed = true;
            value1 = null;
            value2 = null;
            value3 = null;
            value4 = null;
            value5 = null;
            value6 = null;
            value7 = null;
        }

        protected abstract void Init(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7);

        protected virtual void OnAwake()
        {
        }
    }

    public abstract class Mono<T1, T2, T3, T4, T5, T6, T7, T8> : MonoBehaviour,
        IConstruct<T1, T2, T3, T4, T5, T6, T7, T8>
    {
        [SerializeField] Dependency<T1> value1;
        [SerializeField] Dependency<T2> value2;
        [SerializeField] Dependency<T3> value3;
        [SerializeField] Dependency<T4> value4;
        [SerializeField] Dependency<T5> value5;
        [SerializeField] Dependency<T6> value6;
        [SerializeField] Dependency<T7> value7;
        [SerializeField] Dependency<T8> value8;

        bool _isConstructed;

        void Awake()
        {
            if (!_isConstructed)
                Construct(value1.Resolve(this), value2.Resolve(this), value3.Resolve(this), value4.Resolve(this),
                    value5.Resolve(this), value6.Resolve(this), value7.Resolve(this), value8.Resolve(this));

            OnAwake();
        }

        public void Construct(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8)
        {
            if (_isConstructed)
                throw new Exception("Already constructed".Highlight());

            Init(param1, param2, param3, param4, param5, param6, param7, param8);
            _isConstructed = true;
            value1 = null;
            value2 = null;
            value3 = null;
            value4 = null;
            value5 = null;
            value6 = null;
            value7 = null;
            value8 = null;
        }

        protected abstract void Init(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7,
            T8 param8);

        protected virtual void OnAwake()
        {
        }
    }

    public abstract class Mono<T1, T2, T3, T4, T5, T6, T7, T8, T9> : MonoBehaviour,
        IConstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9>
    {
        [SerializeField] Dependency<T1> value1;
        [SerializeField] Dependency<T2> value2;
        [SerializeField] Dependency<T3> value3;
        [SerializeField] Dependency<T4> value4;
        [SerializeField] Dependency<T5> value5;
        [SerializeField] Dependency<T6> value6;
        [SerializeField] Dependency<T7> value7;
        [SerializeField] Dependency<T8> value8;
        [SerializeField] Dependency<T9> value9;

        bool _isConstructed;

        void Awake()
        {
            if (!_isConstructed)
                Construct(value1.Resolve(this), value2.Resolve(this), value3.Resolve(this), value4.Resolve(this),
                    value5.Resolve(this), value6.Resolve(this), value7.Resolve(this), value8.Resolve(this),
                    value9.Resolve(this));

            OnAwake();
        }

        public void Construct(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8,
            T9 param9)
        {
            if (_isConstructed)
                throw new Exception("Already constructed".Highlight());

            Init(param1, param2, param3, param4, param5, param6, param7, param8, param9);
            _isConstructed = true;
            value1 = null;
            value2 = null;
            value3 = null;
            value4 = null;
            value5 = null;
            value6 = null;
            value7 = null;
            value8 = null;
            value9 = null;
        }

        protected abstract void Init(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7,
            T8 param8, T9 param9);

        protected virtual void OnAwake()
        {
        }
    }

    public abstract class Mono<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : MonoBehaviour,
        IConstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>
    {
        [SerializeField] Dependency<T1> value1;
        [SerializeField] Dependency<T2> value2;
        [SerializeField] Dependency<T3> value3;
        [SerializeField] Dependency<T4> value4;
        [SerializeField] Dependency<T5> value5;
        [SerializeField] Dependency<T6> value6;
        [SerializeField] Dependency<T7> value7;
        [SerializeField] Dependency<T8> value8;
        [SerializeField] Dependency<T9> value9;
        [SerializeField] Dependency<T10> value10;

        bool _isConstructed;

        void Awake()
        {
            if (!_isConstructed)
                Construct(value1.Resolve(this), value2.Resolve(this), value3.Resolve(this), value4.Resolve(this),
                    value5.Resolve(this), value6.Resolve(this), value7.Resolve(this), value8.Resolve(this),
                    value9.Resolve(this), value10.Resolve(this));

            OnAwake();
        }

        public void Construct(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8,
            T9 param9, T10 param10)
        {
            if (_isConstructed)
                throw new Exception("Already constructed".Highlight());

            Init(param1, param2, param3, param4, param5, param6, param7, param8, param9, param10);
            _isConstructed = true;
            value1 = null;
            value2 = null;
            value3 = null;
            value4 = null;
            value5 = null;
            value6 = null;
            value7 = null;
            value8 = null;
            value9 = null;
            value10 = null;
        }

        protected abstract void Init(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7,
            T8 param8, T9 param9, T10 param10);

        protected virtual void OnAwake()
        {
        }
    }
}