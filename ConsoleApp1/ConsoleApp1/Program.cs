using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //var caps = new[]
            //{
            //    "Authorization checks use Auth0 and JWT ( component-integration )                              ",
            //    "Endpoints are Secured ( security-feature )                                                    ",
            //    "LayerDefinition has a persistent store ( persistent-sql-Spefeature )                          ",
            //    "REST API can create a new Layer Definition ( rest-api-feature )                               ",
            //    "REST API can get Layer Definition details ( rest-api-feature )                                ",
            //    "REST API can list all Layer Definitions ( rest-api-feature )                                  ",
            //    "REST API can deactivate a Layer Definition ( rest-api-feature )                               ",
            //    "REST API can update a Layer Definition ( rest-api-feature )                                   ",
            //    "REST API can get details for a specific Layer Definition version(rest - api - feature)        ",
            //    "REST API can list all versions for a Layer Definition(rest - api - feature)                   ",
            //};

            //var col = caps.Select(c => c.Split('('));
            //foreach (var ss in col)
            //{
            //    Console.WriteLine(ss[1].Replace(")", "").Trim());
            //}

            var dir = "F:\\Repos\\l1-v2-specs";
            Do2(dir);

            var ordered = _counter.OrderByDescending(kv => kv.Value).Take(20);
            foreach (var keyValuePair in ordered)
            {
                Console.WriteLine(keyValuePair.Key + " " + keyValuePair.Value);
            }
        }

        private static Dictionary<string, int> _counter = new Dictionary<string, int>();

        private static void Do2(string dir)
        {
            //Console.WriteLine(dir);
            //var dirName = Path.GetDirectoryName(dir);
            //Console.WriteLine(dirName);

            foreach (var file in Directory.GetFiles(dir))
            {
                if (file.Contains("Capabilities.yaml"))
                {
                    var lines = File.ReadAllLines(file);
                    foreach (var line in lines)
                    {
                        if (!line.Trim().StartsWith("#") && line.Contains("!{include"))
                        {
                            //Console.WriteLine(line);
                            var split = line.Split(' ');
                            foreach (var s in split)
                            {
                                if (s.Contains("yaml}"))
                                {
                                    var s2 = s.Replace("}", string.Empty);
                                    var path = Path.Combine(dir, s2);
                                    //Console.WriteLine(path);

                                    if (!File.Exists(path))
                                    {
                                        //Console.WriteLine("File not found: " + path);
                                        continue;
                                    }
                                   
                                    var yamlLines = File.ReadAllLines(path);
                                    foreach (var yline in yamlLines)
                                    {
                                        if (yline.Contains("Type:"))
                                        {
                                            //Console.WriteLine(line);
                                            var trim = yline.Replace("Type:", string.Empty).Trim();
                                            if (_counter.ContainsKey(trim))
                                            {
                                                _counter[trim] = _counter[trim] + 1;
                                            }
                                            else
                                            {
                                                _counter.Add(trim, 1);
                                            }

                                            if (trim == "logical-model")
                                            {
                                                Console.WriteLine(path);
                                            }

                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    break;
                }
            }

            foreach (var directory in Directory.GetDirectories(dir))
            {
                Do2(directory);
            }
        }
    }
}
