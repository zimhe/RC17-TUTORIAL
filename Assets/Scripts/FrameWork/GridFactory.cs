using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridFactory : GridFactoryBase<Grid>
{
    protected override Grid Create()
    {
        return new Grid();
    }
}
