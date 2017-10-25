using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaDesk2

{
    public class Desk : Object

    {
        public int width;
        public int depth;
        public int drawers;
        public DesktopMaterial material;

        public enum DesktopMaterial { LAMINATE = 100, OAK = 200, ROSEWOOD = 300, VENEER = 125, PINE = 50 };       
        public static int minWidth = 24;
        public static int maxWidth = 96;
        public static int minDepth = 24;
        public static int maxDepth = 96;
        


        public Desk(int width, int depth, int drawers, DesktopMaterial material)
        {
            this.width = width;
            this.depth = depth;
            this.drawers = drawers;
            this.material = (DesktopMaterial)material;
           
        }
        public string getMaterial()
        {
            string rv = "";

            switch (this.material)
            {
                case DesktopMaterial.LAMINATE:
                    rv = "Laminate";
                    break;

                case DesktopMaterial.OAK:
                    rv = "Oak";
                    break;

                case DesktopMaterial.ROSEWOOD:
                    rv = "Rosewood";
                    break;

                case DesktopMaterial.VENEER:
                    rv = "Veneer";
                    break;

                case DesktopMaterial.PINE:
                    rv = "Pine";
                    break;


            }
            return rv;
        }

        public static string validateWidth(int width)
        {
            if (width < minWidth)
            { return "Width too small"; }


            if (width > maxWidth)
            { return "Width too big"; }

            return "";

        }
        public static string validateDepth(int depth)
        {
            if (depth < minDepth)
            { return "Depth too small"; }


            if (depth > maxDepth)
            { return "Depth too big"; }

            return "";

        }
    }
}

