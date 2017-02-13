using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using MonitorClient.ServiceReference1;

namespace MonitorClient
{
    [CallbackBehavior(UseSynchronizationContext=false)]
    class MonitorClient : ServiceReference1.IMeasureServiceCallback
    {
        private ServiceReference1.MeasureServiceClient client;

        public MonitorClient()
        {
            InstanceContext context = new InstanceContext(this);
            client = new ServiceReference1.MeasureServiceClient(context);


            while (true)
            {
                Meni1();
                String odg = Console.ReadLine();
                int izbor = int.Parse(odg);
                if (izbor == 1)
                {
                    Meni2();
                    String odg2 = Console.ReadLine();
                    int izbor2 = int.Parse(odg2);
                    if (izbor2 ==1)
                    {
                        Console.WriteLine("Uneti ID meraca koji zelimo pratiti!");
                        String m = Console.ReadLine();
                        int mm = int.Parse(m);
                        client.Prijavi(mm);
                    }
                    else if(izbor2 ==2)
                    {
                        Console.WriteLine("Uneti ID meraca koji ne zelimo vise pratiti!");
                        String m = Console.ReadLine();
                        int mm = int.Parse(m);
                        client.Odjavi(mm);
                    }
                    else
                        Console.WriteLine("Pogresan izbor");
                }
                else if(izbor ==2)
                {
                    Meni3();
                    String odg3 = Console.ReadLine();
                    int izbor3 = int.Parse(odg3);
                    if (izbor3 == 1)
                    {
                        Console.WriteLine("Uneti Id meraca: ");

                        String stationId = Console.ReadLine();
                        int station = int.Parse(stationId);
                       
                        Console.WriteLine("Uneti vreme 'od kad' u formatu: [mm/dd/yyyy hh:mm:ss.stst]");

                        String dateFromStr = Console.ReadLine();
                        DateTime df = ConvertStringToDate(dateFromStr);
                        
                        Console.WriteLine("Uneti vreme 'do kad' u formatu: [mm/dd/yyyy hh:mm:ss.stst]");
                        String dateUntilStr = Console.ReadLine();
                        DateTime du = ConvertStringToDate(dateUntilStr);

                        PrikaziMerenjaSaStanice(station, df, du, 0); //0=sva merenja
                        continue;
                    }


                    else if (izbor3 == 2)
                    {
                        Console.WriteLine("Uneti Id meraca:");

                        String stationId = Console.ReadLine();
                        int station = int.Parse(stationId);
                       
                        Console.WriteLine("Uneti vreme 'od kad' u formatu: [mm/dd/yyyy hh:mm:ss.stst]");

                        String dateFromStr = Console.ReadLine();
                        DateTime df = ConvertStringToDate(dateFromStr);
                       
                        Console.WriteLine("Uneti vreme 'do kad' u formatu:[mm/dd/yyyy hh:mm:ss.stst]");
                        String dateUntilStr = Console.ReadLine();
                        DateTime du = ConvertStringToDate(dateUntilStr);


                        Console.WriteLine("Uneti tip merenja: \n 1=Vlaznost\n 2=Temperatura\n 0=Sva merenja");
                        String typeStr = Console.ReadLine();
                        int type = int.Parse(typeStr);

                        //type=1-vlaznost 2-temperatura
                        PrikaziMerenjaSaStanice(station, df, du, type);
                        continue;
                    }

                    else if (izbor3 == 3)
                    {
                        Console.WriteLine("Uneti Id meraca:");

                        String stationId = Console.ReadLine();
                        int station = int.Parse(stationId);
                       
                        Console.WriteLine("Uneti tip merenja: \n 1=Vlaznost\n 2=Temperatura");

                        String typeStr = Console.ReadLine();
                        int type = int.Parse(typeStr);

                        Console.WriteLine("Manje/Vece \n 1  Manje \n 2 Vece \n ");

                        String limitType = Console.ReadLine();
                        int limitSign = int.Parse(limitType);

                        Console.WriteLine("Limit? ");

                        String limitNumber = Console.ReadLine();

                        double limit = double.Parse(limitNumber);

                        PrikaziLimitSaStanice(station, type, limitSign, limit);
                        continue;

                    }
                    else if (izbor3 == 5)
                    {
                        Console.WriteLine("Uneti ime lokacije: ");

                        String location = Console.ReadLine();

                     
                        Console.WriteLine("Uneti tip merenja: \n 1=Vlaznost\n 2=Temperatura");

                        String typeStr = Console.ReadLine();
                        int type = int.Parse(typeStr);

                        Console.WriteLine("Manje/Vece \n 1 Manje \n 2 Vece \n ");

                        String limitType = Console.ReadLine();
                        int limitSign = int.Parse(limitType);

                        Console.WriteLine("Limit? ");

                        String limitNumber = Console.ReadLine();

                        double limit = double.Parse(limitNumber);

                        PrikaziLimitSaLokacije(location, type, limitSign, limit);
                        continue;

                    }

                    else if (izbor3 == 4)
                    {
                        Console.WriteLine("Uneti ime lokacije: ");
                        String location = Console.ReadLine();
                        Console.WriteLine("Uneti tip merenja: \n 1=Vlaznost\n 2=Temperatura");

                        String typeStr = Console.ReadLine();
                        int type = int.Parse(typeStr);

                        Console.WriteLine("Uneti vreme 'od kad' u formatu: [mm/dd/yyyy hh:mm:ss.stst]");
                        String dateFromStr = Console.ReadLine();
                        DateTime df = ConvertStringToDate(dateFromStr);
                     
                        Console.WriteLine("Uneti vreme 'do kad' u formatu:[mm/dd/yyyy hh:mm:ss.stst]");
                        String dateUntilStr = Console.ReadLine();
                        DateTime du = ConvertStringToDate(dateUntilStr);

                        PrikaziProsek(type, location, df, du);

                        continue;

                    }
                    else if (izbor3 == 0)
                        return;
                }
            }
        }


