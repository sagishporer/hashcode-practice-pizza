using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCode_Pizza
{
    class PizzaSlice
    {
        public int RowMin;
        public int RowMax;
        public int ColumnMin;
        public int ColumnMax;

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
