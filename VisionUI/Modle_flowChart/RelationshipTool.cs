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
using System.Drawing;
using System.Windows.Forms;
using Northwoods.Go;

namespace VisionUI.Modle
{
    [Serializable]
    public class RelationshipTool : GoTool
    {
        public RelationshipTool(GoView view) : base(view) { }

        public override void Start()
        {
            myLink = null;
            if (this.Predecessor == null)
            {
                //frmMDI.App.SetStatusMessage("Create relationship -- click to choose a node and then a node that should follow it");
            }
            else
            {
                // already have a GraphNode to start with
                MakeTemporaryLink();
                //frmMDI.App.SetStatusMessage("Create relationship by choosing a node");
            }
            StartTransaction();
        }

        public override void Stop()
        {
            if (myLink != null)
            {
                this.View.Layers.Default.Remove(myLink);
                myLink = null;
            }
            this.Predecessor = null;
            StopTransaction();
            //frmMDI.App.SetStatusMessage("Stopped creating relationships");
        }

        private void MakeTemporaryLink()
        {
            if (myLink == null)
            {
                // create a new link starting at the bottom port of the first node
                GoLink l = new GoLink();
                l.Orthogonal = true;

                GoPort fp = new GoPort();
                fp.Style = GoPortStyle.Rectangle;
                fp.FromSpot = this.Predecessor.BottomPort.FromSpot;
                fp.Bounds = this.Predecessor.BottomPort.Bounds;
                l.FromPort = fp;

                GoPort tp = new GoPort();
                tp.Size = new SizeF(1, 1);
                tp.Position = this.LastInput.DocPoint;
                tp.ToSpot = GoObject.MiddleTop;
                l.ToPort = tp;

                // the link is temporarily a view object
                this.View.Layers.Default.Add(l);
                myLink = l;
            }
        }

        public override void DoMouseDown()
        {
            if (this.Predecessor == null)
            {
                GraphNode gn = this.View.PickObject(true, false, this.LastInput.DocPoint, true) as GraphNode;
                if (gn == null)
                    return;
                if (!gn.IsPredecessor)
                    return;
                this.Predecessor = gn;
                //frmMDI.App.SetStatusMessage("Predecessor will be " + this.Predecessor.Text);
                MakeTemporaryLink();
            }
            else
            {
                GraphNode gn = this.View.PickObject(true, false, this.LastInput.DocPoint, true) as GraphNode;
                if (gn != null && gn != this.Predecessor && !GraphView.IsLinked(this.Predecessor, gn))
                {
                    // get rid of the temporary link
                    this.View.Layers.Default.Remove(myLink);
                    myLink = null;
                    // create the link in the document
                    GoPort nearest = FindNearestPort(this.LastInput.DocPoint, gn);
                    IGoLink link = this.View.CreateLink(this.Predecessor.BottomPort, nearest);
                    if (link != null)
                    {
                        this.TransactionResult = "New Relationship";
                        this.View.RaiseLinkCreated(link.GoObject);
                        this.View.Selection.Select(link.GoObject);
                    }
                    StopTool();
                }
            }
        }

        public GoPort FindNearestPort(PointF pt, GraphNode gn)
        {
            float maxdist = 10e20f;
            GoPort closest = null;
            GoPort p;
            p = gn.TopPort;
            if (p != null)
            {
                float dist = (p.Left - pt.X) * (p.Left - pt.X) + (p.Top - pt.Y) * (p.Top - pt.Y);
                if (dist < maxdist)
                {
                    maxdist = dist;
                    closest = p;
                }
            }
            p = gn.RightPort;
            if (p != null)
            {
                float dist = (p.Left - pt.X) * (p.Left - pt.X) + (p.Top - pt.Y) * (p.Top - pt.Y);
                if (dist < maxdist)
                {
                    maxdist = dist;
                    closest = p;
                }
            }
            p = gn.BottomPort;
            if (p != null)
            {
                float dist = (p.Left - pt.X) * (p.Left - pt.X) + (p.Top - pt.Y) * (p.Top - pt.Y);
                if (dist < maxdist)
                {
                    maxdist = dist;
                    closest = p;
                }
            }
            p = gn.LeftPort;
            if (p != null)
            {
                float dist = (p.Left - pt.X) * (p.Left - pt.X) + (p.Top - pt.Y) * (p.Top - pt.Y);
                if (dist < maxdist)
                {
                    maxdist = dist;
                    closest = p;
                }
            }
            return closest;
        }

        public override void DoMouseMove()
        {
            if (myLink != null && myLink.ToPort != null)
            {
                GoPort p = myLink.ToPort.GoObject as GoPort;
                if (p != null)
                {
                    p.Position = this.LastInput.DocPoint;
                    GraphNode gn = this.View.PickObject(true, false, this.LastInput.DocPoint, true) as GraphNode;
                    if (gn != null && gn != this.Predecessor && !GraphView.IsLinked(this.Predecessor, gn))
                    {
                        GoPort nearest = FindNearestPort(this.LastInput.DocPoint, gn);
                        if (nearest != null)
                        {
                            p.Position = nearest.Position;
                            p.ToSpot = nearest.ToSpot;
                        }
                    }
                }
            }
        }

        public override void DoMouseUp()
        {
            // don't call the base functionality, which resets the tool
        }

        public GraphNode Predecessor
        {
            get { return myPredecessor; }
            set { myPredecessor = value; }
        }

        private GoLink myLink = null;
        private GraphNode myPredecessor = null;
    }
}
