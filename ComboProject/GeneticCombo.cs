using GeneticSharp.Domain;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Crossovers;
using GeneticSharp.Domain.Fitnesses;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Domain.Terminations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComboProject
{
    class GeneticCombo
    {
        public GeneticCombo(ComboGenerator _combogen, List<LoadMoves.Attack> _movelist)
        {
            combogen = _combogen;
            movelist = _movelist;
        }

        public void Start()
        {
            var w = new StreamWriter("C:\\Users\\Taylor\\source\\repos\\ComboProject\\ComboProject\\bin\\Debug\\genelog.csv");
            int gen = 1;

            // init chromosomes
            var chromosome = new FloatingPointChromosome(
                new double[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                new double[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                new int[] { 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20},
                new int[] { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5});

            // gen population
            var population = new Population(50, 100, chromosome);

            // fitness function
            var fitness = new FuncFitness((c) =>
            {
                var fc = c as FloatingPointChromosome;

                var genes = fc.ToFloatingPoints();

                string combo = combogen.generateGeneticCombo(genes, 0);

                int damage = ComboSimulator.getComboDamage(combo, movelist);

                w.WriteLine(string.Format("{0},{1},{2},,{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}", 
                    gen.ToString(),
                    combo,
                    damage.ToString(),
                    genes[0],
                    genes[1],
                    genes[2],
                    genes[3],
                    genes[4],
                    genes[5],
                    genes[6],
                    genes[7],
                    genes[8],
                    genes[9]));
                w.Flush();

                return damage;
            });

            var selection = new EliteSelection();

            var crossover = new UniformCrossover(0.5f);

            var mutation = new ReverseSequenceMutation();

            var termination = new FitnessStagnationTermination(5000);

            var ga = new GeneticAlgorithm(
                population,
                fitness,
                selection,
                crossover,
                mutation);

            ga.Termination = termination;
            ga.MutationProbability = 0.7f;

            Console.WriteLine("Generation: combo = damage");

            var latestFitness = 0.0;

            ga.GenerationRan += (sender, e) =>
            {
                var bestChromosome = ga.BestChromosome as FloatingPointChromosome;
                var bestFitness = bestChromosome.Fitness.Value;

                if (bestFitness != latestFitness)
                {
                    latestFitness = bestFitness;
                    string combo = combogen.generateGeneticCombo(bestChromosome.ToFloatingPoints(), 0);

                    Console.WriteLine("Generation {0,2}: {1} = {2}",
                            ga.GenerationsNumber,
                            combo,
                            bestFitness
                        );
                }

                gen++;
            };

            ga.Start();

            Console.ReadKey();
        }

        ComboGenerator combogen;
        private List<LoadMoves.Attack> movelist;
    }
}
