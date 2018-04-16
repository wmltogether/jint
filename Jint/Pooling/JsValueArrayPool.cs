﻿using System;
using Jint.Native;

namespace Jint.Pooling
{
    /// <summary>
    /// Cache reusable <see cref="JsValue" /> array instances as we allocate them a lot.
    /// </summary>
    internal sealed class JsValueArrayPool
    {
        private const int PoolSize = 15;
        private readonly ObjectPool<JsValue[]> _poolArray1;
        private readonly ObjectPool<JsValue[]> _poolArray2;
        private readonly ObjectPool<JsValue[]> _poolArray3;

        public JsValueArrayPool()
        {
            _poolArray1 = new ObjectPool<JsValue[]>(Factory1, PoolSize);
            _poolArray2 = new ObjectPool<JsValue[]>(Factory2, PoolSize);
            _poolArray3 = new ObjectPool<JsValue[]>(Factory3, PoolSize);
        }

        private static JsValue[] Factory1()
        {
            return new JsValue[1];
        }

        private static JsValue[] Factory2()
        {
            return new JsValue[2];
        }

        private static JsValue[] Factory3()
        {
            return new JsValue[3];
        }

        public JsValue[] RentArray(int size)
        {
            switch (size)
            {
                case 0:
                    return Array.Empty<JsValue>();
                case 1:
                    return _poolArray1.Allocate();
                case 2:
                    return _poolArray2.Allocate();
                case 3:
                    return _poolArray3.Allocate();
            }

            return new JsValue[size];
        }

        public void ReturnArray(JsValue[] array)
        {
            switch (array.Length)
            {
                case 1:
                    _poolArray1.Free(array);
                    break;
                case 2:
                    _poolArray2.Free(array);
                    break;
                case 3:
                    _poolArray3.Free(array);
                    break;
            }
        }
    }
}