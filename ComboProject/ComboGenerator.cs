using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComboProject
{
    class ComboGenerator
    {
        public ComboGenerator(List<LoadMoves.Attack> _movelist, List<LoadMoves.Chain> _chains)
        {
            movelist = _movelist;
            chains = _chains;
            rnd = new Random();
        }

        public string generateRandomCombo(int initUndizzy)
        {
            IPS_STAGE = 2;
            OTG_USED = false;
            undizzy = initUndizzy;
            IPS_USED = new bool[19];
            string combo = "2LK 5MK";
            comboChains = new List<LoadMoves.Chain>();
            chainPath = new List<string> { "GROUND_CHAIN_L", "GROUND_CHAIN_M" };

            for (int i = 0; i < chains.Count; i++)
            {
                if (chains[i].combo == "2LK")
                    comboChains.Add(chains[i]);
            }

            for (int i = 0; i < chains.Count; i++)
            {
                if (chains[i].combo == "5MK")
                    comboChains.Add(chains[i]);
            }

            while(true)
            {
                int index = comboChains.Last().comeFrom.IndexOf(chainPath.Last());
                List<LoadMoves.Chain> options = getOptions(comboChains.Last().goTo[index]);

                if (options.Count == 0)
                    return combo;

                int r = rnd.Next(options.Count);

                comboChains.Add(options[r]);
                combo += " " + options[r].combo;
                chainPath.Add(options[r].comeFrom[0]);

                // Advance IPS stage if applicable
                if (IPS_STAGE < 4 && optionsNewChain[r])
                    IPS_STAGE++;

                if (IPS_STAGE > 2)
                {
                    for (int j = 0; j < comboChains.Last().comboAttack.Count; j++)
                    {
                        // Add undizzy
                        undizzy += comboChains.Last().comboAttack[j].undizzy;

                        // Add IPS tags
                        if (comboChains.Last().comboAttack[j].ips != LoadMoves.IPS_TAG.UNWATCHED)
                            IPS_USED[(int)comboChains.Last().comboAttack[j].ips] = true;
                    }
                }

                // Use OTG if applicable
                if (comboChains.Last().comeFrom.Contains("OTG"))
                {
                    OTG_USED = true;
                }
            }
        }

        public string generateRandomCombo()
        {
            return generateRandomCombo(0);
        }

        public string generateGeneticCombo(double[] genes, int initUndizzy)
        {
            IPS_STAGE = 2;
            OTG_USED = false;
            undizzy = initUndizzy;
            IPS_USED = new bool[19];
            string combo = "2LK 5MK";
            comboChains = new List<LoadMoves.Chain>();
            chainPath = new List<string> { "GROUND_CHAIN_L", "GROUND_CHAIN_M" };

            int currentGene = 0;

            string[] starter = combo.Split(' ');

            for (int i = 0; i < starter.Count(); i++)
            {
                for (int j = 0; j < chains.Count; j++)
                {
                    if (chains[j].combo == starter[i])
                        comboChains.Add(chains[j]);
                }
            }

            while (true)
            {
                int index = comboChains.Last().comeFrom.IndexOf(chainPath.Last());
                List<LoadMoves.Chain> options = getOptions(comboChains.Last().goTo[index]);

                if (options.Count == 0 || currentGene >= genes.Length)
                    return combo;

                int r = (int)Math.Floor(genes[currentGene] * options.Count);
                if (r == options.Count)
                    r--;

                comboChains.Add(options[r]);
                combo += " " + options[r].combo;
                chainPath.Add(options[r].comeFrom[0]);

                // Advance IPS stage if applicable
                if (IPS_STAGE < 4 && optionsNewChain[r])
                    IPS_STAGE++;

                if (IPS_STAGE > 2)
                {
                    for (int j = 0; j < comboChains.Last().comboAttack.Count; j++)
                    {
                        // Add undizzy
                        undizzy += comboChains.Last().comboAttack[j].undizzy;

                        // Add IPS tags
                        if (comboChains.Last().comboAttack[j].ips != LoadMoves.IPS_TAG.UNWATCHED)
                            IPS_USED[(int)comboChains.Last().comboAttack[j].ips] = true;
                    }
                }

                // Use OTG if applicable
                if (comboChains.Last().comeFrom.Contains("OTG"))
                {
                    OTG_USED = true;
                }

                currentGene++;
            }
        }

        public List<LoadMoves.Chain> getOptions(string[] goTo)
        {
            List<LoadMoves.Chain> options = new List<LoadMoves.Chain>();
            optionsNewChain = new List<bool>();

            for (int i = 0; i < chains.Count; i++)
            {
                LoadMoves.Chain currentChain = chains[i];
                List<int> indexRemain = new List<int>();

                for (int j = 0; j < currentChain.comeFrom.Count; j++)
                {
                    for (int k = 0; k < goTo.Length; k++)
                    {
                        if (currentChain.comeFrom[j] == goTo[k])
                        {
                            // Would this be a new chain if this was selected?
                            switch (goTo[k])
                            {
                                case "GROUND_CHAIN_M":
                                    newChain = !(chainPath.Last() == "GROUND_CHAIN_L");
                                    break;
                                case "GROUND_CHAIN_H":
                                    newChain = !(chainPath.Last() == "GROUND_CHAIN_L" || chainPath.Last() == "GROUND_CHAIN_M");
                                    break;
                                case "GROUND_SPECIAL":
                                case "H_LUGER":
                                case "SLIDE":
                                    newChain = !(chainPath.Last() == "GROUND_CHAIN_L" || chainPath.Last() == "GROUND_CHAIN_M" || chainPath.Last() == "GROUND_CHAIN_H");
                                    break;
                                default:
                                    newChain = true;
                                    break;
                            }

                            // Are we out of undizzy?
                            if (newChain && undizzy >= 240 && IPS_STAGE == 4)
                                continue;

                            // Check IPS
                            if (!(IPS_USED[(int)currentChain.comboAttack.First().ips] && newChain) || IPS_STAGE + Convert.ToInt32(newChain) < 4)
                             {
                                 // Check OTG
                                 if (!(currentChain.comeFrom[j].Contains("OTG") && OTG_USED))
                                 {
                                    if (indexRemain.Count == 0)
                                        optionsNewChain.Add(newChain);
                                    indexRemain.Add(j);
                                 }    
                             }
                        }
                    }
                }

                if (indexRemain.Count > 0)
                {
                    List<string> tempCF = new List<string>();
                    List<string[]> tempGT = new List<string[]>();

                    for (int j = 0; j < currentChain.comeFrom.Count; j++)
                    {
                        if (indexRemain.Contains(j))
                        {
                            tempCF.Add(currentChain.comeFrom[j]);
                            tempGT.Add(currentChain.goTo[j]);
                        }
                    }

                    for (int j = 0; j < tempCF.Count; j++)
                    {
                        currentChain.comeFrom = new List<string> { tempCF[j] };
                        currentChain.goTo = new List<string[]> { tempGT[j] };

                        options.Add(currentChain);

                        if (j != 0)
                            optionsNewChain.Add(optionsNewChain.Last());
                    }

                    
                }

            }

            return options;
        }

        public List<LoadMoves.Attack> movelist;
        private List<LoadMoves.Chain> chains;
        private List<LoadMoves.Chain> comboChains;
        private List<string> chainPath;
        private int IPS_STAGE;
        private bool[] IPS_USED = new bool[19];
        private bool OTG_USED;
        private bool newChain = false;
        private int undizzy = 0;
        private List<bool> optionsNewChain = new List<bool>();

        private static Random rnd;
    }
}
