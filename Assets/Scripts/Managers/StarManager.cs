using System.Collections.Generic;

public class StarManager : Manager<StarManager> {
    public short Result { get; set; }

    //  borders in ascending order
    public void CalculateResult(short[] borders, int clicks) {
        for (short i  = 0; i < borders.Length; ++i)
            if (borders[i] > clicks) {
                Result = (short)(5 - i);
                break;
            }
    }
}
