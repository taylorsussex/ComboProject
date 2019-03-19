using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComboProject
{
    class ComboSimulator
    {
        public static int getComboDamage(string combo, List<LoadMoves.Attack> moves, double initScale, int initHit)
        {
            int hit = initHit;
            int damage = 0;
            double scale = initScale;

            string[] comboMoves = combo.Split(' ');

            for(int i = 0; i < comboMoves.Length; i++)
            {
                LoadMoves.Attack currentAttack = moves.Find(x => x.name == comboMoves[i]);

                for(int j = 0; j < currentAttack.hits.Length; j++)
                {
                    if (hit > 2)
                        scale *= 0.875;
                    if (currentAttack.hits[j] < 1000 && scale < 0.2)
                        scale = 0.2;
                    if (currentAttack.hits[j] >= 1000 && scale < 0.275)
                        scale = 0.275;

                    damage += Convert.ToInt32(Math.Floor(scale * currentAttack.hits[j]));

                    hit++;
                    if (hit >= scaling.Length)
                        hit = scaling.Length - 1;
                }
            }

            return damage;
        }

        public static int getComboDamage(string combo, List<LoadMoves.Attack> moves)
        {
            return getComboDamage(combo, moves, 1, 0);
        }

        private static double[] scaling = new double[] {1,1,1,.875,.766,.67,.586,.513,.449,.393,.344,.301,.263,.23,.201,.2};
    }
}
