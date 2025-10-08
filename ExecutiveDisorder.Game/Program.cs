using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace ExecutiveDisorder.GameCli
{
    public class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                var repoRoot = FindRepoRoot();
                var dataDir = Path.Combine(repoRoot, "unity", "Assets", "Game", "Data");
                if (!Directory.Exists(dataDir))
                {
                    Console.Error.WriteLine($"Data directory not found: {dataDir}");
                    return 2;
                }

                var cards = ReadContainer<Card>(Path.Combine(dataDir, "cards.json"), "cards");
                var leaders = ReadContainer<Leader>(Path.Combine(dataDir, "leaders.json"), "leaders");
                var factions = ReadContainer<Faction>(Path.Combine(dataDir, "factions.json"), "factions");
                var crises = ReadContainer<Crisis>(Path.Combine(dataDir, "crises.json"), "crises");

                // Basic validations
                CheckUnique(cards.Select(c => c.id), "cards");
                CheckUnique(leaders.Select(l => l.id), "leaders");
                CheckUnique(factions.Select(f => f.id), "factions");
                CheckUnique(crises.Select(c => c.id), "crises");

                var cardIds = cards.Select(c => c.id).ToHashSet();
                foreach (var leader in leaders)
                {
                    foreach (var cid in leader.startingDeck)
                    {
                        if (!cardIds.Contains(cid))
                            Console.WriteLine($"WARN: Leader {leader.id} references missing card {cid}");
                    }
                }

                Console.WriteLine("=== Executive Disorder Content Summary ===");
                Console.WriteLine($"Cards:   {cards.Count}");
                Console.WriteLine($"Leaders: {leaders.Count}");
                Console.WriteLine($"Factions:{factions.Count}");
                Console.WriteLine($"Crises:  {crises.Count}");
                Console.WriteLine("OK");
                return 0;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return 1;
            }
        }

        private static string FindRepoRoot()
        {
            var dir = Directory.GetCurrentDirectory();
            while (dir != null && !File.Exists(Path.Combine(dir, "ExecutiveDisorder.sln")))
            {
                dir = Directory.GetParent(dir)?.FullName;
            }
            return dir ?? Directory.GetCurrentDirectory();
        }

        private static List<T> ReadContainer<T>(string path, string listKey)
        {
            if (!File.Exists(path)) return new List<T>();
            var json = File.ReadAllText(path);
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;
            if (!root.TryGetProperty(listKey, out var arr) || arr.ValueKind != JsonValueKind.Array) return new List<T>();
            var res = new List<T>();
            foreach (var el in arr.EnumerateArray())
            {
                var item = System.Text.Json.JsonSerializer.Deserialize<T>(el.GetRawText());
                if (item != null) res.Add(item);
            }
            return res;
        }

        private static void CheckUnique(IEnumerable<string> ids, string kind)
        {
            var set = new HashSet<string>();
            foreach (var id in ids)
            {
                if (!set.Add(id))
                {
                    Console.WriteLine($"WARN: Duplicate {kind} id: {id}");
                }
            }
        }

        public record Card(string id, string name, string description, int cost, string rarity);
        public record Leader(string id, string name, string bio, List<string> traitTags, List<string> startingDeck);
        public record Faction(string id, string name, string description, string color, List<string> tags);
        public record Crisis(string id, string name, string description, int severity);
    }
}

