﻿using System;
using System.Runtime.CompilerServices;
using Jint.Runtime.Environments;

namespace Jint.Runtime
{
    internal sealed class ExecutionContextStack
    {
        private ExecutionContext[] _array;
        private int _size;

        private const int DefaultCapacity = 4;

        public ExecutionContextStack()
        {
            _array = new ExecutionContext[4];
            _size = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref readonly ExecutionContext Peek()
        {
            if (_size == 0)
            {
                ThrowEmptyStackException();
            }
            return ref _array[_size - 1];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Pop()
        {
            if (_size == 0)
            {
                ThrowEmptyStackException();
            }
            _size--;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Push(in ExecutionContext item)
        {
            if (_size == _array.Length)
            {
                var newSize = 2 * _array.Length;
                var newArray = new ExecutionContext[newSize];
                Array.Copy(_array, 0, newArray, 0, _size);
                _array = newArray;
            }

            _array[_size++] = item;
        }

        private static void ThrowEmptyStackException()
        {
            throw new InvalidOperationException("stack is empty");
        }

        public void ReplaceTopLexicalEnvironment(LexicalEnvironment newEnv)
        {
            _array[_size - 1] = _array[_size - 1].UpdateLexicalEnvironment(newEnv);
        }
    }
}