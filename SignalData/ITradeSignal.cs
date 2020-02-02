
using System;
using System.Collections.Generic;
using System.Text;

namespace SignalData
{
    /// <summary>
    /// Defines the query types available
    /// </summary>
    public enum TradeSignalType
    {
        enter, renter, exit, condition, undefined
    }

    /// <summary>
    /// defines the status of a trade signal
    /// </summary>
    public enum TradeSignalStatus
    {
        /// <summary>
        /// The trade signal is on route/waiting to be handled
        /// </summary>
        pending,
        /// <summary>
        /// The signal was rejected - you may get more information in the Text Field
        /// </summary>
        reject,
        /// <summary>
        /// The time valid had elasped before the signal could be processed
        /// </summary>
        timeout,
        /// <summary>
        /// The Signal has been receieved by some system for processing
        /// </summary>
        received,
        /// <summary>
        /// The signal has been completely processed
        /// </summary>
        complete,
        /// <summary>
        /// An error occured prcoessing the signal - check the Text field
        /// </summary>
        error,
        /// <summary>
        /// No defiend state
        /// </summary>
        undefined
    }

    /// <summary>
    /// Defines a trade signal or condition from an external system
    /// </summary>
    public interface ITradeSignal
    {
        /// <summary>
        /// Type of signal
        /// </summary>
        TradeSignalType SignalType
        { get; set; }

        /// <summary>
        /// Return a trade signal status
        /// </summary>
        TradeSignalStatus Status
        { get; set; }

        /// <summary>
        /// Get/Set the time created on a tradesignal
        /// </summary>
        DateTime TimeCreated
        { get; set; }

        /// <summary>
        /// Time in ms that this is signal is valid 
        /// </summary>
        long TimeValid
        { get; set; }

        /// <summary>
        /// unique identifier - GUID 
        /// </summary>
        string Identity
        { get; set; }

        /// <summary>
        /// User id ( if specified) - the Identity not the user signonID
        /// </summary>
        string User
        { get; set; }


        /// <summary>
        /// Origin of signal - for example a CQG system trade in a trading system
        /// </summary>
        string Origin
        { get; set; }

        /// <summary>
        /// Identifies a strategy that will be used to execute the signal
        /// this can be empty.
        /// </summary>
        string StrategyID
        { get; set; }

        /// <summary>
        /// Name of signal
        /// </summary>
        string Name
        { get; set; }

        /// <summary>
        /// Is the signal set
        /// </summary>
        bool Set
        { get; set; }

        /// <summary>
        /// Product to be used with this signal - if this is empty then the
        /// product must be already specified in the strategy/algo processing
        /// the signal
        /// </summary>
        string Mnemonic
        { get; set; }

        /// <summary>
        /// Type of order associated with this signal
        /// </summary>
        string OrdType
        { get; set; }

        /// <summary>
        /// Side of order associated with this signal
        /// </summary>
        string Side
        { get; set; }

        /// <summary>
        /// Order quantity associated with the signal
        /// </summary>
        double OrdQty
        { get; set; }

        /// <summary>
        /// price associated with the signal
        /// </summary>
        double Price
        { get; set; }


        /// <summary>
        /// stop price associated with the signal
        /// </summary>
        double StopPrice
        { get; set; }

        /// <summary>
        /// profit price associated with the signal
        /// </summary>
        double ProfitPrice
        { get; set; }

        /// <summary>
        /// user defined text associated with the signal
        /// </summary>
        string Text
        { get; set; }
    }
}
