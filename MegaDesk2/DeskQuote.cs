using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaDesk2
{
    
    public class DeskQuote : Object
    {
       
        public enum RUSH {  THREE, FIVE, SEVEN, NO_RUSH };
        public enum DESKSIZE {UNDER_1K, UNDER_2K, OVER_2K, NO_SIZE };
        public Desk desk;
        public string name;
        public RUSH rush;
        public double price;
        public DateTime date;
        double price_per_sqin = 1.00;
        static double[,] rushPrices;


        
        public DeskQuote(string name, int rush, Desk desk)
        {
            this.name = name;
            this.desk = desk;
            this.rush = (RUSH)rush;
            getRushOrder();
            this.date = DateTime.Now;

            try {
                this.calculatePrice();
            }
            catch (Exception exception)
            {

            }


        }

        public string getRush()
        {
            return this.rush.ToString();

            //string rv = "";

            //switch (this.rush)
            //{
            //    case RUSH.NO_RUSH:
            //        rv = "No Rush";
            //        break;

            //    case RUSH.THREE:
            //        rv = "3 days";
            //        break;

            //    case RUSH.FIVE:
            //        rv = "5 Days";
            //        break;

            //    case RUSH.SEVEN:
            //        rv = "7 Days";
            //        break;

            //}
            //return rv;
        }
        public static void getRushOrder()
        {
            if (rushPrices == null)
            {
                string filepath = ".\\rushOrderPrices.txt";
                string[] lines = File.ReadAllLines(filepath);
                int lineIndex = 0;
                rushPrices = new double[(int) RUSH.NO_RUSH, (int) DESKSIZE.NO_SIZE];

                for (int daysIndex = 0; daysIndex < (int) RUSH.NO_RUSH; daysIndex++ )
                {
                    for (int sizeIndex = 0; sizeIndex < (int) DESKSIZE.NO_SIZE; sizeIndex++ )
                    {
                        double value = 0.0;
                        Double.TryParse(lines[lineIndex], out value);
                        lineIndex++;
                        rushPrices[daysIndex, sizeIndex] = value;
                    }
                }
            }

        }
       
        private void calculatePrice()
        {
            this.price = 200.0;
            this.price += 50 * desk.drawers;
            this.price += (double)this.desk.material;
           
            int surfacearea = desk.depth * desk.width;

            if (surfacearea > 1000)
            {
                this.price += surfacearea * this.price_per_sqin;
            }
          

            if (this.rush < DeskQuote.RUSH.NO_RUSH)
            {
                int daysIndex = (int)this.rush;
                int sizeIndex = -1;

                if (surfacearea < 1000)
                {

                    sizeIndex = (int)DeskQuote.DESKSIZE.UNDER_1K;
                }
                else if (surfacearea < 2000)
                {
                    sizeIndex = (int)DeskQuote.DESKSIZE.UNDER_2K;
                }
                else
                {
                    sizeIndex = (int)DeskQuote.DESKSIZE.OVER_2K;
                }
                this.price += rushPrices[daysIndex, sizeIndex];
            }

            

            

        }
    }

}
