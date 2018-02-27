using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCode_Pizza
{
    class PizzaPlate
    {
        private const int CHECK_SLICE_VALID = 0;
        private const int CHECK_SLICE_TOO_LOW = 1;
        private const int CHECK_SLICE_INVALID_SLICE = 2;
        private const int CHECK_SLICE_TOO_BIG = 3;

        private int mColumns;
        private int mRows;

        private int mMinIngPerSlice;
        private int mMaxSliceSize;

        private int[,] mPlate;

        public PizzaPlate(int rows, int columns, int[,] plate, int minIng, int maxSliceSize)
        {
            mRows = rows;
            mColumns = columns;
            mPlate = plate;
            mMinIngPerSlice = minIng;
            mMaxSliceSize = maxSliceSize;
        }

        public List<PizzaSlice> PerformSlice()
        {
            List<PizzaSlice> slices = new List<PizzaSlice>();
            int nextSliceId = -1;
            //int nextRow = -1;
            //int nextColumn = -1;
            int[,] plate = (int[,])mPlate.Clone();

            for (int r = 0; r < mRows; r++)
            {
                for (int c = 0; c < mColumns; c++)
                {
                    if (mPlate[r, c] > 0)
                    {
                        PizzaSlice maxSlice = GetMaxSliceAt(plate, r, c, nextSliceId);
                        if (maxSlice != null)
                        {
                            maxSlice.RemoveSliceFromPlate(plate);
                            slices.Add(maxSlice);
                            nextSliceId--;
                        }
                    }
                }
            }

            return slices;
        }

        public PizzaSlice GetMaxSliceAt(int[,] plate, int row, int column, int nextSliceId)
        {
            PizzaSlice maxSlice = null;

            for (int r = row; r < mRows; r++)
            {
                for (int c = column; c < mColumns; c++)
                {
                    int isValidSlice = IsValidSlice(plate, row, r, column, c);
                    if ((isValidSlice == CHECK_SLICE_TOO_BIG) || (isValidSlice == CHECK_SLICE_INVALID_SLICE))
                        break;

                    if (isValidSlice == CHECK_SLICE_VALID)
                    {
                        PizzaSlice newSlice = new PizzaSlice(nextSliceId, row, r, column, c);
                        if (maxSlice == null)
                            maxSlice = newSlice;
                        else if (maxSlice.GetSize() < newSlice.GetSize())
                            maxSlice = newSlice;
                    }
                }
            }

            return maxSlice;
        }

        public int IsValidSlice(int[,] plate, int minRow, int maxRow, int minCol, int maxCol)
        {
            int count1 = 0;
            int count2 = 0;

            if ((maxRow - minRow + 1)*(maxCol - minCol + 1) > mMaxSliceSize)
                return CHECK_SLICE_TOO_BIG;

            for (int r = minRow; r <= maxRow; r++)
            {
                for (int c = minCol; c <= maxCol; c++)
                {
                    int plateVal = plate[r, c];
                    if (plateVal <= 0)
                        return CHECK_SLICE_INVALID_SLICE;
                    else if (plateVal == 1)
                        count1++;
                    else if (plateVal == 2)
                        count2++;
                    else
                        throw new Exception("Valid plate value: " + plateVal);
                }
            }

            if ((count1 < this.mMinIngPerSlice) || (count2 < this.mMinIngPerSlice))
                return CHECK_SLICE_TOO_LOW;

            return CHECK_SLICE_VALID;
        }
    }
}
