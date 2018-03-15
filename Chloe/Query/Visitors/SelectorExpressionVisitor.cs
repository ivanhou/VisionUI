using System;
using System.Linq.Expressions;
using System.Reflection;
using Chloe.InternalExtensions;
using Chloe.Utility;
using Chloe.DbExpressions;
using Chloe.Query.Visitors;
using System.Collections.Generic;
using Chloe.Core.Visitors;
using Chloe.Extensions;
using Chloe.Infrastructure;

namespace Chloe.Query
{
    class SelectorExpressionVisitor : ExpressionVisitor<IMappingObjectExpression>
    {
        ExpressionVisitorBase _visitor;
        LambdaExpression _lambda;
        List<IMappingObjectExpression> _moeList;
        SelectorExpressionVisitor(List<IMappingObjectExpression> moeList)
        {
            this._moeList = moeList;
        }

        public static IMappingObjectExpression VisitSelectExpression(LambdaExpression exp, List<IMappingObjectExpression> moeList)
        {
            SelectorExpressionVisitor visitor = new SelectorExpressionVisitor(moeList);
            return visitor.Visit(exp);
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
        DbExpression VisistExpression(Expression exp)
        {
            return this._visitor.Visit(exp);
        }
        IMappingObjectExpression VisitNavigationMember(MemberExpression exp)
        {
            ParameterExpression p;
            if (ExpressionExtension.IsDerivedFromParameter(exp, out p))
            {
                int idx = this.FindParameterIndex(p);
                IMappingObjectExpression moe = this._moeList[idx];
                return moe.GetNavMemberExpression(exp);
            }
            else
            {
                throw new Exception();
            }
        }

        public override IMappingObjectExpression Visit(Expression exp)
        {
            if (exp == null)
                return default(IMappingObjectExpression);
            switch (exp.NodeType)
            {
                case ExpressionType.Lambda:
                    return this.VisitLambda((LambdaExpression)exp);
                case ExpressionType.New:
                    return this.VisitNew((NewExpression)exp);
                case ExpressionType.MemberInit:
                    return this.VisitMemberInit((MemberInitExpression)exp);
                case ExpressionType.MemberAccess:
                    return this.VisitMemberAccess((MemberExpression)exp);
                case ExpressionType.Parameter:
                    return this.VisitParameter((ParameterExpression)exp);
                default:
                    return this.VisistMapTypeSelector(exp);
            }
        }

        protected override IMappingObjectExpression VisitLambda(LambdaExpression exp)
        {
            this._lambda = exp;
            this._visitor = new GeneralExpressionVisitor(exp, this._moeList);
            return this.Visit(exp.Body);
        }
        protected override IMappingObjectExpression VisitNew(NewExpression exp)
        {
            IMappingObjectExpression result = new MappingObjectExpression(exp.Constructor);
            ParameterInfo[] parames = exp.Constructor.GetParameters();
            for (int i = 0; i < parames.Length; i++)
            {
                ParameterInfo pi = parames[i];
                Expression argExp = exp.Arguments[i];
                if (MappingTypeSystem.IsMappingType(pi.ParameterType))
                {
                    DbExpression dbExpression = this.VisistExpression(argExp);
                    result.AddConstructorParameter(pi, dbExpression);
                }
                else
                {
                    IMappingObjectExpression subResult = this.Visit(argExp);
                    result.AddConstructorEntityParameter(pi, subResult);
                }
            }

            return result;
        }
        protected override IMappingObjectExpression VisitMemberInit(MemberInitExpression exp)
        {
            IMappingObjectExpression result = this.Visit(exp.NewExpression);

            foreach (MemberBinding binding in exp.Bindings)
            {
                if (binding.BindingType != MemberBindingType.Assignment)
                {
                    throw new NotSupportedException();
                }

                MemberAssignment memberAssignment = (MemberAssignment)binding;
                MemberInfo member = memberAssignment.Member;
                Type memberType = member.GetMemberType();

                //是数据库映射类型
                if (MappingTypeSystem.IsMappingType(memberType))
                {
                    DbExpression dbExpression = this.VisistExpression(memberAssignment.Expression);
                    result.AddMemberExpression(member, dbExpression);
                }
                else
                {
                    //对于非数据库映射类型，只支持 NewExpression 和 MemberInitExpression
                    IMappingObjectExpression subResult = this.Visit(memberAssignment.Expression);
                    result.AddNavMemberExpression(member, subResult);
                }
            }

            return result;
        }
        /// <summary>
        /// a => a.Id   a => a.Name   a => a.User
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        protected override IMappingObjectExpression VisitMemberAccess(MemberExpression exp)
        {
            //create MappingFieldExpression object if exp is map type
            if (MappingTypeSystem.IsMappingType(exp.Type))
            {
                DbExpression dbExp = this.VisistExpression(exp);
                MappingFieldExpression ret = new MappingFieldExpression(exp.Type, dbExp);
                return ret;
            }

            //如 a.Order a.User 等形式
            return this.VisitNavigationMember(exp);
        }
        protected override IMappingObjectExpression VisitParameter(ParameterExpression exp)
        {
            int idx = this.FindParameterIndex(exp);
            IMappingObjectExpression moe = this._moeList[idx];
            return moe;
        }

        IMappingObjectExpression VisistMapTypeSelector(Expression exp)
        {
            if (!MappingTypeSystem.IsMappingType(exp.Type))
            {
                throw new NotSupportedException(exp.ToString());
            }

            DbExpression dbExp = this.VisistExpression(exp);
            MappingFieldExpression ret = new MappingFieldExpression(exp.Type, dbExp);
            return ret;
        }
    }
}
