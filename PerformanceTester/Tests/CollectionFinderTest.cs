using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using Bogus;

namespace PerformanceTester
{
    [SimpleJob(targetCount: 5)]
    [MemoryDiagnoser] 
    public class CollectionFinderTest
    {
        private IList<Item> _items;

        [Params(10, 100, 1000, 10000, 100000, 1000000, 10000000)]
        public int N;

        [GlobalSetup]
        public void Setup()
        {
            var idx = 0;
            var random = new Bogus.Randomizer();
            _items = new Faker<Item>()
                .RuleFor(x => x.Id, f => idx++)
                .RuleFor(x => x.Name, f => f.Lorem.Word())
                .Generate(N);
        }


        [Benchmark]
        public void ForLoopFindString()
        {
            for (var i = 0; i < _items.Count; i++)
            {
                if (_items[i].Name.Contains("aaa"))
                {
                    return;
                }
            }
        }

        /*
        [Benchmark]
        public void ForLoopFindInt()
        {
            for (var i = 0; i < _items.Count; i++)
            {
                if (_items[i].Id == _items.Count - 1)
                {
                    return;
                }
            }
        }
        */

        [Benchmark]
        public void ForEachLoopFindString()
        {
            foreach (var item in _items)
            {
                if (item.Name.Contains("aaa"))
                {
                    return;
                }
            }
        }

        /*
        [Benchmark]
        public void ForEachLoopFindInt()
        {
            foreach (var item in _items)
            {
                if (item.Id == _items.Count - 1)
                {
                    return;
                }
            }
        }
        */

        [Benchmark]
        public void LinqFirstOrDefaultFindString()
        {
            _items.FirstOrDefault(x => x.Name.Contains("aaa"));
        }

        /*
        [Benchmark]
        public void LinqFirstOrDefaultFindInt()
        {
            _items.FirstOrDefault(x => x.Id == _items.Count - 1);
        }
        */
    }
}