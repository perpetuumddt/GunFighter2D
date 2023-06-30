using System.Collections.Generic;

namespace Plugins.NavMeshPlus_master.NavMeshComponents.Scripts
{
    public interface INavMeshExtensionsProvider
    {
        int Count { get; }
        NavMeshExtension this[int index] { get; }
        void Add(NavMeshExtension extension, int order);
        void Remove(NavMeshExtension extension);
    }
    internal class NavMeshExtensionMeta
    {
        public int Order;

        public NavMeshExtensionMeta(int order, NavMeshExtension extension)
        {
            this.Order = order;
            this.Extension = extension;
        }

        public NavMeshExtension Extension;
    }
    internal class NavMeshExtensionsProvider : INavMeshExtensionsProvider
    {
        List<NavMeshExtensionMeta> _extensions = new List<NavMeshExtensionMeta>();
        static Comparer<NavMeshExtensionMeta> _comparer = Comparer<NavMeshExtensionMeta>.Create((x, y) => x.Order > y.Order ? 1 : x.Order < y.Order ? -1 : 0);
        public NavMeshExtension this[int index] => _extensions[index].Extension;

        public int Count => _extensions.Count;

        public void Add(NavMeshExtension extension, int order)
        {
            var meta = new NavMeshExtensionMeta(order, extension);
            var at = _extensions.BinarySearch(meta, _comparer);
            if (at < 0)
            {
                _extensions.Add(meta);
                _extensions.Sort(_comparer);
            }
            else
            {
                _extensions.Insert(at, meta);
            }
        }

        public void Remove(NavMeshExtension extension)
        {
            _extensions.RemoveAll(x => x.Extension = extension);
        }
    }
}
