<%@ Page Language="C#" MasterPageFile="~/Template/Site.master" AutoEventWireup="true"
    CodeBehind="contact.aspx.cs" Inherits="marnahattingh.com.contact" %>

<asp:Content ID="cont" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-header">
        <h1>
            Contact</h1>
    </div>
    <form class="form-horizontal" role="form">
    <div class="form-group">
        <label for="inputEmail1" class="col-lg-2 control-label">
            Email</label>
        <div class="col-lg-8">
            <input type="email" class="form-control" id="inputEmail1" placeholder="Email">
        </div>
        
    </div>
    <div class="form-group">
        <label for="inputPassword1" class="col-lg-2 control-label">
            Message</label>
        <div class="col-lg-8">
           <textarea class="form-control" rows="3"></textarea>
        </div>

    </div>

    <div class="form-group">
        <div class="col-lg-offset-2 col-lg-10">
            <button type="submit" class="btn btn-default">
                Send</button>
        </div>
    </div>
    </form>
</asp:Content>
