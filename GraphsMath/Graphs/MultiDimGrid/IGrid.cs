namespace GraphsMath.Graphs.MultiDimGrid
{
    public interface IGrid<TGridItem, TCoordsType>
    {
        #region Properties        
        public int Rank { get; }

        public List<TCoordsType> Bounds { get; }

        public dynamic GridMatrix { get; }
        #endregion

        #region Methods
        TGridItem GetItem(List<TCoordsType> point);

        void SetItem(TGridItem item, List<TCoordsType> point);
        


        #endregion
    }
}