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
using System.Drawing;
using System.IO;
using Northwoods.Go;
using Northwoods.Go.Xml;

namespace VisionUI.Modle
{
    /// <summary>
    /// Summary description for GraphDoc.
    /// </summary>
    [Serializable]
    public class GraphDoc : GoDocument
    {
        public GraphDoc()
        {
            this.Name = "Flow Chart " + NextDocumentID();
            this.IsModified = false;
            this.PaperColor = Color.FromArgb(236, 236, 213);
        }

        public String Location
        {
            get { return myLocation; }
            set
            {
                String old = myLocation;
                if (old != value)
                {
                    RemoveDocument(old);
                    myLocation = value;
                    AddDocument(value, this);
                    RaiseChanged(ChangedLocation, 0, null, 0, old, NullRect, 0, value, NullRect);
                }
            }
        }

        public void AddTitleAndAnnotation()
        {
            StartTransaction();
            Title title = new Title();
            title.Text = "Flow Chart";
            title.Label.FontSize = 20;
            title.Label.Bold = true;
            title.Position = new PointF(100, 10);
            Add(title);
            Title annot = new Title();
            annot.Text = DateTime.Today.ToShortDateString();
            annot.Position = new PointF(400, 10);
            Add(annot);
            FinishTransaction("Added Title and Annotation");
        }

        public void InsertComment()
        {
            GoComment comment = new GoComment();
            comment.Text = "Enter your comment here,\r\non multiple lines.";
            comment.Position = NextNodePosition();
            comment.Label.Multiline = true;
            comment.Label.Editable = true;
            StartTransaction();
            Add(comment);
            FinishTransaction("Insert Comment");
        }

        public void InsertNode(GraphNodeKind k)
        {
            GoObject n = new GraphNode(k);
            n.Position = NextNodePosition();
            StartTransaction();
            Add(n);
            FinishTransaction("Insert Node");
        }


        public PointF NextNodePosition()
        {
            PointF next = myNextNodePos;
            myNextNodePos.X += 50;
            if (myNextNodePos.X > 400)
            {
                myNextNodePos.X = 40;
                myNextNodePos.Y += 50;
                if (myNextNodePos.Y > 300)
                    myNextNodePos.Y = 40;
            }
            return next;
        }

        public override void ChangeValue(GoChangedEventArgs e, bool undo)
        {
            switch (e.Hint)
            {
                case ChangedLocation:
                    {
                        this.Location = (String)e.GetValue(undo);
                        break;
                    }
                default:
                    base.ChangeValue(e, undo);
                    return;
            }
        }

        private PointF InternalNextNodePos
        {
            get { return myNextNodePos; }
            set { myNextNodePos = value; }
        }

        public int Version
        {
            get { return 3; }
            set
            {
                if (value != this.Version)
                    throw new NotSupportedException("For simplicity, this sample application does not handle different versions of saved documents");
            }
        }

        // TODO: adapt the XML elements and attributes to match your classes and their properties
        private static void InitReaderWriter(GoXmlReaderWriterBase rw)
        {
            GoXmlBindingTransformer.DefaultTracingEnabled = true;  // for debugging, check your Output window (trace listener)
            GoXmlBindingTransformer t;

            t = new GoXmlBindingTransformer("flowchart", new GraphDoc());
            t.AddBinding("version", "Version", GoXmlBindingFlags.RethrowsExceptions);  // let exception from Version setter propagate out
            t.AddBinding("name", "Name");
            t.AddBinding("nextnodepos", "InternalNextNodePos");
            rw.AddTransformer(t);

            t = new GoXmlBindingTransformer("node", new GraphNode(GraphNodeKind.Step));
            t.IdAttributeUsedForSharedObjects = true;  // each GraphNode gets a unique ID
            t.HandlesNamedPorts = true;  // generate attributes for each of the named ports, specifying their IDs
            t.AddBinding("kind", "Kind");
            t.AddBinding("text", "Text");
            t.AddBinding("pos", "Position");
            rw.AddTransformer(t);

            t = new GoXmlBindingTransformer("title", new Title());
            rw.AddTransformer(t);

            t = new GoXmlBindingTransformer("comment", new GoComment());
            t.IdAttributeUsedForSharedObjects = true;  // each GoComment and Title gets a unique ID
            t.AddBinding("text", "Text");
            t.AddBinding("familyname", "Label.FamilyName");
            t.AddBinding("fontsize", "Label.FontSize");
            t.AddBinding("alignment", "Label.Alignment");
            t.AddBinding("bold", "Label.Bold");
            t.AddBinding("italic", "Label.Italic");
            t.AddBinding("strikethrough", "Label.StrikeThrough");
            t.AddBinding("underline", "Label.Underline");
            t.AddBinding("multiline", "Label.Multiline");
            t.AddBinding("wrapping", "Label.Wrapping");
            t.AddBinding("wrappingwidth", "Label.WrappingWidth");
            t.AddBinding("editable", "Label.Editable");
            t.AddBinding("loc", "Location");  // last property, since it depends on content/alignment
            rw.AddTransformer(t);

            t = new GoXmlBindingTransformer("link", new GraphLink());
            t.AddBinding("from", "FromPort");
            t.AddBinding("to", "ToPort");
            t.AddBinding("label", "Text");
            rw.AddTransformer(t);
        }
        
