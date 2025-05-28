namespace RicKit.InfiniteScroller
{
    public interface IInfiniteScrollerDelegate
    {
        int GetNumberOfCells(InfiniteScroller scroller);
        
        float GetCellViewSize(InfiniteScroller scroller, int dataIndex);
        
        IInfiniteScrollerCellView GetCellView(InfiniteScroller scroller, int dataIndex, int cellIndex);
    }
}