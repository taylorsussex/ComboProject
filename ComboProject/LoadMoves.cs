using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ComboProject
{
    class LoadMoves
    {
        public static List<Attack> getMovelist(string file)
        {
            List<Attack> moves = new List<Attack>();

            try
            {
                using (var reader = new StreamReader(file))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        line = Regex.Replace(line, ",+", ",").Trim(',');
                        string[] values = line.Split(',');

                        int[] hits = Array.ConvertAll(values.Skip(3).ToArray(), int.Parse);
                        int undizzy = Convert.ToInt32(values[1]);
                        Enum.TryParse(values[2], out IPS_TAG ips);

                        moves.Add(new Attack(values[0], hits, ips, undizzy));
                        values = null;
                    }
                }
            }
            catch (IOException)
            {
            }

            return moves;
        }

        public static List<Chain> getChains(string file, List<Attack> moves)
        {
            List<Chain> chains = new List<Chain>();

            try
            {
                using (var reader = new StreamReader(file))
                {
                    // Declare variables that we will use to construct Chains
                    string combo = null;
                    string[] comboMoves = null;
                    List<Attack> comboAttack = null;
                    List<IPS_TAG> ips = null;
                    List<string> comeFrom = new List<string>();
                    List<string[]> goTo = new List<string[]>();
                    Chain chain;

                    int numLines = 0;
                    int duplicate = 0;

                    while (!reader.EndOfStream)
                    {
                        numLines++;
                        var line = reader.ReadLine();
                        line = line[0].ToString() + Regex.Replace(line.Substring(1), ",+", ",").Trim(',');
                        string[] values = line.Split(',');                       

                        if (values[0] != "")
                        {
                            if(combo != null)
                            {
                                // Add chain to list
                                chain = new Chain(combo, comboAttack, ips, comeFrom, goTo);
                                chains.Add(chain);
                            }
                            
                            // Reset variables
                            combo = values[0];
                            comboMoves = combo.Split(' ');
                            comboAttack = new List<Attack>();
                            ips = new List<IPS_TAG>();
                            comeFrom = new List<string>();
                            goTo = new List<string[]>();

                            // Get combo/attack list/ips list
                            for (int i = 0; i < comboMoves.Length; i++)
                            {
                                Attack currentAttack = moves.Find(x => x.name == comboMoves[i]);
                                ips.Add(currentAttack.ips);
                                comboAttack.Add(currentAttack);
                            }
                        }
                        
                        // Add come from/go to information
                        if (numLines % 2 == 1)
                        {
                            // Come from
                            for (duplicate = 1; duplicate < values.Length; duplicate++)
                                comeFrom.Add(values[duplicate]);
                        }
                        else
                        {
                            // Go to
                            for (int i = 0; i < duplicate - 1; i++)
                                goTo.Add(values.Skip(1).ToArray());
                        }
                        
                        values = null;
                    }
                    // Add last chain in file
                    chain = new Chain(combo, comboAttack, ips, comeFrom, goTo);
                    chains.Add(chain);
                }
            }
            catch (IOException)
            {
            }

            return chains;
        }

        public struct Attack
        {
            public Attack(string _name, int[] _hits, IPS_TAG _ips, int _undizzy)
            {
                name = _name;
                hits = _hits;
                ips = _ips;
                undizzy = _undizzy;
            }

            public string name { get; private set; }
            public int[] hits { get; private set; }
            public IPS_TAG ips { get; private set; }
            public int undizzy { get; private set; }
        }

        public struct Chain
        {
            public Chain(string _combo, List<Attack> _comboAttack, List<IPS_TAG> _ips, List<string> _comeFrom, List<string[]> _goTo)
            {
                combo = _combo;
                comboAttack = _comboAttack;
                ips = _ips;
                comeFrom = _comeFrom;
                goTo = _goTo;
            }

            public override string ToString()
            {
                return combo;
            }

            public string combo { get; private set; }
            public List<Attack> comboAttack { get; private set; }
            public List<IPS_TAG> ips { get; private set; }
            public List<string> comeFrom { get; set; }
            public List<string[]> goTo { get; set; }
        }

        public enum IPS_TAG
        {
            LP,
            MP,
            HP,
            LK,
            MK,
            HK,
            LP_AIR,
            LK_AIR,
            MP_AIR,
            MK_AIR,
            HP_AIR,
            HK_AIR,
            SPECIAL1,
            SPECIAL2,
            SPECIAL3,
            SPECIAL4,
            SPECIAL5,
            SPECIAL6,
            UNWATCHED
        }
    }
}
