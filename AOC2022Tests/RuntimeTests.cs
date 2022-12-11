using AOC2022;
using AOC2022.IO;
using AOC2022.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AOC2022Tests
{
    [TestClass]
    public class RuntimeTests
    {
        [TestMethod]
        [DataRow(1, 1, true, "24000", DisplayName = "Day 01 Part 1 Test")]
        [DataRow(1, 1, false, "72718", DisplayName = "Day 01 Part 1 Actual")]
        [DataRow(1, 2, true, "45000", DisplayName = "Day 01 Part 2 Test")]
        [DataRow(1, 2, false, "213089", DisplayName = "Day 01 Part 2 Actual")]

        [DataRow(2, 1, true, "15", DisplayName = "Day 02 Part 1 Test")]
        [DataRow(2, 1, false, "11603", DisplayName = "Day 02 Part 1 Actual")]
        [DataRow(2, 2, true, "12", DisplayName = "Day 02 Part 2 Test")]
        [DataRow(2, 2, false, "12725", DisplayName = "Day 02 Part 2 Actual")]

        [DataRow(3, 1, true, "157", DisplayName = "Day 03 Part 1 Test")]
        [DataRow(3, 1, false, "8233", DisplayName = "Day 03 Part 1 Actual")]
        [DataRow(3, 2, true, "70", DisplayName = "Day 03 Part 2 Test")]
        [DataRow(3, 2, false, "2821", DisplayName = "Day 03 Part 2 Actual")]

        [DataRow(4, 1, true, "2", DisplayName = "Day 04 Part 1 Test")]
        [DataRow(4, 1, false, "464", DisplayName = "Day 04 Part 1 Actual")]
        [DataRow(4, 2, true, "4", DisplayName = "Day 04 Part 2 Test")]
        [DataRow(4, 2, false, "770", DisplayName = "Day 04 Part 2 Actual")]

        [DataRow(5, 1, true, "CMZ", DisplayName = "Day 05 Part 1 Test")]
        [DataRow(5, 1, false, "WSFTMRHPP", DisplayName = "Day 05 Part 1 Actual")]
        [DataRow(5, 2, true, "MCD", DisplayName = "Day 05 Part 2 Test")]
        [DataRow(5, 2, false, "GSLCMFBRP", DisplayName = "Day 05 Part 2 Actual")]

        [DataRow(6, 1, true, "7", DisplayName = "Day 06 Part 1 Test")]
        [DataRow(6, 1, false, "1702", DisplayName = "Day 06 Part 1 Actual")]
        [DataRow(6, 2, true, "19", DisplayName = "Day 06 Part 2 Test")]
        [DataRow(6, 2, false, "3559", DisplayName = "Day 06 Part 2 Actual")]

        [DataRow(7, 1, true, "95437", DisplayName = "Day 07 Part 1 Test")]
        [DataRow(7, 1, false, "1077191", DisplayName = "Day 07 Part 1 Actual")]
        [DataRow(7, 2, true, "24933642", DisplayName = "Day 07 Part 2 Test")]
        [DataRow(7, 2, false, "5649896", DisplayName = "Day 07 Part 2 Actual")]

        [DataRow(8, 1, true, "21", DisplayName = "Day 08 Part 1 Test")]
        [DataRow(8, 1, false, "1851", DisplayName = "Day 08 Part 1 Actual")]
        [DataRow(8, 2, true, "8", DisplayName = "Day 08 Part 2 Test")]
        [DataRow(8, 2, false, "574080", DisplayName = "Day 08 Part 2 Actual")]

        [DataRow(9, 1, true, "88", DisplayName = "Day 09 Part 1 Test")]
        [DataRow(9, 1, false, "5513", DisplayName = "Day 09 Part 1 Actual")]
        [DataRow(9, 2, true, "36", DisplayName = "Day 09 Part 2 Test")]
        [DataRow(9, 2, false, "2427", DisplayName = "Day 09 Part 2 Actual")]

        [DataRow(10, 1, true, "13140", DisplayName = "Day 10 Part 1 Test")]
        [DataRow(10, 1, false, "15880", DisplayName = "Day 10 Part 1 Actual")]
        [DataRow(10, 2, true, "##  ##  ##  ##  ##  ##  ##  ##  ##  ##  \n###   ###   ###   ###   ###   ###   ### \n####    ####    ####    ####    ####    \n#####     #####     #####     #####     \n######      ######      ######      ####\n#######       #######       #######     ", DisplayName = "Day 10 Part 2 Test")]
        [DataRow(10, 2, false, "###  #     ##  #### #  #  ##  ####  ##  \n#  # #    #  # #    # #  #  #    # #  # \n#  # #    #    ###  ##   #  #   #  #    \n###  #    # ## #    # #  ####  #   # ## \n#    #    #  # #    # #  #  # #    #  # \n#    ####  ### #    #  # #  # ####  ### ", DisplayName = "Day 10 Part 2 Actual")]

        [DataRow(11, 1, true, "10605", DisplayName = "Day 11 Part 1 Test")]
        [DataRow(11, 1, false, "99840", DisplayName = "Day 11 Part 1 Actual")]
        [DataRow(11, 2, true, "2713310158", DisplayName = "Day 11 Part 2 Test")]
        [DataRow(11, 2, false, "", DisplayName = "Day 11 Part 2 Actual")]
        public async Task TestRun(int challange, int part, bool useTestFile, string expected)
        {
            var inputProvider = InputProvider.Create();
            var file = inputProvider.GetInputFile(challange, useTestFile);
            var options = new RuntimeOptions(challange, part, file);
            var runtime = new Runtime(options);

            var result = await runtime.Run();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccess(out var answer, out var error));
            Assert.IsNotNull(answer);
            Assert.IsNull(error);
            Assert.AreEqual(expected, answer);
        }
    }
}