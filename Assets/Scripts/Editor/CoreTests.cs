using System.Linq;
using System.Reflection;
using Core;
using Data;
using NUnit.Framework;
using UnityEngine;

namespace Editor {
    public class CoreTests {
        private DataProvider _dataProvider;
        private DistributionSO _distribution;
        
        [SetUp]
        public void Setup() {
            _distribution = Resources.Load<DistributionSO>("Data/Distribution");
            _dataProvider = new DataProvider(_distribution);
        }
        
        [Test]
        public void DistributionTest() {
            var type = typeof(DataProvider);
            var method = type.GetMethod("CreateNewDistribution", BindingFlags.Instance | BindingFlags.NonPublic);
            method!.Invoke(_dataProvider, null);
            var field = type.GetField("_lineups", BindingFlags.Instance | BindingFlags.NonPublic);
            var lineups = field!.GetValue(_dataProvider) as Lineup[];

            foreach (var entry in _distribution.Entries) {
                var expected = entry.Weight;
                var actual = lineups!.Count(lineup => lineup.Equals(entry.Lineup));
                Assert.AreEqual(expected, actual);
            }
        }
    }
}