using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using EmployeeBO;
using WcfServiceAPI;

namespace WcfServiceAPI
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [WebInvoke(Method = "GET",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Xml)]
        [OperationContract]
        List<EmpBO> GetData();

        
        [WebInvoke(Method = "GET",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        List<EmpBO> GetDataJson();

        [WebGet]
        [OperationContract]
        List<EmpBo1> DataCont();


        [WebGet]
        string DataCont1();

        [WebGet]
        [FaultContract(typeof(ExecClass))]
        [OperationContract]
        Object FaultCont();

        [WebGet]
        [OperationContract(Name ="Method1")]
        int OverloadindMethod(int param1, int param2);

        [WebGet]
        [OperationContract(Name = "Method2")]
        int OverloadindMethod(int param1, int param2, int param3);

    }

    [DataContract]
    public class EmpBo1
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int Age { get; set; }
        
        public DateTime JoiningDate { get; set; }
        
        public int Salary { get; set; }
    }
    

    [DataContract]
    public class ExecClass
    {
        [DataMember]
        public int ErrorCode { get; set; }
        [DataMember]
        public string ErrorMessage { get; set; }
    }


}
