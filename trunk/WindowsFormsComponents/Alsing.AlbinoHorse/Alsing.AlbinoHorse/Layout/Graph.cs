using System;
using System.Collections.Generic;
using System.Drawing;

namespace AlbinoHorse.Layout
{
    public class Graph
    {
        public Graph()
        {
            Nodes = new List<Node>();
        }

        #region Property Nodes

        public List<Node> Nodes { get; set; }

        #endregion

        public void AutoLayout()
        {
            var SingleNodes = new List<Node>();
            var LinearNodeLists = new List<List<Node>>();
            var ConnectedNodes = new List<Node>();
            var AllLinearNodes = new List<Node>();

            var r = new Random(0);
            foreach (Node node in Nodes)
            {
                if (AllLinearNodes.Contains(node))
                    continue;


                if (node.Connections.Count == 0)
                {
                    SingleNodes.Add(node);
                }
                else
                {
                    var tmpNodes = new List<Node>();
                    bool isLinear = true;
                    if (node.Connections.Count == 1)
                    {
                        tmpNodes.Add(node);
                        Node tmp = node.Connections[0];
                        tmpNodes.Add(tmp);
                        while (isLinear)
                        {
                            if (tmp.Connections.Count == 1)
                            {
                                LinearNodeLists.Add(tmpNodes);
                                foreach (Node linearNode in tmpNodes)
                                    ConnectedNodes.Remove(linearNode);

                                AllLinearNodes.AddRange(tmpNodes);
                                break;
                            }
                            else if (tmp.Connections.Count == 2)
                            {
                                if (tmp.Connections[0] != tmp)
                                    tmp = tmp.Connections[0];
                                else if (tmp.Connections[1] != tmp)
                                    tmp = tmp.Connections[1];

                                if (tmpNodes.Contains(tmp))
                                {
                                    isLinear = false;
                                }
                                else
                                {
                                    tmpNodes.Add(tmp);
                                }
                            }
                            else
                            {
                                isLinear = false;
                            }
                        }
                    }
                    else
                    {
                        isLinear = false;
                    }

                    if (isLinear) {}
                    else
                    {
                        ConnectedNodes.Add(node);
                    }
                }
            }


            int x = 0;
            int y = 0;
            foreach (Node node in SingleNodes)
            {
                if (x == 10)
                {
                    x = 0;
                    y++;
                }

                node.Bounds.X = x*30;
                node.Bounds.Y = y*30;
                node.Bounds.Height = 20;
                x++;
            }

            if (SingleNodes.Count > 0)
                y++;

            x = 0;
            int yl = 0;
            int maxY = 0;
            foreach (var linearNodes in LinearNodeLists)
            {
                yl = y;
                foreach (Node node in linearNodes)
                {
                    node.Bounds.X = x*30;
                    node.Bounds.Y = (y*30) + yl*30;
                    yl += (int) node.Bounds.Height/20;
                }
                x++;
                maxY = Math.Max(yl, maxY);
            }

            //foreach (Node node in ConnectedNodes)
            //{
            //    node.Bounds.X = r.Next(0,400);
            //    node.Bounds.Y = r.Next(0,400);
            //}


            for (int i = 0; i < 3000; i++)
            {
                LayoutNodes(ConnectedNodes, 2);
            }

            for (int i = 0; i < 1000; i++)
            {
                LayoutNodes(ConnectedNodes, 1);
            }

            for (int i = 0; i < 1000; i++)
            {
                LayoutNodes(ConnectedNodes, 0);
            }


            float left = float.MaxValue;
            float right = float.MinValue;
            float top = float.MaxValue;
            float bottom = float.MinValue;

            foreach (Node node in ConnectedNodes)
            {
                left = Math.Min(left, node.Bounds.Left);
                right = Math.Max(right, node.Bounds.Right);

                top = Math.Min(top, node.Bounds.Top);
                bottom = Math.Max(bottom, node.Bounds.Bottom);
            }

            foreach (Node node in ConnectedNodes)
            {
                node.Bounds.Offset(-left + 50, -top + 10);
            }

            //foreach (Node node in ConnectedNodes)
            //{
            //    int xp = (int)node.Bounds.X / 20;
            //    int yp = (int)node.Bounds.Y / 20;

            //    node.Bounds.X = xp * 20;
            //    node.Bounds.Y = yp * 20;

            //}
        }

        private static void LayoutNodes(List<Node> ConnectedNodes, float massFactor)
        {
            foreach (Node node in ConnectedNodes)
            {
                PointF force = node.GetForceDirection(ConnectedNodes, massFactor);
                node.Bounds.X += force.X;
                node.Bounds.Y += force.Y;
            }
        }
    }
}