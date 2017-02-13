using System.ServiceModel;
using System.Threading;
using System.Collections.Generic;
using System;
using System.Linq;

namespace MeasureService
{
    [ServiceBehavior(ConcurrencyMode=ConcurrencyMode.Reentrant)]
    public class MeasureService : IMeasureService
    {

        private static BazaMerenja db = new BazaMerenja();
        private static Dictionary<int, List<IMeasureServiceCallback>> observers = new Dictionary<int, List<IMeasureServiceCallback>>();
        public void Prijavi(int id)
        {
            IMeasureServiceCallback callbackChannel=null;
          
            callbackChannel = OperationContext.Current.GetCallbackChannel<IMeasureServiceCallback>();
            
            if (observers[id] == null)
            {
                observers[id] = new List<IMeasureServiceCallback>();
                Console.WriteLine("");
            }

            observers[id].Add(callbackChannel);


        }
        public void DodajMerenje(int id,int value,string type)
        {
            if (observers.ContainsKey(id))
            {

                foreach (IMeasureServiceCallback callbackChannel in observers[id])
                {
                    callbackChannel.NotifyOfMeasurement(id,value,type);
                }
            }
            else
            {
                observers.Add(id, new List<IMeasureServiceCallback>());
            }

            SnimiMerenje(id, value, type, DateTime.Now);
        }


        public void Odjavi(int id)
        {
            IMeasureServiceCallback callbackChannel = OperationContext.Current.GetCallbackChannel<IMeasureServiceCallback>();

            observers[id].Remove(callbackChannel);
        }

        private void SnimiMerenje(int id,double value,string  type, DateTime currentTime)
        {

            Measure m = new Measure();
            Station s = (from station in db.Stations.ToList() where station.Id == id select station).FirstOrDefault();
            
            m.Type = type;
            m.Value = value;
            m.Station = s;
            m.Time = currentTime;

            db.AddToMeasures(m);
            db.SaveChanges();
           
        }

        public String SvaMerenjaSaMeraca(int measurerId, DateTime fromDate, DateTime untilDate, int type)
        {
            String typeStr="";
            List<Measure> measurements = new List<Measure>();
            
            if (type==1)
            {
                typeStr = "Vlaznost";
                measurements = (from m in db.Measures
                          where m.Station.Id == measurerId &&
                                       m.Time > fromDate &&
                                       m.Time < untilDate &&
                                       m.Type.Equals(typeStr)
                          select m).ToList<Measure>();
            }
            else if (type == 2)
            {
                typeStr = "Temperatura";
                measurements = (from m in db.Measures
                          where m.Station.Id == measurerId &&
                                       m.Time > fromDate &&
                                       m.Time < untilDate &&
                                       m.Type.Equals(typeStr)
                           select m).ToList<Measure>();
            }
            else
            {
                if (type == 0)
                {
                    measurements= (from m in db.Measures
                              where m.Station.Id == measurerId &&
                                           m.Time > fromDate &&
                                           m.Time < untilDate
                                   select m).ToList<Measure>();
                }
            }

            
            String retval = "";
            for (var i = 0; i < measurements.Count; i++)
            {
                retval += " [Id: " + measurements[i].Id + " Vrsta : " + measurements[i].Type + " Vrednost : " + measurements[i].Value + " ]\n";

            }
            return retval;
        }

       


        public String SviMomentiZaMerenjaLimit(int measurerId,int type, int limitType, double limit)
        {
            List<Measure> measurements=new List<Measure>();
            String typeStr="";
            String retVal="";
            if (type==2)
            {
                typeStr="Temperatura";

            }
            else if(type==1)
            {
                typeStr="Vlaznost";
            }
           

            if (limitType==1)
            {
                Console.WriteLine("1");
                measurements = db.Measures.
                Where(m => m.Station.Id == measurerId
                        && m.Type.Equals(typeStr)
                        && m.Value < limit).ToList();
            }
            else if (limitType==2)
            {
                Console.WriteLine("2");
                measurements = db.Measures.
                Where(m => m.Station.Id == measurerId
                        && m.Type.Equals(typeStr)
                        && m.Value > limit).ToList();
            }

            foreach (Measure measure in measurements)
            {
                retVal += "[Id: " + measure.Id + " , " + "Vrsta: " + measure.Type + " , " + " Vrednost: " + measure.Value + " , " + "Vreme: " +  measure.Time +" ]\n";
            }

            return retVal;
        }

        public String SviTrenuciZaMerenjaSaLokacije(String locationName, int type, int limitType, double limit)
        {
            List<Measure> measurements = new List<Measure>();
            String typeStr = "";
            String retVal = "";
            
            if (type == 2)
            {
                typeStr = "Temperatura";

            }
            else if (type == 1)
            {
                typeStr = "Vlaznost";
            }


            if (limitType == 1)
            {
                Console.WriteLine("1");
                measurements = db.Measures.
                Where(m => m.Station.Location.Name.Equals(locationName)
                        && m.Type.Equals(typeStr)
                        && m.Value < limit).ToList();
            }
            else if (limitType == 2)
            {
                Console.WriteLine("2");
                measurements = db.Measures.
                Where(m => m.Station.Location.Name.Equals(locationName)
                        && m.Type.Equals(typeStr)
                        && m.Value > limit).ToList();
            }

            foreach (Measure measure in measurements)
            {
                retVal += "[Id: " + measure.Id + " , " + "Vrsta: " + measure.Type + " , " + " Vrednost: " + measure.Value + " , " + "Time: " + measure.Time + " ]\n";
            }
           
            return retVal;
        }


        public String ProsekLokacija(String locationName, int type,DateTime dateFrom,DateTime dateUntil)
        {
            List<Measure> measurements = new List<Measure>();
            String typeStr = "";
            if (type == 2)
            {
                typeStr = "Temperatura";

            }
            else if (type == 1){
            
                typeStr = "Vlaznost";
            }

            double AverageResult = db.Measures.Where(m => m.Station.Location.Name.Equals(locationName)
                                                                && m.Type.Equals(typeStr)
                                                                && m.Time>dateFrom
                                                                && m.Time<dateUntil)
                                                                .Average(m =>m.Value);

            return "[ Ime lokacije " + locationName + " , Vrsta: " + typeStr + " , " + "Prosek:" + AverageResult + " ]";
        }


   
    }
}