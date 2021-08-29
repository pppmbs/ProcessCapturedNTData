using CsvHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCapturedNTData
{
    static class Constants
    {
        public const int TickCount = 2000; // ticks per bar
        public const int barsLookAhear = 5; // look ahead 5 bars
        //public const int minBarRecords = 50; //anything less Some indicators, e.g. SMA50, MACD, will not have any value
        //public const int slidingWindow = TickCount / 10; // sliding window to create multiple variations of the 2000 ticks bar records, i.e. expended the data size by 10x
        //public const int slidingWindow = 97; // largest prime <100 sliding window to create multiple variations of the 2000 ticks bar records, i.e. expended the data size by 10x
        public const int slidingTotal = 10; // total number of sliding, i.e. # of augmented files generated
    }

    class Program
    {
        static public void BuildBarRecords(List<BarRecord> records, List<BarRecord> barRecords, int slidingNum)
        {
            CultureInfo provider = new CultureInfo("en-US");

            BarRecord bar = new BarRecord();
            for (int i = 0; i < records.Count; i++)
            {
                double part1;
                double part2;
                DateTime time1;
                DateTime time2;
                long timePart1;
                long timePart2;

                //skip the last record
                if ((i + 1) == records.Count)
                    return;
                BarRecord preRec = records[i];
                BarRecord postRec = records[i + 1];

                time1 = DateTime.ParseExact(Convert.ToInt32(preRec.START_TIME).ToString("000000.##"), "HHmmss", provider);
                timePart1 = time1.Ticks * (Constants.slidingTotal - slidingNum) / Constants.slidingTotal;
                time2 = DateTime.ParseExact(Convert.ToInt32(postRec.START_TIME).ToString("000000.##"), "HHmmss", provider);
                timePart2 = time2.Ticks * slidingNum / Constants.slidingTotal;
                DateTime newStartTime = new DateTime(timePart1 + timePart2);
                bar.START_TIME = newStartTime.ToString("HHmmss");

                time1 = DateTime.ParseExact(Convert.ToInt32(preRec.END_TIME).ToString("000000.##"), "HHmmss", provider);
                timePart1 = time1.Ticks * (Constants.slidingTotal - slidingNum) / Constants.slidingTotal;
                time2 = DateTime.ParseExact(Convert.ToInt32(postRec.END_TIME).ToString("000000.##"), "HHmmss", provider);
                timePart2 = time2.Ticks * slidingNum / Constants.slidingTotal;
                DateTime newEndTime = new DateTime(timePart1 + timePart2);
                bar.END_TIME = newEndTime.ToString("HHmmss");

                part1 = Convert.ToDouble(preRec.OPEN_PRICE) * (Constants.slidingTotal - slidingNum) / Constants.slidingTotal;
                part2 = Convert.ToDouble(postRec.OPEN_PRICE) * slidingNum / Constants.slidingTotal;
                bar.OPEN_PRICE =  (part1 + part2).ToString();

                part1 = Convert.ToDouble(preRec.CLOSE_PRICE) * (Constants.slidingTotal - slidingNum) / Constants.slidingTotal;
                part2 = Convert.ToDouble(postRec.CLOSE_PRICE) * slidingNum / Constants.slidingTotal;
                bar.CLOSE_PRICE = (part1 + part2).ToString();

                part1 = Convert.ToDouble(preRec.HIGH_PRICE) * (Constants.slidingTotal - slidingNum) / Constants.slidingTotal;
                part2 = Convert.ToDouble(postRec.HIGH_PRICE) * slidingNum / Constants.slidingTotal;
                bar.HIGH_PRICE = (part1 + part2).ToString();

                part1 = Convert.ToDouble(preRec.LOW_PRICE) * (Constants.slidingTotal - slidingNum) / Constants.slidingTotal;
                part2 = Convert.ToDouble(postRec.LOW_PRICE) * slidingNum / Constants.slidingTotal;
                bar.LOW_PRICE = (part1 + part2).ToString();

                part1 = Convert.ToDouble(preRec.TOTAL_VOLUME) * (Constants.slidingTotal - slidingNum) / Constants.slidingTotal;
                part2 = Convert.ToDouble(postRec.TOTAL_VOLUME) * slidingNum / Constants.slidingTotal;
                bar.TOTAL_VOLUME = (part1 + part2).ToString();

                part1 = Convert.ToDouble(preRec.SMA9) * (Constants.slidingTotal - slidingNum) / Constants.slidingTotal;
                part2 = Convert.ToDouble(postRec.SMA9) * slidingNum / Constants.slidingTotal;
                bar.SMA9 = (part1 + part2).ToString();

                part1 = Convert.ToDouble(preRec.SMA20) * (Constants.slidingTotal - slidingNum) / Constants.slidingTotal;
                part2 = Convert.ToDouble(postRec.SMA20) * slidingNum / Constants.slidingTotal;
                bar.SMA20 = (part1 + part2).ToString();

                part1 = Convert.ToDouble(preRec.SMA50) * (Constants.slidingTotal - slidingNum) / Constants.slidingTotal;
                part2 = Convert.ToDouble(postRec.SMA50) * slidingNum / Constants.slidingTotal;
                bar.SMA50 = (part1 + part2).ToString();

                part1 = Convert.ToDouble(preRec.MACD_DIFF) * (Constants.slidingTotal - slidingNum) / Constants.slidingTotal;
                part2 = Convert.ToDouble(postRec.MACD_DIFF) * slidingNum / Constants.slidingTotal;
                bar.MACD_DIFF = (part1 + part2).ToString();

                part1 = Convert.ToDouble(preRec.RSI) * (Constants.slidingTotal - slidingNum) / Constants.slidingTotal;
                part2 = Convert.ToDouble(postRec.RSI) * slidingNum / Constants.slidingTotal;
                bar.RSI = (part1 + part2).ToString();

                part1 = Convert.ToDouble(preRec.BOLL_LOW) * (Constants.slidingTotal - slidingNum) / Constants.slidingTotal;
                part2 = Convert.ToDouble(postRec.BOLL_LOW) * slidingNum / Constants.slidingTotal;
                bar.BOLL_LOW = (part1 + part2).ToString();

                part1 = Convert.ToDouble(preRec.BOLL_HIGH) * (Constants.slidingTotal - slidingNum) / Constants.slidingTotal;
                part2 = Convert.ToDouble(postRec.BOLL_HIGH) * slidingNum / Constants.slidingTotal;
                bar.BOLL_HIGH = (part1 + part2).ToString();

                part1 = Convert.ToDouble(preRec.CCI) * (Constants.slidingTotal - slidingNum) / Constants.slidingTotal;
                part2 = Convert.ToDouble(postRec.CCI) * slidingNum / Constants.slidingTotal;
                bar.CCI = (part1 + part2).ToString();

                part1 = Convert.ToDouble(preRec.ATR_TrueHigh) * (Constants.slidingTotal - slidingNum) / Constants.slidingTotal;
                part2 = Convert.ToDouble(postRec.ATR_TrueHigh) * slidingNum / Constants.slidingTotal;
                bar.ATR_TrueHigh = (part1 + part2).ToString();

                part1 = Convert.ToDouble(preRec.ATR_TrueLow) * (Constants.slidingTotal - slidingNum) / Constants.slidingTotal;
                part2 = Convert.ToDouble(postRec.ATR_TrueLow) * slidingNum / Constants.slidingTotal;
                bar.ATR_TrueLow = (part1 + part2).ToString();

                part1 = Convert.ToDouble(preRec.Momentum) * (Constants.slidingTotal - slidingNum) / Constants.slidingTotal;
                part2 = Convert.ToDouble(postRec.Momentum) * slidingNum / Constants.slidingTotal;
                bar.Momentum = (part1 + part2).ToString();

                part1 = Convert.ToDouble(preRec.ADX_DIPositive) * (Constants.slidingTotal - slidingNum) / Constants.slidingTotal;
                part2 = Convert.ToDouble(postRec.ADX_DIPositive) * slidingNum / Constants.slidingTotal;
                bar.ADX_DIPositive = (part1 + part2).ToString();

                part1 = Convert.ToDouble(preRec.ADX_DINegative) * (Constants.slidingTotal - slidingNum) / Constants.slidingTotal;
                part2 = Convert.ToDouble(postRec.ADX_DINegative) * slidingNum / Constants.slidingTotal;
                bar.ADX_DINegative = (part1 + part2).ToString();

                part1 = Convert.ToDouble(preRec.VROC) * (Constants.slidingTotal - slidingNum) / Constants.slidingTotal;
                part2 = Convert.ToDouble(postRec.VROC) * slidingNum / Constants.slidingTotal;
                bar.VROC = (part1 + part2).ToString();

                barRecords.Add(bar);
                bar = new BarRecord();
            }
        }

        static public void BuildLookAhead5Bars(List<BarRecord> barRecords)
        {
            BarRecord[] barRecordArry = barRecords.ToArray();
            int index = 0;
            double lastClosePrice = 0.0;
            double lastOpenPrice = 0.0;
            foreach (BarRecord bar in barRecords)
            {
                if ((index + Constants.barsLookAhear) >= barRecordArry.Length)
                {
                    bar.NEXT_CLOSE_BAR1 = lastClosePrice.ToString();
                    bar.NEXT_CLOSE_BAR2 = lastClosePrice.ToString();
                    bar.NEXT_CLOSE_BAR3 = lastClosePrice.ToString();
                    bar.NEXT_CLOSE_BAR4 = lastClosePrice.ToString();
                    bar.NEXT_CLOSE_BAR5 = lastClosePrice.ToString();
                    bar.NEXT_OPEN_BAR1 = lastOpenPrice.ToString();
                    bar.NEXT_OPEN_BAR2 = lastOpenPrice.ToString();
                    bar.NEXT_OPEN_BAR3 = lastOpenPrice.ToString();
                    bar.NEXT_OPEN_BAR4 = lastOpenPrice.ToString();
                    bar.NEXT_OPEN_BAR5 = lastOpenPrice.ToString();

                }
                else
                {
                    bar.NEXT_CLOSE_BAR1 = barRecordArry[index + 1].CLOSE_PRICE;
                    bar.NEXT_CLOSE_BAR2 = barRecordArry[index + 2].CLOSE_PRICE;
                    bar.NEXT_CLOSE_BAR3 = barRecordArry[index + 3].CLOSE_PRICE;
                    bar.NEXT_CLOSE_BAR4 = barRecordArry[index + 4].CLOSE_PRICE;
                    bar.NEXT_CLOSE_BAR5 = barRecordArry[index + 5].CLOSE_PRICE;
                    bar.NEXT_OPEN_BAR1 = barRecordArry[index + 1].OPEN_PRICE;
                    bar.NEXT_OPEN_BAR2 = barRecordArry[index + 2].OPEN_PRICE;
                    bar.NEXT_OPEN_BAR3 = barRecordArry[index + 3].OPEN_PRICE;
                    bar.NEXT_OPEN_BAR4 = barRecordArry[index + 4].OPEN_PRICE;
                    bar.NEXT_OPEN_BAR5 = barRecordArry[index + 5].OPEN_PRICE;
                    lastClosePrice = Convert.ToDouble(bar.NEXT_CLOSE_BAR5);
                    lastOpenPrice = Convert.ToDouble(bar.NEXT_OPEN_BAR5);

                }
                index++;
            }
        }

        static void Main(string[] args)
        {
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(Directory.GetCurrentDirectory());
            foreach (string inFile in fileEntries)
            {
                using (var sr = new StreamReader(inFile))
                {
                    var reader = new CsvReader(sr, CultureInfo.InvariantCulture);

                    //CSVReader will now read the whole file into an enumerable
                    List<BarRecord> records = reader.GetRecords<BarRecord>().ToList();

                    for (int slidingNum = 0; slidingNum < Constants.slidingTotal; slidingNum++)
                    {
                        //Covert captured NT data into bar records
                        List<BarRecord> barRecords = new List<BarRecord>();

                        String outFile = Constants.TickCount + "-ticks\\" + Path.GetFileNameWithoutExtension(inFile) + "-" + Constants.TickCount + "-bar-" + slidingNum.ToString() + ".csv";
                        using (var sw = new StreamWriter(outFile))
                        {
                            var writer = new CsvWriter(sw, CultureInfo.InvariantCulture);

                            BuildBarRecords(records, barRecords, slidingNum);

                            //provide the lookahead bars
                            BuildLookAhead5Bars(barRecords);

                            //Write the entire contents of the CSV file into another
                            //Do not use WriteHeader as WriteRecords will have done that already.
                            writer.WriteRecords(barRecords);
                            writer.Flush();
                        }
                    }
                }
            }
        }
    }
}
