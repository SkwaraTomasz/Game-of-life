using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameOfLife
{
    public class Komorka : Form1
    {
        int x;
        int y;
        bool stan;

        public Komorka(bool stan, int x, int y) {
            this.stan = stan;
            this.x = x;
            this.y = y;
        }
        public Komorka(Komorka druga) {
            this.x = druga.x;
            this.y = druga.y;
            this.stan = druga.stan;
        
        }



        public bool getStan() {
            return this.stan;
        }

        public void setStan(bool stan) {
            this.stan = stan;
        }

        public int getX() {
            return this.x;
        }

        public int getY() {
            return this.y;
        }


    }
}
