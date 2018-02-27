namespace HashCode_Pizza
{
    class PizzaSlice
    {
        public int RowMin { get; private set; }
        public int RowMax { get; private set; }
        public int ColumnMin { get; private set; }
        public int ColumnMax { get; private set; }

        public PizzaSlice(int rowMin, int rowMax, int columnMin, int columnMax)
        {
            this.RowMin = rowMin;
            this.ColumnMin = columnMin;
            this.ColumnMax = columnMax;
            this.RowMax = rowMax;
        }

        public int GetSize()
        {
            return (RowMax - RowMin + 1) * (ColumnMax - ColumnMin + 1);
        }

        public void RemoveSliceFromPlate(int[,] plate)
        {
            for (int r = RowMin; r <= RowMax; r++)
                for (int c = ColumnMin; c <= ColumnMax; c++)
                    plate[r, c] = 0;
        }
    }
}
