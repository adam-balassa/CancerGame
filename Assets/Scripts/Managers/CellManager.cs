using System.Collections.Generic;
using UnityEngine.Events;
using System;

public class CellManager : Manager<CellManager> {
    public OnCellRemoved OnCellRemovedEvent;
    public OnCellAdded OnCellAddedEvent;
	List<CellBehaviour> cells = new List<CellBehaviour>();

    public void AddCell(CellBehaviour cell) {
        cells.Add(cell);
        OnCellAddedEvent.Invoke(cell);
    }

    public void RemoveCell(CellBehaviour cell) {
        cells.Remove(cell);
        OnCellRemovedEvent.Invoke(cell);
    }

    public List<CellBehaviour> GetCells() {
        return new List<CellBehaviour>(cells);
    }
    [Serializable]
    public class OnCellRemoved : UnityEvent<CellBehaviour>{}

    [Serializable]
    public class OnCellAdded : UnityEvent<CellBehaviour>{}
}
