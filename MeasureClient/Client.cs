using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.Threading;

namespace MeasureClient
{

    [CallbackBehavior(UseSynchronizationContext=false)]
    [DataContract]
    class Client:ServiceReference1.IMeasureServiceCallback
    {
        private int id;
        private ServiceReference1.MeasureServiceClient client;
        public Client(int  id)
        {
            
            this.id=id;
            InstanceContext context = new InstanceContext(this);
            client = new ServiceReference1.MeasureServiceClient(context);
            PocniMerenje();
            
        }

        public int getId()
        {
            return id;
        }
      
        private void PocniMerenje()
        {
           int i=0;
            while(true)
            {
                i++;
                Thread.Sleep(1000);
                if (i%6==0)
                {
                    int vlaznost = new Random().Next(40, 90);
                    Console.WriteLine(" Merac (Id = " + this.id + ") : " + "Izmerena vlaznost:  " + vlaznost + " %. Slanje rezultata posmatracima.");
                    client.DodajMerenje(this.id, vlaznost, "Vlaznost");
                }
                int temp = new Random().Next(-10, 40);
                Console.WriteLine(" Merac (Id = " + this.id + ") : " + "Izmerena temperatura: " + temp + " C. Slanje rezultata posmatracima.");
                client.DodajMerenje(this.id, temp, "Temperatura");   
            }
        }

        public void NotifyOfMeasurement(int id, double value, string type)
        {
            throw new NotImplementedException();
        }
    }
}
