<%@ Page Title="" Language="C#" MasterPageFile="~/MyMaster.Master" AutoEventWireup="true" CodeBehind="ShowPost.aspx.cs" Inherits="MyBlog.WebSite.ShowPost" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class="MainContent">
        <div class="PostList">
            <div class="Post">
                <div class="PostPublishDate">
                    <asp:Literal ID="litPublishDate" runat="server" Text="" ></asp:Literal>
                    
                </div>
                <div class="PostSubject">
                    <h1>
                        <asp:Literal ID="litSubject" runat="server" Text="" ></asp:Literal>
                    </h1>
                </div>
                <div class="PostBody">
                    <asp:Literal ID="litBody" runat="server" Text="" ></asp:Literal>
                </div> 
                <div class="PostReplies">
                    <asp:Repeater ID="repReplies" runat="server">
                        <HeaderTemplate>
                            <div>
                        </HeaderTemplate>
                        <FooterTemplate>
                            </div>
                        </FooterTemplate>
                        <ItemTemplate>
                            <%# Eval("UserName") %>
                            <%# Eval("Body") %>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>   
            </div>
        </div>
    </div>
</asp:Content>
