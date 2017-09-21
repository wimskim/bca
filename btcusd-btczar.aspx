<%@ Page Language="C#" MasterPageFile="~/Template/Site.master" AutoEventWireup="true"
    CodeBehind="btcusd-btczar.aspx.cs" Inherits="CryptoTrader.btcusd_btczar" %>

<asp:Content ID="cont" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-xs-12">
             <h4>Current Profits</h4>
            <label id="lblProfits" runat="server"></label>
        </div>
    </div>
    
    <div class="row">

        <div class="col-xs-12">
              <h4>History</h4>
            <div class="period">
                <a href="#" onclick="drawCharts(1 * 24 * 60);">last day</a> |
                <a href="#" onclick="drawCharts(3 * 24 * 60);">last 3 days</a> |
                <a href="#" onclick="drawCharts(7 * 24 * 60);">last week</a> |
                <a href="#" onclick="drawCharts(30 * 24 * 60);">last 30 days</a> |
                <a href="#" onclick="drawCharts(90 * 24 * 60);">last 90 days</a> |
                <a href="#" onclick="drawCharts(365 * 24 * 60);">last year</a> 
            </div>
        </div>

    </div>

    <div class="row">

        <div class="col-xs-12">
           
          

            <div class="chart">
                <canvas id="chart_prof_percent"></canvas>
            </div>
        </div>

    </div>
    <script src="/assets/js/charts/Chart.min.js" type="text/javascript"></script>
    <script src="/assets/js/charts/average.js" type="text/javascript"></script>
    <script src="/assets/js/charts/median.js" type="text/javascript"></script>
    <script src="/assets/js/charts/script.js" type="text/javascript"></script>
    <script type="text/javascript">
        drawCharts(1 * 24 * 60);
    </script>

</asp:Content>
