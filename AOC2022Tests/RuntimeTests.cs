using AOC2022;
using AOC2022.Input;
using AOC2022.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AOC2022Tests
{
    [TestClass]
    public class RuntimeTests
    {
        [TestMethod]
        [DataRow(1, 1, true, 24000, DisplayName = "Day 1 Part 1 Test")]
        [DataRow(1, 1, false, 72718, DisplayName = "Day 1 Part 1 Actual")]
        [DataRow(1, 2, true, 45000, DisplayName = "Day 1 Part 2 Test")]
        [DataRow(1, 2, false, 213089, DisplayName = "Day 1 Part 2 Actual")]
        [DataRow(2, 1, true, 15, DisplayName = "Day 2 Part 1 Test")]
        [DataRow(2, 1, false, 11603, DisplayName = "Day 2 Part 1 Actual")]
        [DataRow(2, 2, true, 12, DisplayName = "Day 2 Part 2 Test")]
        [DataRow(2, 2, false, 12725, DisplayName = "Day 2 Part 2 Actual")]
        [DataRow(3, 1, true, 157, DisplayName = "Day 3 Part 1 Test")]
        [DataRow(3, 1, false, 8233, DisplayName = "Day 3 Part 1 Actual")]
        [DataRow(3, 2, true, 70, DisplayName = "Day 3 Part 2 Test")]
        [DataRow(3, 2, false, 2821, DisplayName = "Day 3 Part 2 Actual")]
        [DataRow(4, 1, true, 2, DisplayName = "Day 4 Part 1 Test")]
        [DataRow(4, 1, false, 464, DisplayName = "Day 4 Part 1 Actual")]
        [DataRow(4, 2, true, 4, DisplayName = "Day 4 Part 2 Test")]
        [DataRow(4, 2, false, 770, DisplayName = "Day 4 Part 2 Actual")]
        public async Task TestRun(int challange, int part, bool useTestFile, int expected)
        {
            var inputProvider = InputProvider.Create();
            var file = inputProvider.GetInputFile(challange, useTestFile);
            var options = new RuntimeOptions(challange, part, file);
            var runtime = new Runtime(options);

            var result = await runtime.Run();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccess(out var answer, out var error));
            Assert.IsTrue(answer.HasValue);
            Assert.IsNull(error);
            Assert.AreEqual(expected, answer.Value);
        }
    }
}