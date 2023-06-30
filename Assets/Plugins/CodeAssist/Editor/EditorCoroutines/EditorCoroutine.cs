/*
 * Derived from Unity package
 * https://docs.unity3d.com/Packages/com.unity.editorcoroutines@0.0/api/Unity.EditorCoroutines.Editor.html
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//namespace Unity.EditorCoroutines.Editor
namespace Plugins.CodeAssist.Editor.EditorCoroutines
{
    /// <summary>
    /// A handle to an EditorCoroutine, can be passed to <see cref="EditorCoroutineUtility">EditorCoroutineUtility</see> methods to control lifetime.
    /// </summary>
    public class EditorCoroutine
    {
        private struct YieldProcessor
        {
            enum DataType : byte
            {
                None = 0,
                WaitForSeconds = 1,
                EditorCoroutine = 2,
                AsyncOp = 3,
            }
            struct ProcessorData
            {
                public DataType Type;
                public double TargetTime;
                public object Current;
            }

            ProcessorData _data;

            public void Set(object yield)
            {
                if (yield == _data.Current)
                    return;

                var type = yield.GetType();
                var dataType = DataType.None;
                double targetTime = -1;

                if(type == typeof(EditorWaitForSeconds))
                {
                    targetTime = EditorApplication.timeSinceStartup + (yield as EditorWaitForSeconds).WaitTime;
                    dataType = DataType.WaitForSeconds;
                }
                else if(type == typeof(EditorCoroutine))
                {
                    dataType = DataType.EditorCoroutine;
                }
                else if(type == typeof(AsyncOperation) || type.IsSubclassOf(typeof(AsyncOperation)))
                {
                    dataType = DataType.AsyncOp;
                }

                _data = new ProcessorData { Current = yield, TargetTime = targetTime, Type = dataType };
            }

            public bool MoveNext(IEnumerator enumerator)
            {
                var advance = _data.Type switch
                {
                    DataType.WaitForSeconds => _data.TargetTime <= EditorApplication.timeSinceStartup,
                    DataType.EditorCoroutine => (_data.Current as EditorCoroutine)._mIsDone,
                    DataType.AsyncOp => (_data.Current as AsyncOperation).isDone,
                    _ => _data.Current == enumerator.Current,//a IEnumerator or a plain object was passed to the implementation
                };
                if (advance)
                {
                    _data = default;// (ProcessorData);
                    return enumerator.MoveNext();
                }
                return true;
            }
        }

        internal WeakReference MOwner;
        IEnumerator _mRoutine;
        YieldProcessor _mProcessor;

        bool _mIsDone;

        internal EditorCoroutine(IEnumerator routine)
        {
            MOwner = null;
            _mRoutine = routine;
            EditorApplication.update += MoveNext;
        }

        internal EditorCoroutine(IEnumerator routine, object owner)
        {
            _mProcessor = new YieldProcessor();
            MOwner = new WeakReference(owner);
            _mRoutine = routine;
            EditorApplication.update += MoveNext;
        }

        internal void MoveNext()
        {
            if (MOwner != null && !MOwner.IsAlive)
            {
                EditorApplication.update -= MoveNext;
                return;
            }

            bool done = ProcessIEnumeratorRecursive(_mRoutine);
            _mIsDone = !done;

            if (_mIsDone)
                EditorApplication.update -= MoveNext;
        }

        static readonly Stack<IEnumerator> KIEnumeratorProcessingStack = new Stack<IEnumerator>(32);
        private bool ProcessIEnumeratorRecursive(IEnumerator enumerator)
        {
            var root = enumerator;
            while(enumerator.Current as IEnumerator != null)
            {
                KIEnumeratorProcessingStack.Push(enumerator);
                enumerator = enumerator.Current as IEnumerator;
            }

            //process leaf
            _mProcessor.Set(enumerator.Current);
            var result = _mProcessor.MoveNext(enumerator);

            while (KIEnumeratorProcessingStack.Count > 1)
            {
                if (!result)
                {
                    result = KIEnumeratorProcessingStack.Pop().MoveNext();
                }
                else
                    KIEnumeratorProcessingStack.Clear();
            }

            if (KIEnumeratorProcessingStack.Count > 0 && !result && root == KIEnumeratorProcessingStack.Pop())
            {
                result = root.MoveNext();
            }

            return result;
        }

        internal void Stop()
        {
            MOwner = null;
            _mRoutine = null;
            EditorApplication.update -= MoveNext;
        }
    }
}
