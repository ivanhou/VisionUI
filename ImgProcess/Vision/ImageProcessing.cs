using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;

namespace ImgProcess.Vision
{
    public class ImageProcessing
    {

        public void ImgProcess(int id, int imgID, HObject ho_Image, out Event.EventArgsStation1 result)
        {
            bool mResult = false;
            HTuple hv_Height = new HTuple(), hv_Width = new HTuple();
            HTuple hv_Mean = new HTuple(), hv_Deviation = new HTuple();
            Event.CommandResult mCommand = Event.CommandResult.NG;
            try
            {
                for (int i = 0; i < 300000000; i++)
                {
                    int ii = i;
                }

                HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
                HOperatorSet.Intensity(ho_Image, ho_Image, out hv_Mean, out hv_Deviation);

                if (hv_Height.I > 100)
                {
                    mCommand = Event.CommandResult.OK;
                    mResult = true;
                }
            }
            catch (Exception ex)
            {
                mCommand = Event.CommandResult.ER;
                LogRecord.LogHelper.Error(typeof(ImageProcessing), string.Format("{0}", ex));
            }

            result = new Event.EventArgsStation1(id, imgID, mCommand, mResult);
        }



        public void findModel(HObject ho_Image, HObject ho_DomainRegion_01, HObject ho_DomainRegion_02,
            HObject ho_ModelRegion_01, HObject ho_ModelContours_01, HObject ho_ModelRegion_02, HObject ho_ModelContours_02,
            out HObject ho_ContoursAffinTrans_01,out HObject ho_RegionAffinTrans_01,
            out HObject ho_ContoursAffinTrans_02, out HObject ho_RegionAffinTrans_02,
            HTuple hv_ModelId_01, HTuple hv_ModelId_02, 
            HTuple hv_CenterRow_01, HTuple hv_CenterColumn_01, HTuple hv_CenterRow_02, HTuple hv_CenterColumn_02, HTuple hv_AngleModel,
            out HTuple hv_HomMat2D,out bool hv_Result)
        {
            // Local iconic variables 

            HObject ho_RegionMoved_01 = null, ho_RegionMoved_02 = null;
            HObject ho_ImageReduce_01 = null, ho_ImageReduce_02 = null;
            // Local control variables 

            HTuple hv_MatchRow_01 = null, hv_MatchColumn_01 = null;
            HTuple hv_MatchAngle_01 = null, hv_MatchScore_01 = null;
            HTuple hv_MatchRow_02 = null, hv_MatchColumn_02 = null;
            HTuple hv_MatchAngle_02 = null, hv_MatchScore_02 = null;
            HTuple hv_HomMat2D_01 = new HTuple(), hv_HomMat2D_02 = new HTuple();
            HTuple hv_Angle = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_ImageReduce_01);
            HOperatorSet.GenEmptyObj(out ho_ImageReduce_02);
            HOperatorSet.GenEmptyObj(out ho_ContoursAffinTrans_01);
            HOperatorSet.GenEmptyObj(out ho_RegionAffinTrans_01);
            HOperatorSet.GenEmptyObj(out ho_ContoursAffinTrans_02);
            HOperatorSet.GenEmptyObj(out ho_RegionAffinTrans_02);
            HOperatorSet.GenEmptyObj(out ho_RegionMoved_01);
            HOperatorSet.GenEmptyObj(out ho_RegionMoved_02);
            hv_HomMat2D = new HTuple();

            ho_ImageReduce_01.Dispose();
            HOperatorSet.ReduceDomain(ho_Image, ho_DomainRegion_01, out ho_ImageReduce_01);
            ho_ImageReduce_02.Dispose();
            HOperatorSet.ReduceDomain(ho_Image, ho_DomainRegion_02, out ho_ImageReduce_02);

            HOperatorSet.FindShapeModel(ho_ImageReduce_01, hv_ModelId_01, (new HTuple(0)).TupleRad()
                , (new HTuple(360)).TupleRad(), 0.5, 1, 0.5, "least_squares", (new HTuple(4)).TupleConcat(
                1), 0.65, out hv_MatchRow_01, out hv_MatchColumn_01, out hv_MatchAngle_01,
                out hv_MatchScore_01);

            HOperatorSet.FindShapeModel(ho_ImageReduce_02, hv_ModelId_02, (new HTuple(0)).TupleRad()
                , (new HTuple(360)).TupleRad(), 0.5, 1, 0.5, "least_squares", (new HTuple(4)).TupleConcat(
                1), 0.65, out hv_MatchRow_02, out hv_MatchColumn_02, out hv_MatchAngle_02,
                out hv_MatchScore_02);

            hv_Result = false;

            if ((int)((new HTuple((new HTuple(hv_MatchScore_01.TupleLength())).TupleEqual(
                1))).TupleAnd(new HTuple((new HTuple(hv_MatchScore_02.TupleLength())).TupleEqual(
                1)))) != 0)
            {

                ho_RegionMoved_01.Dispose();
                HOperatorSet.MoveRegion(ho_ModelRegion_01, out ho_RegionMoved_01, -hv_CenterRow_01,
                    -hv_CenterColumn_01);
                ho_RegionMoved_02.Dispose();
                HOperatorSet.MoveRegion(ho_ModelRegion_02, out ho_RegionMoved_02, -hv_CenterRow_02,
                    -hv_CenterColumn_02);

                HOperatorSet.VectorAngleToRigid(0, 0, 0, hv_MatchRow_01, hv_MatchColumn_01,
                    hv_MatchAngle_01, out hv_HomMat2D_01);
                ho_ContoursAffinTrans_01.Dispose();
                HOperatorSet.AffineTransContourXld(ho_ModelContours_01, out ho_ContoursAffinTrans_01,
                    hv_HomMat2D_01);
                ho_RegionAffinTrans_01.Dispose();
                HOperatorSet.AffineTransRegion(ho_RegionMoved_01, out ho_RegionAffinTrans_01,
                    hv_HomMat2D_01, "nearest_neighbor");

                HOperatorSet.VectorAngleToRigid(0, 0, 0, hv_MatchRow_02, hv_MatchColumn_02,
                    hv_MatchAngle_02, out hv_HomMat2D_02);
                ho_ContoursAffinTrans_02.Dispose();
                HOperatorSet.AffineTransContourXld(ho_ModelContours_02, out ho_ContoursAffinTrans_02,
                    hv_HomMat2D_02);
                ho_RegionAffinTrans_02.Dispose();
                HOperatorSet.AffineTransRegion(ho_RegionMoved_02, out ho_RegionAffinTrans_02,
                    hv_HomMat2D_02, "nearest_neighbor");


                HOperatorSet.AngleLx(hv_MatchRow_01, hv_MatchColumn_01, hv_MatchRow_02, hv_MatchColumn_02,
                    out hv_Angle);
                HOperatorSet.VectorAngleToRigid(0, 0, hv_AngleModel, hv_MatchRow_01, hv_MatchColumn_01,
                    hv_Angle, out hv_HomMat2D);

                //affine_trans_pixel (HomMat2D, Rows, Cols, RowTrans, ColTrans)
                //gen_circle_contour_xld (ContCircle1, RowTrans, ColTrans, Circle_Radius, 0, 6.28318, 'positive', 1)
                hv_Result = true;
            }
            ho_RegionMoved_01.Dispose();
            ho_RegionMoved_02.Dispose();

            return;
        }



        public ImageProcessing()
        {

        }
        
    }
}
