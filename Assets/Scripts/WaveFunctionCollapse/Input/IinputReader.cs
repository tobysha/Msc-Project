using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WaveFunctionCollapse
{
    public interface IinputReader<T>
    {
        IValue<T>[][] ReadInputToGrid();
    }
}

