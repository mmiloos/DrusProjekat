using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace MeasureService
{
    public interface IMeasureServiceCallback
    {
        [OperationContract]
        void NotifyOfMeasurement(int id, double value, string type);
    }
}
