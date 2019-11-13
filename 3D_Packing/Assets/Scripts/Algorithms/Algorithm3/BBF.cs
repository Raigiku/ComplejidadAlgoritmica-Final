using Packing_3D.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Packing_3D.Algorithms.Algorithm3
{
    public class BBF
    {
        public Vector3 Size { get; set; }

        public List<Container> Containers { get; set; }
        public List<Block> Blocks { get; set; }

        public List<List<int>> hojas { get; set; }

        public BBF(Vector3 size, List<Block> blocks)
        {
            Size = size;
            Blocks = blocks;
            Containers = new List<Container>();
        }

        public void run()
        {

            //Pieces.Sort(new GFG());
            Containers.Add(new Container());
            Containers.Last().Size = Size;
            Containers.Last().Blocks = new List<Block>();
            while (!Algorithm())
            {
                //break;
                if (Blocks.Count() == 0)
                    break;


                Containers.Add(new Container());
                Containers.Last().Size = Size;
                Containers.Last().Blocks = new List<Block>();
                // if (resp.Count() == 0)
                //     break;

                // respT.Add(resp.ToList());

                //resp.Clear();



                // Table += 1;
            }
            /*Area = areaDespercidiada();
            Waste = Area;
            Waste /= (H * W);
            Waste /= Table;*/

            //imp();
            //return Table;
        }


        bool Algorithm()
        {
            hojas = new List<List<int>>();
            for (int i = 0; i < Size.y; ++i)
            {
                hojas.Add(new List<int>());
                for (int j = 0; j < Size.x; ++j)
                    hojas[i].Add(0);
            }


            long esp = 0;

            int refenceLine = (int)Size.z;

            bool inf = true;

            Tuple<int, int, int, int> gap = Tuple.Create(-1, -1, -1, -1);

            while (esp < Size.z * Size.x * Size.y && Blocks.Count() > 0)
            {

                gap = FindLowestGap();
                if (gap.Item1 == -1 && gap.Item2 == -1 && gap.Item3 == -1 && gap.Item4 == -1) break;
                int index = FindBestFittingItem(gap.Item4, gap.Item3, (int)Size.z - hojas[gap.Item1][gap.Item2]);
                if (index >= 0)
                {
                    if (inf)
                    {
                        refenceLine = (int)Size.z;
                        //refenceLine = Pieces[index].H + hojas[gap.Item1];
                        inf = false;
                    }

                    Blocks[index].Position = new Vector3(gap.Item2, gap.Item1, hojas[gap.Item1][gap.Item2]);

                    //agregar el bloque al contenerdor correspondiente y eliminarlo de la lista
                    Containers.Last().Blocks.Add(Blocks[index]);

                    FillSheets((int)Blocks[index].Size.z, (int)gap.Item1, (int)Blocks[index].Size.y, (int)gap.Item2, (int)Blocks[index].Size.x);
                    esp += (long)Blocks[index].Size.x * (long)Blocks[index].Size.y * (long)Blocks[index].Size.z;

                    Blocks.Remove(Blocks[index]);

                    //resp.Add(Tuple.Create(Pieces[index].Id + (Pieces[index].Quant - Pieces[index].Available).ToString(), gap.Item1, H - (hojas[gap.Item1] + Pieces[index].H), Pieces[index].Normal));




                    //init = 0;
                }
                else
                {
                    if (index == -1)
                    {
                        //int menor = Math.Min((gap.Item1 - 1 >= 0) ? (hojas[gap.Item1 - 1] - hojas[gap.Item1]) : H, (gap.Item2 + gap.Item1 < W) ? (hojas[gap.Item2 + gap.Item1] - hojas[gap.Item2 + gap.Item1 - 1]) : H);
                        int inc = menor(gap.Item1, gap.Item3, gap.Item2, gap.Item4);
                        FillSheets(inc, gap.Item1, gap.Item3, gap.Item2, gap.Item4);

                        esp += inc * gap.Item1 * gap.Item2;
                    }
                    else
                    {
                        if (!inf)
                        {
                            break;
                            refenceLine = (int)Size.z;
                            inf = true;
                        }

                    }
                }

            }

            return false;

        }


        Tuple<int, int, int, int> FindLowestGap()
        {
            int indexX = -1, indexY = -1, min = (int)Size.z;
            //bool inte = false;
            for (int i = 0; i < Size.y; ++i)
            {
                for (int k = 0; k < Size.x; k++)
                {
                    if (hojas[i][k] < min)
                    {
                        min = hojas[i][k];
                        indexY = i;
                        indexX = k;
                        //w = 1;
                        //inte = false;
                    }
                    //else
                    // {
                    // if (hojas[i][k] == min && min != W && !inte)
                    //     w++;
                    // else
                    // {
                    //     inte = true;
                    // }
                    //}
                }

            }
            //if (indexX == -1) index = init;
            Tuple<int, int> area;
            if (indexX != -1)
            {
                area = RectanguleArea(indexX, indexY);
                return Tuple.Create(indexY, indexX, area.Item2, area.Item1);
            }
            return Tuple.Create(-1, -1, -1, -1);
        }

        private Tuple<int, int> RectanguleArea(int x, int y)
        {
            int large = 1, heigth = 1;
            for (int i = x + 1; i < Size.x; ++i)
            {
                if (hojas[y][i] == hojas[y][x])
                    large++;
            }

            for (int i = y + 1; i < Size.y; ++i)
            {
                if (hojas[i][x] == hojas[y][x])
                    heigth++;
            }
            return Tuple.Create(large, heigth);
        }

        int FindBestFittingItem(int wmax, int lmax, int hmax)
        {
            int index = -1;

            for (int i = 0; i < Blocks.Count(); ++i)
            {
                if (Blocks[i].Size.x <= wmax && Blocks[i].Size.y <= lmax)
                {
                    if (Blocks[i].Size.z <= hmax)
                    {
                        //Pieces[i].Available--;
                        return i;
                    }

                }
                /*else
                {
                    if (Blocks[i].Size.z <= wmax && Blocks[i].Size.y <= lmax)
                    {
                        if (Blocks[i].Size.x <= hmax)
                        {
                            //Pieces[i].Available--;
                            int temp = Blocks[i].Size.z;
                            Blocks[i].Size.z = Blocks[i].Size.x;
                            Blocks[i].Size.x = temp;
                            //rotar segun corresponda
                            // if (Pieces[i].Normal == "G")
                            //     Pieces[i].Normal = "N";
                            // else
                            //     Pieces[i].Normal = "G";
                            return i;
                        }

                    }
                }*/
            }

            return index;
        }

        void FillSheets(int inc, int initY, int distY, int initX, int distX)
        {
            for (int k = initY; k < initY + distY; ++k)
            {
                for (int i = initX; i < initX + distX; ++i)
                {
                    hojas[k][i] += inc;
                }
            }
        }

        int menor(int initY, int distY, int initX, int distX)
        {
            int resp = (int)Size.z;
            for (int k = initX - 1; k < initX + distX + 1; ++k)
            {
                if (k >= 0 && k < Size.x)
                {
                    if (initY - 1 >= 0)
                        resp = Math.Min(resp, hojas[initY - 1][k]);
                    if (initY + 1 < Size.y)
                        resp = Math.Min(resp, hojas[initY + 1][k]);
                }

            }

            for (int i = initY; i < initY + distY; ++i)
            {
                if (i >= 0 && i < Size.y)
                {
                    if (initX >= 0)
                        resp = Math.Min(resp, hojas[i][initX]);
                    if (initX + distX - 1 < Size.x)
                        resp = Math.Min(resp, hojas[i][initX + distX - 1]);
                }

            }

            //resp = Math.Min(resp, hojas[initY][initX]);
            resp -= hojas[initY][initX];
            resp = Math.Max(resp, 1);
            return resp;
        }
    }
}
