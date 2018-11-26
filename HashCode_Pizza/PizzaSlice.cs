using System.Collections.Generic;

namespace HashCode_Pizza
{
    class PizzaSlice
    {
        public int ID { get; private set; }
        public int RowMin { get; private set; }
        public int RowMax { get; private set; }
        public int ColumnMin { get; private set; }
        public int ColumnMax { get; private set; }

        public PizzaSlice(int id, int rowMin, int rowMax, int columnMin, int columnMax)
        {
            this.ID = id;
            this.RowMin = rowMin;
            this.ColumnMin = columnMin;
            this.ColumnMax = columnMax;
            this.RowMax = rowMax;
        }

        public int GetSize()
        {
            return (RowMax - RowMin + 1) * (ColumnMax - ColumnMin + 1);
        }

        public int CountIngredients(int[,] plate)
        {
            int count = 0;

            for (int r = RowMin; r <= RowMax; r++)
                for (int c = ColumnMin; c <= ColumnMax; c++)
                    if (plate[r, c] > 0)
                        count++;

            return count;
        }

        public Dictionary<int, int> GetSliceContent(int[,] plate)
        {
            Dictionary<int, int> ingredients = new Dictionary<int, int>();
            for (int r = RowMin; r <= RowMax; r++)
                for (int c = ColumnMin; c <= ColumnMax; c++)
                {
                    int key = plate[r, c];
                    int value;
                    if (ingredients.TryGetValue(key, out value) == false)
                        value = 0;

                    ingredients[key] = value + 1;
                }

            return ingredients;
        }

        public void RemoveSliceFromPlate(int[,] plate)
        {
            for (int r = RowMin; r <= RowMax; r++)
                for (int c = ColumnMin; c <= ColumnMax; c++)
                    plate[r, c] = this.ID;
        }

        public void RestoreSliceToPlate(int[,] plate, int[,] sourcePlace)
        {
            for (int r = RowMin; r <= RowMax; r++)
                for (int c = ColumnMin; c <= ColumnMax; c++)
                    plate[r, c] = sourcePlace[r, c];
        }

        public static int GetSlicesSize(ICollection<PizzaSlice> slices)
        {
            int size = 0;
            foreach (PizzaSlice slice in slices)
                size += slice.GetSize();

            return size;
        }

        public PizzaSlice BuildShirnkedSliceWithOverlapping(PizzaSlice newSlice)
        {
            // Verify overlap is rect
            if (
                // Partial column overlap
                ((newSlice.ColumnMin > this.ColumnMin) || (newSlice.ColumnMax < this.ColumnMax))
                &&
                // Partial row overlap
                ((newSlice.RowMin > this.RowMin) || (newSlice.RowMax < this.RowMax))
                )
                return null;

            // Calc new overlapping slice
            int existingNewRowMin = this.RowMin;
            int existingNewRowMax = this.RowMax;
            int existingNewColumnMin = this.ColumnMin;
            int existingNewColumnMax = this.ColumnMax;

            // Full column overlap
            if ((newSlice.ColumnMin <= this.ColumnMin) && (newSlice.ColumnMax >= this.ColumnMax))
            {
                // New slice in the middle of old slice
                if ((newSlice.RowMin > this.RowMin) && (newSlice.RowMax < this.RowMax))
                    return null;

                // Above
                if (newSlice.RowMin <= this.RowMin)
                    existingNewRowMin = newSlice.RowMax + 1;

                // Below
                if (newSlice.RowMax >= this.RowMax)
                    existingNewRowMax = newSlice.RowMin - 1;
            }

            // Full row overlap
            if ((newSlice.RowMin <= this.RowMin) && (newSlice.RowMax >= this.RowMax))
            {
                // New slice in the middle of old slice
                if ((newSlice.ColumnMin > this.ColumnMin) && (newSlice.ColumnMax < this.ColumnMax))
                    return null;

                // Left
                if (newSlice.ColumnMin <= this.ColumnMin)
                    existingNewColumnMin = newSlice.ColumnMax + 1;

                // Right
                if (newSlice.ColumnMax >= this.ColumnMax)
                    existingNewColumnMax = newSlice.ColumnMin - 1;
            }

            if (existingNewColumnMin > existingNewColumnMax)
                return null;
            if (existingNewRowMin > existingNewRowMax)
                return null;

            return new PizzaSlice(this.ID, existingNewRowMin, existingNewRowMax, existingNewColumnMin, existingNewColumnMax);
        }
    }
}
