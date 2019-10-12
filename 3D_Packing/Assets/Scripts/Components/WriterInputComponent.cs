using UnityEngine;
using Packing_3D.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Packing_3D.Components
{
    public class WriterInputComponent : MonoBehaviour, IWriter
    {

        int Width;  
        int Hight;
        int Lenght;

        int FormatQuantity;
        List<Format> Formats;

        StringBuilder Id;
        System.Random random;

        public  void WriteFile()
        {

            Initializer();
            GenerateInput();

            using (System.IO.StreamWriter file = new System.IO.StreamWriter("./Assets/input.txt"))
            {
                file.WriteLine($"{Hight} {Width} {Lenght}");
                file.WriteLine($"{FormatQuantity}");
                for (int i = 0; i < FormatQuantity; ++i)
                {
                    int quantity = random.Next(1, 100);
                    file.WriteLine($"{quantity} {Formats[i].Id} {Formats[i].Size.x} {Formats[i].Size.y} {Formats[i].Size.z}");   
                }
            }
        }

        private void Initializer()
        {
            Id = new StringBuilder("A");
            Formats = new List<Format>();
            random = new System.Random();
        }



        public void GenerateInput()
        {
            Lenght = random.Next(1, 500);
            Hight = random.Next(1, Lenght);
            Width = random.Next(1, Hight);

            FormatQuantity = random.Next(1, 500);

           

            for (int i = 0; i < FormatQuantity; ++i)
            {
                Format format = new Format();
                format.Id = this.Id.ToString();
                Id = GenerateString(Id);

                int h, w, l;
                l = random.Next(1, this.Lenght);
                h = random.Next(1, Math.Min(l , this.Hight) );
                w = random.Next(1, Math.Min(h , this.Width) );

                format.Size = new Vector3(h, w, l);
     
                Formats.Add(format);
            }

           
        }

        private StringBuilder GenerateString(StringBuilder code)
        {

            if (code[code.Length - 1] == 'Z')
            {
                code[code.Length - 1] = 'A';
                bool add = true;
                for (int i = code.Length - 2; i >= 0; ++i)
                {
                    if (code[i] != 'Z')
                    {
                        add = false;
                        code[i] = (char)(code[i] + 1);
                        break;
                    }
                    else
                    {
                        code[i] = 'A';
                    }
                }
                if (add)
                    code.Append('A');
            }
            else
            {
                code[code.Length - 1] = (char)(code[code.Length - 1] + 1);
            }
            return code;
        }
    }
}