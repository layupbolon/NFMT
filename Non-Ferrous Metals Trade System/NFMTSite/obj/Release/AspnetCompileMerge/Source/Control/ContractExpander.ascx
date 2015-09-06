<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContractExpander.ascx.cs" Inherits="NFMTSite.Control.ContractExpander" %>
<div id="jqxConExpander" style="float: left; margin: 0px 5px 5px 5px;">
        <div>
            合约信息
        </div>
        <div class="InfoExpander">
            <ul>
                 <li>
                    <strong>我方抬头：</strong>
                    <span runat="server" id="spnInCorpNames"></span>
                </li>
                <li>
                    <strong>对方抬头：</strong>
                    <span runat="server" id="spnOutCorpNames"></span>
                </li>
                <li>
                    <strong>子合约编号：</strong>
                    <span runat="server" id="spnSubNo"></span>
                </li>
                <li>
                    <strong>品种：</strong>
                    <span runat="server" id="spnAsset"></span>
                </li>
                 <li>
                    <strong>子合约数量：</strong>
                    <span runat="server" id="spnSignAmount"></span>
                </li>
                <li>
                    <strong>执行最终日：</strong>
                    <span runat="server" id="spnPeriodE"></span>
                </li>               
                <li>
                    <strong>升贴水：</strong>
                    <span runat="server" id="spnPremium"></span>
                </li>
            </ul>
        </div>
    </div>
<script type="text/javascript">
    $("#jqxConExpander").jqxExpander({ width: "98%", expanded: false });
</script>
  