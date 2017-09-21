<%@ Page Language="C#" MasterPageFile="~/Template/Site.master" AutoEventWireup="true"
    CodeBehind="ethbtc.aspx.cs" Inherits="CryptoTrader.ethbtc" %>

<asp:Content ID="cont" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form-horizontal" role="form">
        <div class="form-group">
            <label class="col-xs-6 col-sm-4 control-label">
                Eth to trade</label>
            <div class="col-xs-6 col-sm-4">
                <input type="text" class="form-control" id="txtEth" runat="server" placeholder="ETH to trade" value="1" />
            </div>
        </div>
        <div class="form-group">
            <label class="col-xs-6 col-sm-4 control-label">
            </label>
            <div class="col-xs-6 col-sm-4">
                <asp:LinkButton ID='lnkSave1' OnClick="lnkSave1_Click" runat="server" class="btn btn-success"><i class="fa fa-fw fa-save"></i>&nbsp;Refresh</asp:LinkButton>
            </div>
        </div>

        <div class="form-group">
            <dic class="col-xs-3">
               </dic>

           
            <div class="col-xs-3">
                <b>BID (price we sell at)</b>
            </div>
             <div class="col-xs-3">
                <b>ASK (price we buy at)</b>
            </div>
            <div class="col-xs-3">
                <b>Gap</b>
            </div>
        </div>
         <div class="form-group">
            <div class="col-xs-3">
                <b>BITLISH</b>
            </div>
             <div class="col-xs-3" id="lblBitlishBid" runat="server">
                0.0
            </div>
            <div class="col-xs-3" id="lblBitlishAsk" runat="server">
                0.0
            </div>

              <div class="col-xs-3" id="lblAvgGap" runat="server">
                0.0
            </div>
            
        </div>
        <div class="form-group">
            <div class="col-xs-3">
                <b>CEX</b>
            </div>
            <div class="col-xs-3" id="lblCexBid" runat="server">
                0.0
            </div>
            <div class="col-xs-3" id="lblCexAsk" runat="server">
                0.0
            </div>
            
        </div>
       
        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <div class="col-xs-12">
                        <h4>Bitlish to CEX</h4>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-6">
                        <b>BTC to sell at Bitlish (Buy Eth) </b>
                    </div>
                    <div class="col-xs-6" id="divBtoC_BTCsold" runat="server">
                        0.0
                    </div>
                </div>

                 <div class="form-group">
                    <div class="col-xs-6">
                        <b>ETH to transfer to CEX<br />(-0.2% taker fee)</b>
                    </div>
                    <div class="col-xs-6" id="divBtoC_Eth" runat="server">
                        0.0
                    </div>
                </div>
                 <div class="form-group">
                    <div class="col-xs-6">
                        <b>BTC to buy at CEX (Sell Eth)<br />(-0.2% taker fee)</b>
                    </div>
                    <div class="col-xs-6" id="divBtoC_BtcBought" runat="server">
                        0.0
                    </div>
                </div>
                 <div class="form-group">
                    <div class="col-xs-6">
                        <b>Profit BTC</b>
                    </div>
                    <div class="col-xs-6" id="divBtoC_ProfitBTC" runat="server">
                        0.0
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-6">
                        <b>Profit %</b>
                    </div>
                    <div class="col-xs-6" id="divBtoC_ProfitPerc" runat="server">
                        0.0
                    </div>
                </div>
            </div>

            <div class="col-md-6">
                <div class="form-group">
                    <div class="col-xs-12">
                        <h4>CEX to Bitlish</h4>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-6">
                        <b>BTC to sell at CEX (Buy Eth)</b>
                    </div>
                    <div class="col-xs-6" id="divCtoB_BTCsold" runat="server">
                        0.0
                    </div>
                </div>
                 <div class="form-group">
                    <div class="col-xs-6">
                        <b>ETH to transfer to Bitlish<br />(-0.2% taker fee)</b>
                    </div>
                    <div class="col-xs-6" id="divCtoB_Eth" runat="server">
                        0.0
                    </div>
                </div>
                 <div class="form-group">
                    <div class="col-xs-6">
                        <b>BTC to buy at Bitlish (Sell Eth)<br />(-0.2% taker fee)</b>
                    </div>
                    <div class="col-xs-6" id="divCtoB_BtcBought" runat="server">
                        0.0
                    </div>
                </div>
                 <div class="form-group">
                    <div class="col-xs-6">
                        <b>Profit BTC</b>
                    </div>
                    <div class="col-xs-6" id="divCtoB_ProfitBTC" runat="server">
                        0.0
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-6">
                        <b>Profit %</b>
                    </div>
                    <div class="col-xs-6" id="divCtoB_ProfitPerc" runat="server">
                        0.0
                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>
