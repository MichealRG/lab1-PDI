<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RssDisplay.aspx.cs" Inherits="RssReader.RssDisplay" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />

    <div class="row">
        <%--<asp:GridView ID="GridView1" runat="server" 
            EmptyDataText="No data available." CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="gv_PageIndexChanging" AllowPaging="True">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="Title" 
                HeaderText="Title" ReadOnly="True"
                SortExpression="Title" />
                <asp:BoundField DataField="PubDate" 
                HeaderText="Publication Date" 
                ReadOnly="True"/>
                <asp:BoundField DataField="Description" 
                HeaderText="Description" ReadOnly="true" />
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscen    dingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>--%>


        <asp:Repeater ID="Repeater1" runat="server" ItemType="RssReader.Model.RssItem" OnItemCommand="rptPaging_ItemCommand">
            <ItemTemplate>
                <div class="col-md-6">
                    <h2>
                        <%# DataBinder.Eval(Container.DataItem,"Title") %>
                        <%--<%# Item.Title %>--%>
                    </h2>
                    <span class="btn btn-info">
                        <%# Item.PubDate %>
                    </span>
                    <p>
                        <%# Item.Description %>
                    </p>
                </div>
                <asp:LinkButton ID="btnPage"
                    Style="padding: 8px; margin: 2px; background: #ffa100; border: solid 1px #666; font: 8pt tahoma;"
                    CommandName="Page" CommandArgument="<%# Container.DataItem %>"
                    runat="server" ForeColor="White" Font-Bold="True">
                 <%# Container.DataItem %>
                      </asp:LinkButton>
            </ItemTemplate>
        </asp:Repeater>


        <nav aria-label="Page navigation example">
            <ul class="pagination">
                <li class="page-item"><a class="page-link" href="#">Previous</a></li>
                <li class="page-item"><a class="page-link" href="#">1</a></li>
                <li class="page-item"><a class="page-link" href="#">2</a></li>
                <li class="page-item"><a class="page-link" href="#">3</a></li>
                <li class="page-item"><a class="page-link" href="#">Next</a></li>
            </ul>
        </nav>
        <asp:XmlDataSource ID="XmlDataSource1" runat="server" XPath="https://news.google.com/rss?hl=en-US&amp;gl=US&amp;ceid=US:en"></asp:XmlDataSource>
    </div>


</asp:Content>