        public void NotifyOfMeasurement(int id, double value, string type)
        {
            PosmatracStatus("Merac [" + id + "] izmerena [" + type + " =" + value + "] ");
        }

        private void PrikaziMerenjaSaStanice(int stationId, DateTime dateFrom, DateTime dateUntil, int type)
        {
            Console.Write(client.SvaMerenjaSaMeraca(stationId, dateFrom, dateUntil, type));
        }



        private void PrikaziLimitSaStanice(int stationId, int type, int limitType, double limit)
        {
            Console.Write(client.SviMomentiZaMerenjaLimit(stationId, type, limitType, limit));
        }


        private void PrikaziLimitSaLokacije(String location, int type, int limitType, double limit)
        {
            Console.Write(client.SviTrenuciZaMerenjaSaLokacije(location, type, limitType, limit));
        }

        private void PrikaziProsek(int type, String locationName, DateTime dFrom, DateTime dUntil)
        {
            Console.Write(client.ProsekLokacija(locationName, type, dFrom, dUntil));
        }


        private void PosmatracStatus(string status)
        {
            Console.WriteLine("[Posmatrac] : " + status);
        }

        private DateTime ConvertStringToDate(String strDate)
        {
            DateTime dt = Convert.ToDateTime(strDate);
            return dt;
        }

        private void Meni1()
        {
            String str = "Izabrati opciju:\n";
            str += "1.Pracenje meraca online.\n";
            str += "2.Izvestaji.\n";
            Console.Write(str);
        }

        private void Meni2()
        {
            String str = "Izabrati opciju: \n";
            str += "1.Prijavi se na merac.\n";
            str += "2.Odjavi se sa meraca.\n";
            Console.Write(str);
        }

        private void Meni3()
        {
            String str = "Izvestaji : \n1-Izvestaj 1, Sva merenja sa odredjenog meraca u intervalu od-do.\n";
            str += "2-Izvestaj 2, Vlaznost ili Temperatura sa odredjenog meraca u intervalu od-do.\n";
            str += "3-Izvestaj 3, Svi trenuci kada je vrednost merenja bila iznad/ispod zadate granice.\n";
            str += "4-Izvestaj 4, Prosek temperature/vlaznosti za odredjeni period i lokaciju.\n";
            str += "5-Izvestaj 5, Svi trenuci kada je vrednost merenja bila iznad/ispod zadate granice na odredjenoj lokaciji.\n";
            str += "0-Izlaz\n";
            Console.Write(str);
        }
    }
}
