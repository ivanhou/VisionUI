/*
 *  Copyright ?Northwoods Software Corporation, 1998-2013. All Rights
 *  Reserved.
 *
 *  Restricted Rights: Use, duplication, or disclosure by the U.S.
 *  Government is subject to restrictions as set forth in subparagraph
 *  (c) (1) (ii) of DFARS 252.227-7013, or in FAR 52.227-19, or in FAR
 *  52.227-14 Alt. III, as applicable.
 */

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Northwoods.Go;

namespace VisionUI.Modle
{
    /// <summary>
    /// Specify different kinds of GraphNodes.
    /// </summary>
    public enum GraphNodeKind
    {
        Start = 1,
        End = 2,
        Step = 3,
        Input = 4,
        Output = 5,
        Decision = 6,
        Read = 7,
        Write = 8,
        ManualOperation = 9,
        Database = 10,
        TestKind = 11
    }


    /// <summary>
    /// A node representing a step or action in a flowchart.
    /// </summary>
    [Serializable]
    public class GraphNode : GoTextNode
    {
        public GraphNode()
        {
            InitCommon();
        }

        public GraphNode(GraphNodeKind k)
        {
            InitCommon();
            this.Kind = k;
        }

        public GraphNode(GraphNodeKind k, string language)
        {
            InitCommon();
            this.Kind = k;
        }

        private void InitCommon()
        {
            // assume GraphNodeKind.Step
            this.Label.Wrapping = true;
            this.Label.Editable = true;
            this.Label.Alignment = Middle;
            //this.Text = "Step";
            this.Text = SelectionLanguage.ResourceCulture.GetString("GraphNode_Step");
            this.Editable = true;
            this.TopLeftMargin = new SizeF(10, 5);
            this.BottomRightMargin = new SizeF(10, 5);
            this.Shadowed = true;
            this.Background = new GoDrawing(GoFigure.Rectangle);
            Color first = Color.FromArgb(70, 174, 250);
            Color second = Color.FromArgb(214, 225, 235);
            this.Shape.FillShapeHighlight(first, second);
        }

        /// <summary>
        /// The location for each node is the Center.
        /// </summary>
        public override PointF Location
        {
            get { return this.Center; }
            set { this.Center = value; }
        }

        /// <summary>
        /// Adjust port positions for certain background shapes.
        /// </summary>
        /// <param name="childchanged"></param>
        public override void LayoutChildren(GoObject childchanged)
        {
            base.LayoutChildren(childchanged);
            GoDrawing draw = this.Background as GoDrawing;
            if (draw != null)
            {
                PointF tempPoint;
                if (draw.Figure == GoFigure.ManualOperation || draw.Figure == GoFigure.Input || draw.Figure == GoFigure.Output)
                {
                    if (this.RightPort != null)
                    {
                        draw.GetNearestIntersectionPoint(new PointF(this.RightPort.Center.X + .01f, this.RightPort.Center.Y),
                          this.RightPort.Center, out tempPoint);
                        this.RightPort.Right = tempPoint.X;
                    }
                    if (this.LeftPort != null)
                    {
                        draw.GetNearestIntersectionPoint(new PointF(this.LeftPort.Center.X + .01f, this.LeftPort.Center.Y),
                          this.LeftPort.Center, out tempPoint);
                        this.LeftPort.Left = tempPoint.X;
                    }
                }
            }
        }

        /// <summary>
        /// When the mouse passes over a node, display all of its ports.
        /// </summary>
        /// <param name="evt"></param>
        /// <param name="view"></param>
        /// <returns></returns>
        /// <remarks>
        /// All ports on all nodes are hidden when the mouse hovers over the background.
        /// </remarks>
        public override bool OnMouseOver(GoInputEventArgs evt, GoView view)
        {
            GraphView v = view as GraphView;
            if (v != null)
            {
                foreach (GoPort p in this.Ports)
                {
                    p.SkipsUndoManager = true;
                    p.Style = GoPortStyle.Ellipse;
                    p.SkipsUndoManager = false;
                }
            }
            return false;
        }

        /// <summary>
        /// Bring up a GraphNode specific context menu.
        /// </summary>
        /// <param name="evt"></param>
        /// <param name="view"></param>
        /// <returns></returns>
        public override GoContextMenu GetContextMenu(GoView view)
        {
            if (view is GoOverview) return null;
            if (!(view.Document is GraphDoc)) return null;
            GoContextMenu cm = new GoContextMenu(view);
            if (!((GraphDoc)view.Document).IsReadOnly && this.IsPredecessor)
            {
                cm.MenuItems.Add(new MenuItem("Draw Relationship", new EventHandler(this.DrawRelationship_Command)));
                cm.MenuItems.Add(new MenuItem("-"));
            }
            if (CanDelete())
            {
                cm.MenuItems.Add(new MenuItem("Cut", new EventHandler(this.Cut_Command)));
            }
            if (CanCopy())
            {
                cm.MenuItems.Add(new MenuItem("Copy", new EventHandler(this.Copy_Command)));
            }
            return cm;
        }