        public void Store(Stream file, String loc)
        {
            bool oldskips = this.SkipsUndoManager;
            this.SkipsUndoManager = true;
            this.Location = loc;
            int lastslash = loc.LastIndexOf("\\");
            if (lastslash >= 0)
                this.Name = loc.Substring(lastslash + 1);
            else
                this.Name = loc;
            this.IsModified = false;
            this.SkipsUndoManager = oldskips;

            GoXmlWriter xw = new GoXmlWriter();
            InitReaderWriter(xw);
            xw.NodesGeneratedFirst = true;
            xw.Objects = this;
            xw.Generate(file);
        }

        public static GraphDoc Load(Stream file, String loc)
        {
            GoXmlReader xr = new GoXmlReader();
            InitReaderWriter(xr);
            GraphDoc doc = xr.Consume(file) as GraphDoc;
            if (doc == null) return null;

            // update the file location
            doc.Location = loc;
            // undo managers are not serialized
            doc.UndoManager = new GoUndoManager();
            doc.IsModified = false;
            AddDocument(loc, doc);
            return doc;
        }


        private static void InitReaderWriterA(GoXmlReaderWriterBase rw)
        {
            GoXmlBindingTransformer tr = new GoXmlBindingTransformer("node", new GraphNode(GraphNodeKind.End));

            // indicate that the XML consists of nested elements
            tr.TreeStructured = true;
            // provide the prototype link for connecting the nodes
            tr.TreeLinkPrototype = new GoLink();
            // indicate the direction of the link (from parent to child)
            tr.TreeLinksToChildren = true;
            //tr.AddBinding("label", "Text");

            //tr.AddBinding("kind", "Kind");
            tr.AddBinding("text", "Text");

            rw.AddTransformer(tr);
        }

        public void StoreA(Stream file, String loc, GraphView view)
        {
            /*bool oldskips = this.SkipsUndoManager;
            this.SkipsUndoManager = true;
            this.Location = loc;
            int lastslash = loc.LastIndexOf("\\");
            if (lastslash >= 0)
                this.Name = loc.Substring(lastslash + 1);
            else
                this.Name = loc;
            this.IsModified = false;
            this.SkipsUndoManager = oldskips;*/

            GoXmlWriter wrt = new GoXmlWriter();
            InitReaderWriterA(wrt);

            // need to select root node(s)
            GoCollection coll = new GoCollection();
            coll.Add(view.Document.FindNode("Start"));
            wrt.Objects = coll;
            wrt.Generate(file);
        }
        

        public override bool IsReadOnly
        {
            get
            {
                if (this.Location == "") return false;
                FileInfo info = new FileInfo(this.Location);
                bool ro = ((info.Attributes & FileAttributes.ReadOnly) != 0);
                bool oldskips = this.SkipsUndoManager;
                this.SkipsUndoManager = true;
                // take out the following statement if you want the user to be able
                // to modify the graph even though the file is read-only
                SetModifiable(!ro);
                this.SkipsUndoManager = oldskips;
                return ro;
            }
        }


        public static int NextDocumentID()
        {
            return myDocCounter++;
        }

        public static GraphDoc FindDocument(String location)
        {
            return myDocuments[location] as GraphDoc;
        }

        internal static void AddDocument(String location, GraphDoc doc)
        {
            myDocuments[location] = doc;
        }

        internal static void RemoveDocument(String location)
        {
            myDocuments.Remove(location);
        }


        public const int ChangedLocation = LastHint + 23;

        private static int myDocCounter = 1;
        private static Hashtable myDocuments = new Hashtable();

        private String myLocation = "";
        private PointF myNextNodePos = new PointF(30, 30);
    }

    [Serializable]
    public class Title : GoComment
    {
        public Title()
        {
            this.Label.Multiline = true;
            this.Label.Editable = true;
            this.Label.Alignment = GoObject.MiddleTop;
        }

        protected override GoObject CreateBackground()
        {
            return null;
        }
    }

    [Serializable]
    public class GraphLink : GoLabeledLink
    {
        public GraphLink()
        {
            this.Orthogonal = true;
            this.Style = GoStrokeStyle.RoundedLine;
            this.ToArrow = true;
        }

        public String Text
        {  // null value means no FromLabel
            get
            {
                GoText lab = this.FromLabel as GoText;
                if (lab != null)
                    return lab.Text;
                else
                    return null;
            }
            set
            {
                if (value == null)
                {
                    this.FromLabel = null;
                }
                else
                {
                    GoText lab = this.FromLabel as GoText;
                    if (lab == null)
                    {
                        lab = new GoText();
                        lab.Selectable = false;
                        lab.Editable = true;
                        this.FromLabel = lab;
                    }
                    lab.Text = value;
                }
            }
        }
    }
}
