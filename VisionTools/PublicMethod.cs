using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;

namespace VisionTools
{
    internal class PublicMethod
    {
        public PublicMethod()
        {

        }

        public static void readImages(string[] fileKey, ref System.Collections.Hashtable images)
        {
            if (images.ContainsKey(fileKey))
            {
                return;
            }

            try
            {
                foreach (string str in fileKey)
                {
                    //HImage image = new HImage(str);
                    HObject ho_image;
                    HOperatorSet.GenEmptyObj(out ho_image);
                    ho_image.Dispose();
                    HOperatorSet.ReadImage(out ho_image, str);
                    images.Add(str, ho_image);
                }
            }
            catch (HOperatorException e)
            {
                return;
            }
            return;
        }

        public static void removeImages(string fileKey, ref System.Collections.Hashtable images)
        {
            if (images.ContainsKey(fileKey))
            {
                images.Remove(fileKey);
            }
        }

        public static void removeImagesAll(ref System.Collections.Hashtable images)
        {
            images.Clear();
        }


        public static void genContourXld(List<ViewWindow.Model.RoiData> roiParameterTrans, out HObject roiTrans, out HObject arrow)
        {
            HObject ho_modelROI;
            HOperatorSet.GenEmptyObj(out ho_modelROI);
            HObject ho_arrow;
            HOperatorSet.GenEmptyObj(out ho_arrow);

            HOperatorSet.GenEmptyObj(out roiTrans);
            HOperatorSet.GenEmptyObj(out arrow);

            foreach (var roiData in roiParameterTrans)
            {
                switch (roiData.Name)
                {
                    //case "Rectangle1":
                    //    ho_modelROI.Dispose();
                    //    HOperatorSet.GenRectangle1(out ho_modelROI, roiData.Rectangle1.Row1, roiData.Rectangle1.Column1,
                    //        roiData.Rectangle1.Row2, roiData.Rectangle1.Column2);
                    //    break;
                    case "Rectangle2":
                        ho_modelROI.Dispose();
                        HOperatorSet.GenRectangle2ContourXld(out ho_modelROI, roiData.Rectangle2.Row, roiData.Rectangle2.Column,
                            -roiData.Rectangle2.Phi, roiData.Rectangle2.Lenth1, roiData.Rectangle2.Lenth2);

                        double m_R = roiData.Rectangle2.Row + Math.Sin(roiData.Rectangle2.Phi) * roiData.Rectangle2.Lenth1 * 1.2;
                        double m_C = roiData.Rectangle2.Column + Math.Cos(-roiData.Rectangle2.Phi) * roiData.Rectangle2.Lenth1 * 1.2;


                        HTuple hv_Rows1 = new HTuple();
                        hv_Rows1 = hv_Rows1.TupleConcat(roiData.Rectangle2.Row);
                        hv_Rows1 = hv_Rows1.TupleConcat(m_R);

                        HTuple hv_Cols1 = new HTuple();
                        hv_Cols1 = hv_Cols1.TupleConcat(roiData.Rectangle2.Column);
                        hv_Cols1 = hv_Cols1.TupleConcat(m_C);
                        ho_arrow.Dispose();
                        HOperatorSet.GenContourPolygonXld(out ho_arrow, hv_Rows1, hv_Cols1);

                        //R:= midR + sin(-phi) * L1 * 1.2
                        //C:= midC + cos(phi) * L1 * 1.2

                        break;
                    case "Circle":
                        ho_modelROI.Dispose();
                        HOperatorSet.GenCircleContourXld(out ho_modelROI, roiData.Circle.Row, roiData.Circle.Column, roiData.Circle.Radius,
                            0, 6.28318, "positive", 1);
                        break;
                    case "Line":

                        HTuple hv_Rows = new HTuple();
                        hv_Rows = hv_Rows.TupleConcat(roiData.Line.RowBegin);
                        hv_Rows = hv_Rows.TupleConcat(roiData.Line.RowEnd);

                        HTuple hv_Cols = new HTuple();
                        hv_Cols = hv_Cols.TupleConcat(roiData.Line.ColumnBegin);
                        hv_Cols = hv_Cols.TupleConcat(roiData.Line.ColumnEnd);

                        ho_modelROI.Dispose();

                        HOperatorSet.GenContourPolygonXld(out ho_modelROI, hv_Rows, hv_Cols);
                        break;
                }

                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(roiTrans, ho_modelROI, out ExpTmpOutVar_0);
                    roiTrans.Dispose();
                    roiTrans = ExpTmpOutVar_0;
                }

                {
                    HObject ExpTmpOutVar_1;
                    HOperatorSet.ConcatObj(arrow, ho_arrow, out ExpTmpOutVar_1);
                    arrow.Dispose();
                    arrow = ExpTmpOutVar_1;
                }
            }
        }

    }
}
