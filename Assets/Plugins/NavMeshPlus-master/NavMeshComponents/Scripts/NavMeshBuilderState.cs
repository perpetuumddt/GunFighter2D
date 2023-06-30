using System;
using System.Collections.Generic;
using UnityEngine;

namespace Plugins.NavMeshPlus_master.NavMeshComponents.Scripts
{
    public class NavMeshBuilderState: IDisposable
    {
        public Matrix4x4 WorldToLocal;
        public Bounds WorldBounds;
        public IEnumerable<GameObject> Roots;
        private CompositeDisposable _disposable;
        private Dictionary<Type, System.Object> _mExtraState;
        private bool _disposed;

        public T GetExtraState<T>(bool dispose = true) where T : class, new()
        {
            if (_mExtraState == null)
            { 
                _mExtraState = new Dictionary<Type, System.Object>();
                _disposable = new CompositeDisposable();
            }
            if (!_mExtraState.TryGetValue(typeof(T), out System.Object extra))
            {
                extra = _mExtraState[typeof(T)] = new T();
                if (dispose)
                {
                    _disposable.Add(extra);
                }
            }

            return extra as T;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // TODO: dispose managed state (managed objects).
                _disposable?.Dispose();
            }

            // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
            // TODO: set large fields to null.

            _disposed = true;
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
    partial class CompositeDisposable: IDisposable
    {
        private bool _disposed;
        private List<IDisposable> _extraStates = new List<IDisposable>();

        public void Add(IDisposable dispose)
        {
            _extraStates.Add(dispose);
        }
        public void Add(object dispose)
        {
            if(dispose is IDisposable)
            {
                _extraStates.Add((IDisposable)dispose);
            }
        }
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // TODO: dispose managed state (managed objects).
                foreach (var item in _extraStates)
                {
                    item?.Dispose();
                }
                _extraStates.Clear();
            }

            // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
            // TODO: set large fields to null.

            _disposed = true;
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}