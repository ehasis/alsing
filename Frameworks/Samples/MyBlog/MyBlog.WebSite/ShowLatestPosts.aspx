<%@ Page Title="" Language="C#" MasterPageFile="~/MyMaster.Master" AutoEventWireup="true"
    CodeBehind="ShowLatestPosts.aspx.cs" Inherits="MyBlog.WebSite.ShowLatestPosts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Repeater ID="repLatestPosts" runat="server">
        <HeaderTemplate>
        </HeaderTemplate>
        <ItemTemplate>
            <div>
                <h2 id="post-699">
                    <a href="ShowPost.aspx?postId=<%#Eval("PostId") %>">
                        <%#Eval("Subject") %>
                    </a>
                </h2>
                <p class="comments">
                    <a href="ShowPost.aspx?postId=<%#Eval("PostId") %>">With
                        <%#Eval("ReplyCount") %>
                        comments </a>
                </p>
                <div class="main">
                    <div>
                        <%# FormatBody( Eval("Body")) %>
                    </div>
                </div>
                <div class="meta group">
                    <div class="signature">
                        <p>
                            Written by Roger Alsing <span class="edit"></span>
                        </p>
                        <p>
                            <%# FormatPublishDate(Eval("PublishDate"))%></p>
                    </div>
                    <div class="tags">
                        <p>
                            Posted in  <%# FormatCategories( Eval("CategoryLinks")) %>                             
                        </p>
                    </div>
                </div>
            </div>
        </ItemTemplate>
        <FooterTemplate>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>
