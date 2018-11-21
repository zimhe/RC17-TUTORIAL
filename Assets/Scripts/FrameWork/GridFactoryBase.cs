using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GridFactoryBase <G> where G: IGrid
{
    protected abstract G Create();

    public G CreateGrid(int countX, int countY)
    {
        var g = Create();
       
        for (int y = 0; y < countY; y++)
        {
            for (int x = 0; x < countX; x++)
            {
                int i = x + y * countX;
                g.AddVertex(i);

                if(x>0)g.AddEdge(i,i-1);
                if(y>0)g.AddEdge(i,i-countX);
            }
        }
        return g;
    }

    public G CreateGrid3d(int countX, int countY, int countZ)
    {
        var g = Create();
        for (int z = 0; z < countZ; z++)
        {
            for (int y = 0; y < countY; y++)
            {
                for (int x = 0; x < countX; x++)
                {
                    int i = x + y * countX;
                    g.AddVertex(i);

                    if (x > 0) g.AddEdge(i, i - 1);
                    if (y > 0) g.AddEdge(i, i - countX);
                }
            }
        }

        return g;
    }

    public G CreateTriangleGrid(int countX, int countY)
    {
        var g = Create();
        int n = countX * countY;

        // add vertices
        for (int i = 0; i < n; i++)
            g.AddVertex(i);

        // add even row edges
        for (int y = 0; y < countY; y += 2)
        {
            for (int x = 0; x < countX; x++)
            {
                int i = x + y * countX;
                if (x > 0) g.AddEdge(i, i - 1); // x-1
                if (y > 0) g.AddEdge(i, i - countX); // y-1
                if (y > 0 && x > 0)
                {
                    g.AddEdge(i, i - countX - 1);// y-1, x-1
                }
            }
        }
        // add odd row edges
        for (int y = 1; y < countY; y += 2)
        {
            for (int x = 0; x < countX; x++)
            {
                int i = x + y * countX;
                if (x > 0) g.AddEdge(i, i - 1); // x-1
                if (y > 0) g.AddEdge(i, i - countX); // y-1
                if (y > 0 && x < countX - 1)
                {
                    g.AddEdge(i, i - countX + 1); // y-1, x+1
                }
            }
        }
        // add even row triangles
        for (int y = 0; y < countY; y += 2)
        {
            for (int x = 0; x < countX; x++)
            {
                int i = x + y * countX;
                if (y > 0 && x > 0)
                {
                    g.AddTriangle(i, i - countX - 1, i - 1);
                    g.AddTriangle(i, i - countX, i - countX - 1);
                }
            }
        }
        // add odd row triangles
        for (int y = 1; y < countY; y += 2)
        {
            for (int x = 0; x < countX; x++)
            {
                int i = x + y * countX;
                if (y > 0 && x < countX - 1)
                {
                    g.AddTriangle(i, i - countX + 1, i - countX);
                }
                if (y > 0 && x > 0)
                {
                    g.AddTriangle(i, i - countX, i - 1);
                }
            }
        }
        return g;
    }

    public G CreateTriangleGrid3D(int countX, int countY, int countZ)
    {
        var g = Create();

        int n = countX * countY*countZ;

        // add vertices
        for (int i = 0; i < n; i++)
            g.AddVertex(i);

        for (int y = 0; y < countY; y++)
        {
          
            for (int z = 0; z < countZ; z ++)
            {
                for (int x = 0; x < countX; x++)
                {
                    if (z % 2 == 0)
                    {
                        // add even row edges
                        int i = x + z * countX + y * countX * countZ;

                        if (z > 0 && x > 0) g.AddEdge(i, i - countX - 1); // z-1, x-1

                        if (z > 0) g.AddEdge(i, i - countX); // z-1

                        if (x > 0) g.AddEdge(i, i - 1); // x-1

                        if (y > 0) g.AddEdge(i, i - countX * countZ); //y-1
                    }
                    else
                    {
                        // add odd row edges
                        int i = x + z * countX + y * countX * countZ;

                        if (z > 0) g.AddEdge(i, i - countX); // z-1

                        if (z > 0 && x < countX - 1) g.AddEdge(i, i - countX + 1); // z-1, x+1

                        if (x > 0) g.AddEdge(i, i - 1); // x-1

                        if (y > 0) g.AddEdge(i, i - countX * countZ);//y-1
                    }
                }
            }

           
            for (int z = 1; z < countZ; z += 2)
            {
                for (int x = 0; x < countX; x++)
                {
                    
                }
            }
        }

        for (int y = 0; y < countY; y++)
        {
         
            for (int z = 0; z < countZ; z ++)
            {
                for (int x = 0; x < countX; x++)
                {
                    if (z % 2 == 0)
                    {
                        // add even row triangles
                        int i = x + z * countX + y * countX * countZ;
                        if (z > 0 && x > 0)
                        {
                            g.AddTriangle(i, i - countX - 1, i - 1);
                            g.AddTriangle(i, i - countX, i - countX - 1);
                        }
                    }
                    else
                    {
                        // add odd row triangles
                        int i = x + z * countX + y * countX * countZ;
                        if (z > 0 && x > 0)
                        {
                            g.AddTriangle(i, i - countX, i - 1);
                        }
                        if (z > 0 && x < countX - 1)
                        {
                            g.AddTriangle(i, i - countX + 1, i - countX);
                        }
                   
                    }
                }
            }
        }
        return g;
    }
}
