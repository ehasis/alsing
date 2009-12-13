<%@ Page Title="" Language="C#" MasterPageFile="~/MyMaster.Master" AutoEventWireup="true" CodeBehind="ShowLatestPosts.aspx.cs" Inherits="MyBlog.WebSite.ShowLatestPosts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class="MainContent">
        <asp:Repeater ID="repLatestPosts" runat="server">
            <HeaderTemplate>
                <div class="PostList">
            </HeaderTemplate>

            <ItemTemplate>
                <div class="Post">
                    <div class="PostPublishDate">
                        <%#Eval("PublishDate")%>
                    </div>
                    <div class="PostSubject">
                        <h1>
                            <a href="ShowPost.aspx?postId=<%#Eval("PostId") %>">
                                <%#Eval("Subject") %>
                            </a>
                        </h1>
                    </div>
                    <div class="PostBody">
                        <%#Eval("Body") %>
                    </div> 
                    <div class="PostReplies">
                        Replies: <%#Eval("ReplyCount") %>
                    </div>   
                </div>
            </ItemTemplate>

            <FooterTemplate>
                </div>
            </FooterTemplate>        
        </asp:Repeater>
    </div>
</asp:Content>
