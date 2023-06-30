#nullable enable


using System.Collections.Concurrent;
using UnityEditor;

namespace Plugins.CodeAssist.Editor
{

    [InitializeOnLoad]
    public static class MainThreadDispatcher
    {
        readonly static ConcurrentBag<System.Action> Actions;

        static MainThreadDispatcher()
        {
            Actions = new ConcurrentBag<System.Action>();
            EditorApplication.update += Update;
        }

        static void Update()
        {
            while (Actions.TryTake(out var action))
            {
                action.Invoke();
            }
        }

        public static void Add(System.Action action) => Actions.Add(action);
    }
}