using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TeleBajaUEA.RaceDataStructs
{
    [Serializable()]
    public struct RaceData : ISerializable
    {
        public List<FileSensorsData> DataList { get { return dataList; } }
        public RaceParameters Parameters { get { return parameters; } }

        private readonly List<FileSensorsData> dataList;
        private readonly RaceParameters parameters;

        public RaceData(List<FileSensorsData> pDataList, RaceParameters pParameters)
        {
            dataList = pDataList;
            parameters = pParameters;
        }

        public RaceData(SerializationInfo info, StreamingContext ctxt)
        {
            dataList = (List<FileSensorsData>)info.GetValue("DataList", typeof(List<FileSensorsData>));
            parameters = (RaceParameters)info.GetValue("Parameters", typeof(RaceParameters));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("DataList", dataList);
            info.AddValue("Parameters", parameters);
        }
    }
}
