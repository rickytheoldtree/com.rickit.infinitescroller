using UnityEngine;

namespace RicKit.InfiniteScroller
{
    public class InfiniteScrollerCellView : MonoBehaviour, IInfiniteScrollerCellView
    {
        public string cellIdentifier;
        public string CellIdentifier => cellIdentifier;
        public int CellIndex { get; set; }
        public int DataIndex { get; set; }
        public bool Active { get; set; }
        public GameObject GameObject => gameObject;
    }

    public interface IInfiniteScrollerCellView
    {
        public string CellIdentifier { get; }
        public int CellIndex { get; set; }
        public int DataIndex { get; set; }
        public bool Active { get; set; }
        public GameObject GameObject { get; }
        public virtual void RefreshCellView()
        {
        }
    }
}