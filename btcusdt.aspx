<%@ Page Language="C#" MasterPageFile="~/Template/Site.master"  AutoEventWireup="true" CodeBehind="btcusdt.aspx.cs" Inherits="CryptoTrader.btcusdt" %>

<asp:Content ID="cont" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    
    

    <div class="row">
        <div class="col-xs-12">
            <div class="chart">
                 <h5>BTCUSD</h5>

                <canvas id="chart_btcusd"></canvas>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-xs-12">
            <div class="chart">
                 <h5>BTC</h5>

                <canvas id="chart_btc"></canvas>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-xs-12">
            <div class="chart">
                 <h5>USD</h5>

                <canvas id="chart_usd"></canvas>
            </div>
        </div>
    </div>


     <script src="assets/js/charts/Chart.min.js" type="text/javascript"></script>
    <script src="assets/js/charts/average.js" type="text/javascript"></script>
    <script src="assets/js/charts/median.js" type="text/javascript"></script>
    <script src="assets/js/charts/script_btcusdavg.js?ver=121ce13dd" type="text/javascript"></script>
    <script type="text/javascript">
       

    </script>
</asp:Content>