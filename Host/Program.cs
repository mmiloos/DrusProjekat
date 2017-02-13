﻿using System.ServiceModel;
using System;


namespace Host
{
    class Program
    {
        public static void Main()
        {
                ServiceHost host = new ServiceHost(typeof(MeasureService.MeasureService));
            
                host.Open();
                Console.WriteLine("Host started " + DateTime.Now.ToString());
                Console.ReadLine();
                
            
        }

      
    }
}