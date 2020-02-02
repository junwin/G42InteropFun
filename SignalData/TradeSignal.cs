//using System.ServiceModel;
using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace SignalData
{
    [DataContract]
    [JsonObject(MemberSerialization.OptIn)]
    public class TradeSignal : ITradeSignal
    {
        /// <summary>
        /// Type of signal
        /// </summary>
        private TradeSignalType m_SignalType;

        private string m_Identity;
        private string m_StrategyID = "";
        private string m_Name;
        private string m_Origin = "";
        private string m_OrdType;
        private string m_Mnemonic = "";

        private TradeSignalStatus m_Status = TradeSignalStatus.undefined;

        /// <summary>
        /// Time we created signal
        /// </summary>
        private DateTime m_TimeCreated;

        /// <summary>
        /// Time that the signal is valid in milli seconds
        /// </summary>
        private long m_TimeValid = long.MaxValue;

        /// <summary>
        /// Is the signal set
        /// </summary>
        private bool m_Set;

        /// <summary>
        /// Side of order associated with this signal
        /// </summary>
        private string m_Side;

        /// <summary>
        /// Order quantity associated with the signal
        /// </summary>
        private double m_OrdQty = 0;

        /// <summary>
        /// price associated with the signal
        /// </summary>
        private double m_Price = 0;

        /// <summary>
        /// stop price associated with the signal
        /// </summary>
        private double m_StopPrice = 0;

        /// <summary>
        /// profit price associated with the signal
        /// </summary>
        private double m_ProfitPrice = 0;

        private string m_Text = "";

        /// <summary>
        /// User id ( if specified) - note this is the identity not the user's signonID
        /// </summary>
        private string user = "";

        public TradeSignal()
        {
            m_Identity = System.Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Type of signal
        /// </summary>
        [DataMember]
        [JsonProperty]
        public TradeSignalType SignalType
        {
            get { return m_SignalType; }
            set { m_SignalType = value; }
        }

        /// <summary>
        /// unique identifier
        /// </summary>
        [DataMember]
        [JsonProperty]
        public string Identity
        {
            get { return m_Identity; }
            set { m_Identity = value; }
        }

        [DataMember]
        [JsonProperty]
        public string User
        {
            get { return user; }
            set { user = value; }
        }

        /// <summary>
        /// Name of signal
        /// </summary>
        [DataMember]
        [JsonProperty]
        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        [DataMember]
        [JsonProperty]
        public TradeSignalStatus Status
        {
            get { return m_Status; }
            set { m_Status = value; }
        }

        /// <summary>
        /// Origin of signal - for example a CQG system trade in a trading system
        /// </summary>
        [DataMember]
        [JsonProperty]
        public string Origin
        {
            get { return m_Origin; }
            set { m_Origin = value; }
        }

        [DataMember]
        [JsonProperty]
        public DateTime TimeCreated
        {
            get { return m_TimeCreated; }
            set { m_TimeCreated = value; }
        }

        [DataMember]
        [JsonProperty]
        public long TimeValid
        {
            get { return m_TimeValid; }
            set { m_TimeValid = value; }
        }

        [DataMember]
        [JsonProperty]
        public string StrategyID
        {
            get { return m_StrategyID; }
            set { m_StrategyID = value; }
        }

        /// <summary>
        /// Is the signal set
        /// </summary>
        [DataMember]
        [JsonProperty]
        public bool Set
        {
            get { return m_Set; }
            set { m_Set = value; }
        }

        [DataMember]
        [JsonProperty]
        public string Mnemonic
        {
            get { return m_Mnemonic; }
            set { m_Mnemonic = value; }
        }

        /// <summary>
        /// Type of order associated with this signal
        /// </summary>
        [DataMember]
        [JsonProperty]
        public string OrdType
        {
            get { return m_OrdType; }
            set { m_OrdType = value; }
        }

        /// <summary>
        /// Side of order associated with this signal
        /// </summary>
        [DataMember]
        public string Side
        {
            get { return m_Side; }
            set { m_Side = value; }
        }

        /// <summary>
        /// Order quantity associated with the signal
        /// </summary>
        [DataMember]
        [JsonProperty]
        public double OrdQty
        {
            get { return m_OrdQty; }
            set { m_OrdQty = value; }
        }

        /// <summary>
        /// price associated with the signal
        /// </summary>
        [DataMember]
        [JsonProperty]
        public double Price
        {
            get { return m_Price; }
            set { m_Price = value; }
        }

        /// <summary>
        /// stop price associated with the signal
        /// </summary>
        [DataMember]
        [JsonProperty]
        public double StopPrice
        {
            get { return m_StopPrice; }
            set { m_StopPrice = value; }
        }

        /// <summary>
        /// profit price associated with the signal
        /// </summary>
        [DataMember]
        [JsonProperty]
        public double ProfitPrice
        {
            get { return m_ProfitPrice; }
            set { m_ProfitPrice = value; }
        }

        [DataMember]
        [JsonProperty]
        public string Text
        {
            get { return m_Text; }
            set { m_Text = value; }
        }

        public override string ToString()
        {
            string myInfo = "";
            try
            {
                myInfo += string.Format("Origin ={0} Name= {1} Type= {2} Set= {3} ", this.Origin, this.Name, this.SignalType.ToString(), this.Set);
                myInfo += string.Format("Side= {0} OrdType= {1} Qty= {2} ", this.Side, this.OrdType, this.OrdQty);
                myInfo += string.Format("Price= {0} StopPrice= {1} ProfitPrice= {2}", this.Price, this.StopPrice, this.ProfitPrice);
                myInfo += string.Format("Text= {0} ", this.Text);
                myInfo += string.Format("DateCreated= {0} ", this.TimeCreated);
            }
            catch
            {
            }
            return myInfo;
        }
    }
}