using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using KnapsackSolver.Algorithms;
using KnapsackSolver.Algorithms.BranchAndBound;

namespace KnapsackSolver
{
    /// <summary>
    /// Main host
    /// </summary>
    class KnapsackSolver
    {
        /// <summary>
        /// Starting point
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // check if parameter passed
            if (args.Length < 1)
            {
                return;
            }

            // gets the filename
            var filename = args[0].Split('=')[1];

            // input variable
            KnapsackIntegralInput input = null;

            // the items
            var items = new List<KnapsackItem<int, int>>();

            // open file for reading
            using (var reader = new StreamReader(filename))
            {
                // get first line
                var metaLine = reader.ReadLine();

                // check for null
                if (metaLine == null)
                {
                    return;
                }

                // split
                var meta = metaLine.Split(' ');

                // get metadata
                var capacity = int.Parse(meta[1]);
                var variables = int.Parse(meta[0]);

                var index = 0;
                // read rest lines
                while (true)
                {
                    // read and check
                    var readLine = reader.ReadLine();
                    if (readLine == null)
                    {
                        break;
                    }

                    // the data
                    var data = readLine.Split(' ');

                    // get values
                    var v = int.Parse(data[0]);
                    var w = int.Parse(data[1]);

                    items.Add(new KnapsackItem<int, int>(w, v, index++));
                }

                if (items.Count != variables)
                {
                    return;
                }

                // create input
                input = new KnapsackIntegralInput
                (
                    capacity, 
                    items
                );
            }

            // if we need to use dp approach
            var shouldUseDynamic = input.RatioDifference() < 0.00001;

            // get output 
            var output = shouldUseDynamic
                ? new KnapsackDynamicAlgorithm(input).Execute()
                : new KnapsackBranchAndBoundAlgorithm(input).Execute();

            // write output
            Console.WriteLine("{0} {1}", output.Objective, output.Optimal);
            Console.WriteLine(string.Join(" ", output.VariableValues));

        }
    }
}
