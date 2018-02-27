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
    }
}
