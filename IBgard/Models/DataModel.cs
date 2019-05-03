using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IBgard.Models
{
    public class Api
    {
        public DataModel Data()
        {
            var wc = new WebClient();
            var jsonString = wc.DownloadString("https://localhost:44312/ankush_11.txt");
            var data = JsonConvert.DeserializeObject<DataModel>(jsonString);

            return data;
        }

        public string Posture()
        {
            string postureData = "";

            foreach (var item in Data().GetIntervalsGoResult.Intervals)
            {
                if (item.IntervalScore == "straight")
                {
                    postureData += "2,";
                }

                if (item.IntervalScore == "slouch")
                {
                    postureData += "1,";
                }
            }

            return postureData.Remove(postureData.Length - 1, 1);
        }
    }

    public class DataModel
    {
        [JsonProperty("getIntervalsGoResult")]
        public GetIntervalsGoResult GetIntervalsGoResult { get; set; }
    }

    public partial class GetIntervalsGoResult
    {
        [JsonProperty("serverTimeUTC")]
        public DateTimeOffset ServerTimeUtc { get; set; }

        [JsonProperty("successIndication")]
        public SuccessIndication SuccessIndication { get; set; }

        [JsonProperty("intervalId")]
        public long IntervalId { get; set; }

        [JsonProperty("intervals")]
        public List<Interval> Intervals { get; set; }
    }

    public partial class Interval
    {
        [JsonProperty("beginTime")]
        public string BeginTime { get; set; }

        [JsonProperty("endTime")]
        public string EndTime { get; set; }

        [JsonProperty("intervalState")]
        public string IntervalState { get; set; }

        [JsonProperty("intervalScore")]
        public string IntervalScore { get; set; }

        [JsonProperty("accStraight")]
        public long AccStraight { get; set; }

        [JsonProperty("accSlouch")]
        public long AccSlouch { get; set; }

        [JsonProperty("numOfVibs")]
        public long NumOfVibs { get; set; }

        [JsonProperty("sensitivity")]
        public long Sensitivity { get; set; }

        [JsonProperty("movement")]
        public long Movement { get; set; }
    }

    public partial class SuccessIndication
    {
        [JsonProperty("errorCode")]
        public long ErrorCode { get; set; }

        [JsonProperty("longDesc")]
        public string LongDesc { get; set; }

        [JsonProperty("shortDesc")]
        public string ShortDesc { get; set; }
    }
}
