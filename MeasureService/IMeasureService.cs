using System.ServiceModel;
using System.Collections.Generic;
using System;


namespace MeasureService
{

    [ServiceContract(CallbackContract = typeof(IMeasureServiceCallback))]
    public interface IMeasureService
    {
        [OperationContract(IsOneWay=true)]
        void Prijavi(int id);

        [OperationContract(IsOneWay = true)]
        void Odjavi(int id);

        [OperationContract]
        void DodajMerenje(int id,int value,string type);


        [OperationContract]
        String SvaMerenjaSaMeraca(int measurerId, DateTime from, DateTime until,int type);

        [OperationContract]
        String SviMomentiZaMerenjaLimit(int measurerId,int type, int limitType, double limit);

        [OperationContract]
        String SviTrenuciZaMerenjaSaLokacije(String locationName, int type, int limitType, double limit);

        [OperationContract]
        String ProsekLokacija(String locationName, int type, DateTime dateFrom, DateTime dateUntil);  
    }
}