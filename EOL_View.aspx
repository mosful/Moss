<%@ Page Title="EOL View" Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/MasterPage_2011_noMenu.master"
    CodeBehind="EOL_View.aspx.cs" Inherits="NewEOL.WebForm.EOL_View" %>

<%@ Register Assembly="AUO.CustomControls" Namespace="AUO.CustomControls" TagPrefix="AUO" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table align="center" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td align="center">
                <h3>
                    <asp:Label ID="lblTitle" runat="server" ReadOnly="true"></asp:Label>
                    <asp:Label ID="lblCancelTitle" runat="server" Text=" Cancel " Visible="false"></asp:Label>
                    Apply</h3>
            </td>
        </tr>
        <tr align="center">
            <td>
                <fieldset>
                    <legend>
                        <asp:Label ID="lblTitleField" runat="server" ReadOnly="true"></asp:Label>
                        Info</legend>
                    <table cellspacing="0" class="table1" align="center">
                        <tr>
                            <td class="table1_th1" style="font-weight: 600;" width="15%">
                                <asp:Label ID="lblTitleType" runat="server" ReadOnly="true"></asp:Label>
                                No.:
                            </td>
                            <td>
                                <asp:Label ID="lblNO" runat="server" ReadOnly="true" />
                            </td>
                            <td class="table1_th1" style="font-weight: 600;" width="15%">
                                Apply Date:
                            </td>
                            <td align="left" width="35%">
                                <asp:Label ID="lblApplyDate" runat="server" ReadOnly="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="table1_th1" style="font-weight: 600;" width="15%">
                                Applicant:
                            </td>
                            <td align="left" width="35%">
                                <asp:Label ID="lblEmp" runat="server" ReadOnly="true"></asp:Label>
                            </td>
                            <td class="table1_th1" style="font-weight: 600;" width="15%">
                                Dept:
                            </td>
                            <td align="left" width="35%">
                                <asp:Label ID="lblEmpDept" runat="server" ReadOnly="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="table1_th1" style="font-weight: 600;" width="15%">
                                <%-- 2018-06-27 mosslin add 毛姊要求顯示表單編號FORM.550,讓user知道是哪張表單 --%>
                                <%-- 2018-09-11 mosslin add 改顯示為Cancel --%>
                                <asp:Label ID="lblCancel" runat="server" Text="Cancel " Visible="false"></asp:Label> FlowER No:
                            </td>
                            <td align="left" width="35%">
                                <asp:Label ID="lblFlowerNo" runat="server" ReadOnly="true"></asp:Label>
                                <asp:Label ID="lblPageCode" runat="server" ReadOnly="true" Visible="false"></asp:Label>
                            </td>
                            <td class="table1_th1" style="font-weight: 600;" width="15%">
                                Status:
                            </td>
                            <td align="left" width="35%">
                                <asp:Label ID="lblStatus" runat="server" ReadOnly="true"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trApplicationCode" runat="server" visible="false">
                            <td class="table1_th1" style="font-weight: 600;" width="15%">
                                Application Code<font color="red">(*)</font>
                            </td>
                            <td colspan="3" align="left">
                                <asp:Label ID="lblAppCode" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <!-- 2019-07-18 mosslin add PM提早轉Lock需求 -->
                        <tr id="trLockStartDate" runat="server" visible="false">
                            <td class="table1_th1" style="font-weight: 600;" width="15%">
                                <font color="red">Lock Start Date</font>
                            </td>
                            <td colspan="3" align="left">
                                <asp:Label ID="lblLockStartDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trPMWishLockDate" runat="server" visible="false">
                            <td class="table1_th1" style="font-weight: 600;" width="15%">
                                <font color="red">PM Wish Lock Date</font>
                            </td>
                            <td colspan="3" align="left">
                                <asp:Label ID="lblPMWishLockDate" runat="server"></asp:Label>
                                <asp:Label ID="lblEarlyLock" runat="server" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trFlower" runat="server">
                            <td class="table1_th1" style="font-weight: 600;" width="15%">
                                Model Version<font color="red">(*)</font>
                            </td>
                            <td colspan="3" align="left">
                                <asp:Label ID="lblModel" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="table1_th1" style="font-weight: 600;" width="15%">
                                Part No.<font color="red">(*)</font>
                            </td>
                            <td colspan="3" align="left">
                                <asp:UpdatePanel ID="upModel" runat="server" UpdateMode="Always">
                                    <ContentTemplate>
                                        <asp:GridView ID="gvPartNo" runat="server" AutoGenerateColumns="False" Width="95%"
                                            OnRowDataBound="gvPartNo_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Part No" DataField="PART_NO" ItemStyle-Width="20%" />
                                                <asp:BoundField HeaderText="EOL Type" DataField="REF_EOL_TYPE" HeaderStyle-Width="10%" />
                                                <asp:BoundField HeaderText="EOL No." DataField="REF_EOL_NO" ItemStyle-Width="20%" />
                                                <asp:BoundField HeaderText="Status" DataField="REF_EOL_STATUS" ItemStyle-Width="15%" />
                                                <asp:BoundField HeaderText="GPoS Status" DataField="GPOS_STATUS" ItemStyle-Width="20%" />
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr id="trReplace" runat="server">
                            <td class="table1_th1" style="font-weight: 600;" width="15%">
                                Replacement model :
                            </td>
                            <td colspan="3" align="left">
                                <asp:CheckBox ID="chkShowDoc" runat="server" Text="Show on EOL document" />
                                <br />
                                <asp:GridView ID="gvReplace" runat="server" AutoGenerateColumns="False" Width="65%">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-Width="5%" HeaderText="No">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Model Version" DataField="MODEL_VERSION" ItemStyle-Width="30%" />
                                        <asp:BoundField HeaderText="Part No" DataField="PART_NO" HeaderStyle-Width="30%" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr id="trSchedule" runat="server">
                            <td class="table1_th1" style="font-weight: 600;" width="15%">
                                Planned schedule :<font color="red">(*)</font>
                            </td>
                            <td colspan="3" align="left">
                                <table align="left" cellpadding="0" cellspacing="0" border="0" style="border-width: 0px;">
                                    <tr id="trOfficialEOLNotice" runat="server" align="left">
                                        <td>
                                            Official EOL Notice :
                                        </td>
                                        <td>
                                            <asp:Label ID="lblOfficial" runat="server" Style="word-break: break-all; word-wrap: break-word" />
                                        </td>
                                    </tr>
                                    <tr id="trFeedback" runat="server" align="left">
                                        <td>
                                            Expected customer feedback :
                                        </td>
                                        <td>
                                            <asp:Label ID="lblFeedback" runat="server" Style="word-break: break-all; word-wrap: break-word" />
                                        </td>
                                    </tr>
                                    <tr id="trLastBuy" runat="server" align="left">
                                        <td>
                                            Last buy order confirmation :
                                        </td>
                                        <td>
                                            <asp:Label ID="lblLastBuy" runat="server" Style="word-break: break-all; word-wrap: break-word" />
                                        </td>
                                    </tr>
                                    <tr id="trEOLDate" runat="server">
                                        <td>
                                            EOL Date :
                                        </td>
                                        <td>
                                            <asp:Label ID="lblEOLDate" runat="server" Style="word-break: break-all; word-wrap: break-word" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="trCDMS" runat="server">
                                <td class="table1_th1" style="font-weight: 600;" width="15%">
                                    CDMS
                                </td>
                                <td colspan="3" align="left">
                                    <asp:Label ID="lblCDMS" runat="server" Text="" /><br />
                                    <asp:GridView ID="gvCDMS" runat="server" AutoGenerateColumns="False" Width="95%"
                                        HeaderStyle-HorizontalAlign="Center">
                                        <Columns>
                                            <asp:BoundField HeaderText="BU" DataField="WEBSITE" ItemStyle-Width="5%" />
                                            <asp:BoundField HeaderText="AppCode" DataField="APPLICATION_CODE" ItemStyle-Width="5%" />
                                            <asp:BoundField HeaderText="Customer" DataField="CUSTOMER" ItemStyle-Width="10%" />
                                            <asp:BoundField HeaderText="Brand" DataField="BRAND" HeaderStyle-Width="10%" />
                                            <asp:BoundField HeaderText="Part No." DataField="PART_NO" HeaderStyle-Width="10%" />
                                            <asp:BoundField DataField="MON1" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="MON2" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="MON3" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="MON4" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="MON5" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="MON6" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Right" />
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        <tr runat="server" id="trCustomer">
                            <td class="table1_th1" style="font-weight: 600;" width="15%">
                                Customer<font color="red">(*)</font>
                            </td>
                            <td colspan="3" align="left">
                                <asp:Label ID="lblCustomer" runat="server" Style="word-break: break-all; word-wrap: break-word" />
                            </td>
                        </tr>
                        <tr>
                            <td class="table1_th1" style="font-weight: 600;" width="15%">
                                Purpose<font color="red">(*)</font>
                            </td>
                            <td colspan="3" align="left">
                                <asp:Label ID="lblPurpose" runat="server" Style="word-break: break-all; word-wrap: break-word" />
                            </td>
                        </tr>
                        <tr id="trPic" runat="server">
                            <td class="table1_th1" style="font-weight: 600;" width="15%">
                                Attached Picture
                            </td>
                            <td colspan="3" align="left">
                                <table width="100%">
                                    <tr>
                                        <td align="left">
                                            <asp:GridView ID="gvPic" runat="server" AutoGenerateColumns="False" Width="80%"
                                                OnRowCommand="gvPic_RowCommand" OnRowDataBound="gvPic_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No" HeaderStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="File Name" DataField="FILE_NAME" ItemStyle-Width="50%" />
                                                    <asp:BoundField HeaderText="Upload Date" DataField="FILE_DATE" ItemStyle-Width="40%" />
                                                    <asp:BoundField HeaderText="FILE_ID" DataField="FILE_ID" />
                                                    <asp:TemplateField HeaderText="Download" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="lnkDownLoad" runat="server" CausesValidation="False" CommandName="DownLoad"
                                                                AlternateText="Download" ImageUrl="../img/download.gif" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="table1_th1" style="font-weight: 600;" width="15%">
                                Attached File
                            </td>
                            <td colspan="3" align="left">
                                <table width="100%">
                                    <tr>
                                        <td align="left">
                                            <asp:GridView ID="gvFile" runat="server" AutoGenerateColumns="False" Width="80%"
                                                OnRowCommand="gvFile_RowCommand" OnRowDataBound="gvFile_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No" HeaderStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="File Name" DataField="FILE_NAME" ItemStyle-Width="50%" />
                                                    <asp:BoundField HeaderText="Upload Date" DataField="FILE_DATE" ItemStyle-Width="40%" />
                                                    <asp:BoundField HeaderText="FILE_ID" DataField="FILE_ID" />
                                                    <asp:TemplateField HeaderText="Download" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="lnkDownLoad" runat="server" CausesValidation="False" CommandName="DownLoad"
                                                                AlternateText="Download" ImageUrl="../img/download.gif" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="trShip" runat="server">
                            <td colspan="4" style="text-align: left">
                                <asp:UpdatePanel ID="upCDMS" runat="server" UpdateMode="Always">
                                    <ContentTemplate>
                                        <asp:Label ID="lblShipment" runat="server" Text="Shipment" />
                                        <asp:GridView ID="gvShipment" runat="server" AutoGenerateColumns="False" Width="80%"
                                            HeaderStyle-HorizontalAlign="Center">
                                            <Columns>
                                                <asp:BoundField HeaderText="BU" DataField="WEBSITE" ItemStyle-Width="4%" />
                                                <asp:BoundField HeaderText="AppCode" DataField="APPLICATION_CODE" ItemStyle-Width="4%" />
                                                <asp:BoundField HeaderText="Customer" DataField="CUSTOMER" ItemStyle-Width="8%" />
                                                <asp:BoundField HeaderText="Brand" DataField="BRAND" HeaderStyle-Width="8%" />
                                                <asp:BoundField HeaderText="Part No." DataField="PART_NO" HeaderStyle-Width="8%" />
                                                <asp:BoundField HeaderText="Ship To" DataField="SHIP_TO_CUSTOMER" HeaderStyle-Width="8%" />
                                                <asp:BoundField DataField="MON1" HeaderStyle-Width="5%" />
                                                <asp:BoundField DataField="MON2" HeaderStyle-Width="5%" />
                                                <asp:BoundField DataField="MON3" HeaderStyle-Width="5%" />
                                                <asp:BoundField DataField="MON4" HeaderStyle-Width="5%" />
                                                <asp:BoundField DataField="MON5" HeaderStyle-Width="5%" />
                                                <asp:BoundField DataField="MON6" HeaderStyle-Width="5%" />
                                                <asp:BoundField DataField="MON7" HeaderStyle-Width="5%" />
                                                <asp:BoundField DataField="MON8" HeaderStyle-Width="5%" />
                                                <asp:BoundField DataField="MON9" HeaderStyle-Width="5%" />
                                                <asp:BoundField DataField="MON10" HeaderStyle-Width="5%" />
                                                <asp:BoundField DataField="MON11" HeaderStyle-Width="5%" />
                                                <asp:BoundField DataField="MON12" HeaderStyle-Width="5%" />
                                            </Columns>
                                        </asp:GridView>
                                        <asp:Label ID="lblAllocation" runat="server" Text="Allocation" />
                                        <asp:GridView ID="gvAllocation" runat="server" AutoGenerateColumns="False" Width="80%"
                                            HeaderStyle-HorizontalAlign="Center">
                                            <Columns>
                                                <asp:BoundField HeaderText="BU" DataField="WEBSITE" ItemStyle-Width="5%" />
                                                <asp:BoundField HeaderText="AppCode" DataField="APPLICATION_CODE" ItemStyle-Width="5%" />
                                                <asp:BoundField HeaderText="Customer" DataField="CUSTOMER" ItemStyle-Width="10%" />
                                                <asp:BoundField HeaderText="Brand" DataField="BRAND" HeaderStyle-Width="10%" />
                                                <asp:BoundField HeaderText="Part No." DataField="PART_NO" HeaderStyle-Width="10%" />
                                                <asp:BoundField DataField="MON1" HeaderStyle-Width="10%" ControlStyle-CssClass="right" DataFormatString="{0:N0}" HtmlEncode="false" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField DataField="MON2" HeaderStyle-Width="10%" ControlStyle-CssClass="right" DataFormatString="{0:N0}" HtmlEncode="false" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField DataField="MON3" HeaderStyle-Width="10%" DataFormatString="{0:N0}" HtmlEncode="false" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField DataField="MON4" HeaderStyle-Width="10%" DataFormatString="{0:N0}" HtmlEncode="false" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField DataField="MON5" HeaderStyle-Width="10%" DataFormatString="{0:N0}" HtmlEncode="false" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField DataField="MON6" HeaderStyle-Width="10%" DataFormatString="{0:N0}" HtmlEncode="false" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" />
                                            </Columns>
                                        </asp:GridView>
                                        <%--<asp:Label ID="lblForecast" runat="server" Text="Forecast" />
                                        <asp:GridView ID="gvForecast" runat="server" AutoGenerateColumns="False" Width="80%"
                                            HeaderStyle-HorizontalAlign="Center">
                                            <Columns>
                                                <asp:BoundField HeaderText="BU" DataField="WEBSITE" ItemStyle-Width="5%" />
                                                <asp:BoundField HeaderText="AppCode" DataField="APPLICATION_CODE" ItemStyle-Width="5%" />
                                                <asp:BoundField HeaderText="Customer" DataField="CUSTOMER" ItemStyle-Width="10%" />
                                                <asp:BoundField HeaderText="Brand" DataField="BRAND" HeaderStyle-Width="10%" />
                                                <asp:BoundField HeaderText="Part No." DataField="PART_NO" HeaderStyle-Width="10%" />
                                                <asp:BoundField DataField="MON1" HeaderStyle-Width="10%" DataFormatString="{0:N0}" HtmlEncode="false" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField DataField="MON2" HeaderStyle-Width="10%" DataFormatString="{0:N0}" HtmlEncode="false" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField DataField="MON3" HeaderStyle-Width="10%" DataFormatString="{0:N0}" HtmlEncode="false" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField DataField="MON4" HeaderStyle-Width="10%" DataFormatString="{0:N0}" HtmlEncode="false" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField DataField="MON5" HeaderStyle-Width="10%" DataFormatString="{0:N0}" HtmlEncode="false" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField DataField="MON6" HeaderStyle-Width="10%" DataFormatString="{0:N0}" HtmlEncode="false" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" />
                                            </Columns>
                                        </asp:GridView>
                                        <asp:Label ID="lblBP" runat="server" Text="BP Customer" />
                                        <asp:GridView ID="gvBP" runat="server" AutoGenerateColumns="False" Width="80%" HeaderStyle-HorizontalAlign="Center">
                                            <Columns>
                                                <asp:BoundField HeaderText="BU" DataField="WEBSITE" ItemStyle-Width="5%" />
                                                <asp:BoundField HeaderText="AppCode" DataField="APPLICATION_CODE" ItemStyle-Width="5%" />
                                                <asp:BoundField HeaderText="Customer" DataField="CUSTOMER" ItemStyle-Width="10%" />
                                                <asp:BoundField HeaderText="Brand" DataField="BRAND" HeaderStyle-Width="10%" />
                                                <asp:BoundField HeaderText="Part No." DataField="PART_NO" HeaderStyle-Width="10%" />
                                                <asp:BoundField DataField="MON1" HeaderStyle-Width="10%" DataFormatString="{0:N0}" />
                                                <asp:BoundField DataField="MON2" HeaderStyle-Width="10%" DataFormatString="{0:N0}" />
                                                <asp:BoundField DataField="MON3" HeaderStyle-Width="10%" DataFormatString="{0:N0}" />
                                                <asp:BoundField DataField="MON4" HeaderStyle-Width="10%" DataFormatString="{0:N0}" />
                                                <asp:BoundField DataField="MON5" HeaderStyle-Width="10%" DataFormatString="{0:N0}" />
                                                <asp:BoundField DataField="MON6" HeaderStyle-Width="10%" DataFormatString="{0:N0}" />
                                            </Columns>
                                        </asp:GridView>--%>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
    <div>
        <%-- 2018-06-27 mosslin add 毛姊要求顯示表單名稱"[FORM.550] BIS End-of-Life Management",讓user知道是哪張表單 --%>
        <div id="divFormTitle" runat="server" class="title" visible="true" style="font-family: Verdana; font-size: 12pt; color: #0E52B9; position: relative;
            font-weight: bold; text-align: center; border:none; border-color:White;">[FORM.550] BIS End-of-Life Management <asp:Label ID="lblFormTitle" runat="server" ReadOnly="true"></asp:Label>
        </div>
        <iframe id="IFRAME_APPROVE_HISTORY" runat="server" width="100%" height="300px" frameborder="0"
            cellspacing="0" style="border-style: solid; border-width: 1px; border-color: white;
            font: Arial; font-size: xx-small;" scrolling="yes"></iframe>
    </div>
</asp:Content>