        public void DrawRelationship_Command(Object sender, EventArgs e)
        {
            GoView v = GoContextMenu.FindView(sender as MenuItem);
            if (v != null)
            {
                RelationshipTool t = new RelationshipTool(v);
                t.Predecessor = this;
                v.Tool = t;
            }
        }

        public void Cut_Command(Object sender, EventArgs e)
        {
            GoView v = GoContextMenu.FindView(sender as MenuItem);
            if (v != null)
                v.EditCut();
        }

        public void Copy_Command(Object sender, EventArgs e)
        {
            GoView v = GoContextMenu.FindView(sender as MenuItem);
            if (v != null)
                v.EditCopy();
        }

        public GraphNodeKind Kind
        {
            get { return myKind; }
            set
            {
                GraphNodeKind old = myKind;
                if (old != value)
                {
                    myKind = value;
                    Changed(ChangedKind, (int)old, null, NullRect, (int)value, null, NullRect);
                    OnKindChanged(old, value);
                }
            }
        }

        protected virtual void OnKindChanged(GraphNodeKind oldkind, GraphNodeKind newkind)
        {
            // update the parts, based on the Kind of node this now is
            Color first = Color.FromArgb(70, 174, 250);
            Color second = Color.FromArgb(214, 225, 235);
            switch (newkind)
            {
                //TestKind
                case GraphNodeKind.TestKind:
                    {
                        //this.Text = "TestKind";
                        this.Text = SelectionLanguage.ResourceCulture.GetString("GraphNode_TestKind");
                        this.Background = new GoDrawing(GoFigure.RoundedRectangle);
                        this.Shape.FillShapeHighlight(Color.Green);
                        this.TopLeftMargin = new SizeF(10, 5);
                        this.BottomRightMargin = new SizeF(10, 5);
                        UpdatePorts("", "o", "o", "o");
                        break;
                    }
                case GraphNodeKind.Start:
                    {
                        //this.Text = "Start";
                        this.Text = SelectionLanguage.ResourceCulture.GetString("GraphNode_Start");
                        this.Background = new GoDrawing(GoFigure.RoundedRectangle);
                        this.Shape.FillShapeHighlight(Color.Green);
                        this.TopLeftMargin = new SizeF(10, 5);
                        this.BottomRightMargin = new SizeF(10, 5);
                        UpdatePorts("", "o", "o", "o");
                        break;
                    }
                case GraphNodeKind.End:
                    {
                        //this.Text = "End";
                        this.Text = SelectionLanguage.ResourceCulture.GetString("GraphNode_End");
                        this.Background = new GoDrawing(GoFigure.RoundedRectangle);
                        this.Shape.FillShapeHighlight(Color.DarkRed);
                        this.TopLeftMargin = new SizeF(10, 5);
                        this.BottomRightMargin = new SizeF(10, 5);
                        UpdatePorts("i", "i", "", "i");
                        break;
                    }
                case GraphNodeKind.Step:
                    {
                        //this.Text = "Step";
                        this.Text = SelectionLanguage.ResourceCulture.GetString("GraphNode_Step");
                        this.Background = new GoDrawing(GoFigure.Rectangle);
                        this.Shape.FillShapeHighlight(first, second);
                        this.TopLeftMargin = new SizeF(10, 5);
                        this.BottomRightMargin = new SizeF(10, 5);
                        UpdatePorts("io", "io", "io", "io");
                        break;
                    }
                case GraphNodeKind.Input:
                    {
                        //this.Text = "Input";
                        this.Text = SelectionLanguage.ResourceCulture.GetString("GraphNode_Input");
                        this.Background = new GoDrawing(GoFigure.Input);
                        this.Shape.FillShapeHighlight(first, second);
                        this.TopLeftMargin = new SizeF(20, 4);
                        this.BottomRightMargin = new SizeF(20, 4);
                        UpdatePorts("io", "io", "io", "io");
                        break;
                    }
                case GraphNodeKind.Output:
                    {
                        //this.Text = "Output";
                        this.Text = SelectionLanguage.ResourceCulture.GetString("GraphNode_Output");
                        this.Background = new GoDrawing(GoFigure.Output);
                        this.Shape.FillShapeHighlight(first, second);
                        this.TopLeftMargin = new SizeF(20, 4);
                        this.BottomRightMargin = new SizeF(20, 4);
                        UpdatePorts("io", "io", "io", "io");
                        break;
                    }
                case GraphNodeKind.Decision:
                    {
                        this.Text = "?";
                        this.Text = SelectionLanguage.ResourceCulture.GetString("GraphNode_Decision");
                        this.Background = new GoDrawing(GoFigure.Decision);
                        this.Shape.FillShapeHighlight(first, second);
                        this.TopLeftMargin = new SizeF(35, 20);
                        this.BottomRightMargin = new SizeF(35, 20);
                        UpdatePorts("i", "io", "o", "io");
                        break;
                    }
                case GraphNodeKind.Read:
                    {
                        this.Text = "Read";
                        this.Text = SelectionLanguage.ResourceCulture.GetString("GraphNode_Read");
                        this.Background = new GoDrawing(GoFigure.Ellipse);
                        this.Shape.FillEllipseGradient(first, Color.White);
                        this.Shape.BrushPoint = new PointF(.3f, .3f);
                        this.TopLeftMargin = new SizeF(20, 10);
                        this.BottomRightMargin = new SizeF(20, 10);
                        UpdatePorts("io", "io", "io", "io");
                        break;
                    }
                case GraphNodeKind.Write:
                    {
                        this.Text = "Write";
                        this.Text = SelectionLanguage.ResourceCulture.GetString("GraphNode_Write");
                        this.Background = new GoDrawing(GoFigure.Ellipse);
                        this.Shape.FillEllipseGradient(first, Color.White);
                        this.Shape.BrushPoint = new PointF(.3f, .3f);
                        this.TopLeftMargin = new SizeF(20, 10);
                        this.BottomRightMargin = new SizeF(20, 10);
                        UpdatePorts("io", "io", "io", "io");
                        break;
                    }
                case GraphNodeKind.ManualOperation:
                    {
                        this.Text = "Manual \nOperation";
                        this.Text = SelectionLanguage.ResourceCulture.GetString("GraphNode_ManualOperation");
                        this.Background = new GoDrawing(GoFigure.ManualOperation);
                        this.Shape.FillShapeHighlight(first, second);
                        this.TopLeftMargin = new SizeF(20, 10);
                        this.BottomRightMargin = new SizeF(20, 10);
                        UpdatePorts("io", "io", "io", "io");
                        break;
                    }
                case GraphNodeKind.Database:
                    {
                        this.Text = "Database";
                        this.Text = SelectionLanguage.ResourceCulture.GetString("GraphNode_Database");
                        this.Background = new GoDrawing(GoFigure.Database);
                        this.Shape.FillShapeHighlight(first, second);
                        this.Shape.BrushPoint = new PointF(.15f, .55f);
                        this.Shape.BrushFocusScales = new SizeF(.1f, .1f);
                        this.TopLeftMargin = new SizeF(10, 30);
                        this.BottomRightMargin = new SizeF(10, 15);
                        UpdatePorts("io", "io", "io", "io");
                        break;
                    }
                default: throw new InvalidEnumArgumentException("newkind", (int)newkind, typeof(GraphNodeKind));
            }
        }

