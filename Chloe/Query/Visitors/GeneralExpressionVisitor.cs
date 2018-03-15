using Chloe.Core.Visitors;
using Chloe.DbExpressions;
using Chloe.Extensions;
using Chloe.Infrastructure;
using Chloe.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Chloe.Query.Visitors
{
    internal class GeneralExpressionVisitor : ExpressionVisitorBase
    {
        LambdaExpression _lambda;
        List<IMappingObjectExpression> _moeList;

        GeneralExpressionVisitor(List<IMappingObjectExpression> moeList)
        {
            this._moeList = moeList;
        }
        public GeneralExpressionVisitor(LambdaExpression lambda, List<IMappingObjectExpression> moeList)
        {
            this._lambda = lambda;
            this._moeList = moeList;
        }
        public static DbExpression ParseLambda(LambdaExpression lambda, List<IMappingObjectExpression> moeList)
        {
            GeneralExpressionVisitor visitor = new GeneralExpressionVisitor(moeList);
            return visitor.Visit(lambda);
        }

        int FindParameterIndex(ParameterExpression exp)
        {
            int idx = this._lambda.Parameters.IndexOf(exp);
            if (idx == -1)
            {
                throw new Exception("Can not find the ParameterExpression index");
            }

            return idx;
        }

        protected override DbExpression VisitLambda(LambdaExpression lambda)
        {
            this._lambda = lambda;
            return base.VisitLambda(lambda);
        }

        protected override DbExpression VisitMemberAccess(MemberExpression exp)
        {
            ParameterExpression p;
            if (ExpressionExtension.IsDerivedFromParameter(exp, out p))
            {
                int idx = this.FindParameterIndex(p);

                IMappingObjectExpression moe = this._moeList[idx];
                return moe.GetDbExpression(exp);
            }
            else
            {
                return base.VisitMemberAccess(exp);
            }
        }

        protected override DbExpression VisitParameter(ParameterExpression exp)
        {
            //TODO 只支持 MappingFieldExpression 类型，即类似 q.Select(a=> a.Id).Where(a=> a > 0) 这种情况，也就是 ParameterExpression.Type 为基本映射类型。

            if (MappingTypeSystem.IsMappingType(exp.Type))
            {
                int idx = this.FindParameterIndex(exp);
                IMappingObjectExpression moe = this._moeList[idx];
                MappingFieldExpression mfe = (MappingFieldExpression)moe;
                return mfe.Expression;
            }
            else
                throw new NotSupportedException(exp.ToString());
        }
    }
}
