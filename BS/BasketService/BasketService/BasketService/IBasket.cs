using BasketService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml.Linq;

namespace BasketService
{    
    [ServiceContractAttribute(SessionMode = SessionMode.Required)]
    public interface IBasket
    {
        [OperationContract(IsInitiating = true)]
        void Start();

        [OperationContract]
        XElement Search(string query);

        [OperationContract]
        bool AddItem(int id);

        [OperationContract]
        bool DeleteItem(int id);

        [OperationContract]
        XElement GetCurrentBasket();

        [OperationContract]
        int GetTotal();

        [OperationContract(IsTerminating = true)]
        void PayPurchase();
    }   
}
