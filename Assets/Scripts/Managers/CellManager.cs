using System.Collections.Generic;

public class CellManager : Manager<CellManager> {

	List<CellBehaviour> cells = new List<CellBehaviour>();

    public void AddCell(CellBehaviour cell) {
        cells.Add(cell);
    }

    public void RemoveCell(CellBehaviour cell) {
        cells.Remove(cell);
    }

    public List<CellBehaviour> GetCells() {
        return new List<CellBehaviour>(cells);
    }
}
