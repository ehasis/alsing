<%@ Page Title="" Language="C#" MasterPageFile="~/MyMaster.Master" AutoEventWireup="true" CodeBehind="ShowPost.aspx.cs" Inherits="MyBlog.WebSite.ShowPost" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

            <h2>
                <asp:Literal ID="litSubject" runat="server" Text="" ></asp:Literal>
            </h2>
            <p class="comments">
                <a href="ShowPost.aspx?postId=<%#Eval("PostId") %>">With
                    <%#Eval("ReplyCount") %>
                    comments </a>
            </p>
            <div class="main">
                <div>
                    <asp:Literal ID="litBody" runat="server" Text="" ></asp:Literal>
                </div>
            </div>
            <div class="meta group">
                <div class="signature">
                    <p>
                        Written by Roger Alsing <span class="edit"></span>
                    </p>
                    <p>
                        <asp:Literal ID="litPublishDate" runat="server" Text="" ></asp:Literal>
                    </p>
                </div>
                <div class="tags">
                    <p>
                        Posted in  <%# FormatCategories( Eval("CategoryLinks")) %>                             
                    </p>
                </div>
            </div>
            
            <div class="navigation group">
                <div class="alignleft"><a href="ShowPost.aspx?postId=<%="hej"%>" >&laquo; Previous post</a></div>
                <div class="alignright"></div>
            </div>
            
            <h3 class="reply"><asp:Literal ID="litCommentCount" runat="server" Text="" ></asp:Literal> Responses</h3>             
 
            <ol class="commentlist">
                <asp:Repeater ID="repReplies" runat="server">
                    <HeaderTemplate>
                        <div>
                    </HeaderTemplate>
                    <FooterTemplate>
                        </div>
                    </FooterTemplate>
                    <ItemTemplate>
                        <li id="comment-1372" class="comment even thread-even depth-1">
                            <div id="div-comment-1372">
                                <div class="comment_mod">
                                </div>
                        	
                                <div class="comment_text">
                                    <%# Eval("Body") %>
                                </div>
            	
                                <div class="comment_author vcard">
                                    <img alt='' src='http://1.gravatar.com/avatar/7ac3d313189eb4d3fe101e3aadcd08e2?s=32&d=identicon&r=G' class='avatar avatar-32' height='32' width='32' />	<p><strong class="fn"><a href='<%# Eval("UserWebSite") %>' rel='external nofollow' class='url'><%# Eval("UserName") %></a></strong></p>
                                    <p><small>
                                        <%# FormatCreationDate(Eval("CreationDate"))%>
                                    </small></p>
                                </div>
                                <div class="clear"></div>
          
                            </div>	                    
                        </li>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                         <li id="comment-1372" class="comment odd thread-odd depth-1">
                            <div id="div-comment-1372">
                                <div class="comment_mod">
                                </div>
                        	
                                <div class="comment_text">
                                    <%# Eval("Body") %>
                                </div>
            	
                                <div class="comment_author vcard">
                                    <img alt='' src='http://1.gravatar.com/avatar/7ac3d313189eb4d3fe101e3aadcd08e2?s=32&d=identicon&r=G' class='avatar avatar-32' height='32' width='32' />	<p><strong class="fn"><a href='<%# Eval("UserWebSite") %>' rel='external nofollow' class='url'><%# Eval("UserName") %></a></strong></p>
                                    <p><small>
                                        <%# FormatCreationDate(Eval("CreationDate"))%>
                                    </small></p>
                                </div>
                                <div class="clear"></div>
            	
                            </div>	                    
                        </li>
                    </AlternatingItemTemplate>
                </asp:Repeater>
            </ol>
            
            <asp:Panel ID="pnlReply" runat="server">
                <div class="navigation">
                    <div class="alignleft">
                    </div>
                    <div class="alignright">
                    </div>
                </div>
                <br />
                <div id="respond">
                    <h3 class="reply">
                        Leave a Reply</h3>
                   
                    <div id="commentform">
                        <div class="postinput">
                            <p>
                                <input class="comment" type="text" name="author" id="author" value="" size="22" tabindex="1" />
                                <label for="author">
                                    <small>Name (required)</small></label>
                            </p>
                            <p>
                                <input class="comment" type="text" name="email" id="email" value="" size="22" tabindex="2" />
                                <label for="email">
                                    <small>E-mail (will not be published) (required)</small></label>
                            </p>
                            <p>
                                <input class="comment" type="text" name="url" id="url" value="" size="22" tabindex="3" />
                                <label for="url">
                                    <small>Website</small></label>
                            </p>
                            <p>
                                <textarea name="comment" id="comment" cols="100%" rows="10" tabindex="4"></textarea></p>
                            <p>
                                <input class="submit" name="submit" type="submit" id="submit" tabindex="5" value="Submit Comment"
                                    title="Please review your comment before submitting" />
                                <input type='hidden' name='comment_post_ID' value='699' id='comment_post_ID' />
                                <input type='hidden' name='comment_parent' id='comment_parent' value='0' />
                            </p>
                        </div>
                    </div>
                </div>
            </asp:Panel>
</asp:Content>
