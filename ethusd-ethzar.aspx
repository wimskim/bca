﻿<%@ Page Language="C#" MasterPageFile="~/Template/Site.master" AutoEventWireup="true"
    CodeBehind="ethusd-ethzar.aspx.cs" Inherits="CryptoTrader.ethusd_ethzar" %>

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
                <a href="#" onclick="drawCharts(1);">last day</a> |
                <a href="#" onclick="drawCharts(3);">last 3 days</a> |
                <a href="#" onclick="drawCharts(7);">last week</a> |
                <a href="#" onclick="drawCharts(30);">last 30 days</a> |
                <a href="#" onclick="drawCharts(90);">last 90 days</a> |
                <a href="#" onclick="drawCharts(365);">last year</a> 
            </div>
        </div>

    </div>

    <div class="row">
        <div class="col-xs-12">
            <div class="chart">
                 <h5>Gaps</h5>

                <canvas id="chart_prof_percent"></canvas>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-xs-12">
            <div class="chart">
                 <h5>All Exchanges converted to ZAR plus fees</h5>
                <canvas id="chart_all_to_zar"></canvas>
            </div>
        </div>
    </div>

     <div class="row">
        <div class="col-xs-12">
            <div class="chart">
                 <h5>Exchange Rates</h5>
                <canvas id="chart_all_exchangerates"></canvas>
            </div>
        </div>
    </div>

    
    

    <script src="assets/js/charts/Chart.min.js" type="text/javascript"></script>
    <script src="assets/js/charts/average.js" type="text/javascript"></script>
    <script src="assets/js/charts/median.js" type="text/javascript"></script>
    <script src="assets/js/charts/scripteth.js?ver=122d11" type="text/javascript"></script>
    <script type="text/javascript">
       

    </script>
       
</asp:Content>