        protected override GoPort CreatePort(int spot)
        {
            GoPort p = base.CreatePort(spot);
            p.Brush = null;
            return p;
        }

        private void UpdatePorts(String t, String r, String b, String l)
        {  // TopPort, RightPort, BottomPort, LeftPort
            if (t == "")
            {
                this.TopPort = null;
            }
            else
            {
                if (this.TopPort == null) this.TopPort = CreatePort(MiddleTop);
                if (this.TopPort != null)
                {
                    this.TopPort.IsValidFrom = t.IndexOf('o') > -1;
                    this.TopPort.IsValidTo = t.IndexOf('i') > -1;
                }
            }
            if (r == "")
            {
                this.RightPort = null;
            }
            else
            {
                if (this.RightPort == null) this.RightPort = CreatePort(MiddleRight);
                if (this.RightPort != null)
                {
                    this.RightPort.IsValidFrom = r.IndexOf('o') > -1;
                    this.RightPort.IsValidTo = r.IndexOf('i') > -1;
                }
            }
            if (b == "")
            {
                this.BottomPort = null;
            }
            else
            {
                if (this.BottomPort == null) this.BottomPort = CreatePort(MiddleBottom);
                if (this.BottomPort != null)
                {
                    this.BottomPort.IsValidFrom = b.IndexOf('o') > -1;
                    this.BottomPort.IsValidTo = b.IndexOf('i') > -1;
                }
            }
            if (l == "")
            {
                this.LeftPort = null;
            }
            else
            {
                if (this.LeftPort == null) this.LeftPort = CreatePort(MiddleLeft);
                if (this.LeftPort != null)
                {
                    this.LeftPort.IsValidFrom = l.IndexOf('o') > -1;
                    this.LeftPort.IsValidTo = l.IndexOf('i') > -1;
                }
            }
        }

        public bool IsPredecessor
        {
            get { return this.BottomPort != null; }
        }

        public override void ChangeValue(GoChangedEventArgs e, bool undo)
        {
            if (e.SubHint == ChangedKind)
                myKind = (GraphNodeKind)e.GetInt(undo);
            else
                base.ChangeValue(e, undo);
        }

        public const int ChangedKind = GoObject.LastChangedHint + 7;

        private GraphNodeKind myKind = GraphNodeKind.Step;
    }
}
