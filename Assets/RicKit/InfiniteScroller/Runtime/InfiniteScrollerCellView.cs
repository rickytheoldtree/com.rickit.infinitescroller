using System;
using UnityEngine;

namespace RicKit.InfiniteScroller
{
    public class InfiniteScrollerCellView : MonoBehaviour
    {
        public string cellIdentifier;
        
        [NonSerialized]
        public int cellIndex;
        
        [NonSerialized]
        public int dataIndex;
        
        [NonSerialized]
        public bool active;

        public virtual void RefreshCellView() { }
    }
}