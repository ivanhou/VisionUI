using Chloe.Core;
using Chloe.DbExpressions;
using Chloe.InternalExtensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;


namespace Chloe.SQLite
{
    partial class SqlGenerator : DbExpressionVisitor<DbExpression>
    {
        static Dictionary<string, Action<DbMethodCallExpression, SqlGenerator>> InitMethodHandlers()
        {
            var methodHandlers = new Dictionary<string, Action<DbMethodCallExpression, SqlGenerator>>();

            methodHandlers.Add("Equals", Method_Equals);

            methodHandlers.Add("Trim", Method_Trim);
            methodHandlers.Add("TrimStart", Method_TrimStart);
            methodHandlers.Add("TrimEnd", Method_TrimEnd);
            methodHandlers.Add("StartsWith", Method_StartsWith);
            methodHandlers.Add("EndsWith", Method_EndsWith);
            methodHandlers.Add("ToUpper", Method_String_ToUpper);
            methodHandlers.Add("ToLower", Method_String_ToLower);
            methodHandlers.Add("Substring", Method_String_Substring);
            methodHandlers.Add("IsNullOrEmpty", Method_String_IsNullOrEmpty);

            methodHandlers.Add("Contains", Method_Contains);

            methodHandlers.Add("Count", Method_Count);
            methodHandlers.Add("LongCount", Method_LongCount);
            methodHandlers.Add("Sum", Method_Sum);
            methodHandlers.Add("Max", Method_Max);
            methodHandlers.Add("Min", Method_Min);
            methodHandlers.Add("Average", Method_Average);

            methodHandlers.Add("AddYears", Method_DateTime_AddYears);
            methodHandlers.Add("AddMonths", Method_DateTime_AddMonths);
            methodHandlers.Add("AddDays", Method_DateTime_AddDays);
            methodHandlers.Add("AddHours", Method_DateTime_AddHours);
            methodHandlers.Add("AddMinutes", Method_DateTime_AddMinutes);
            methodHandlers.Add("AddSeconds", Method_DateTime_AddSeconds);

            methodHandlers.Add("Parse", Method_Parse);

            methodHandlers.Add("NewGuid", Method_Guid_NewGuid);

            methodHandlers.Add("DiffYears", Method_DbFunctions_DiffYears);
            methodHandlers.Add("DiffMonths", Method_DbFunctions_DiffMonths);
            methodHandlers.Add("DiffDays", Method_DbFunctions_DiffDays);
            methodHandlers.Add("DiffHours", Method_DbFunctions_DiffHours);
            methodHandlers.Add("DiffMinutes", Method_DbFunctions_DiffMinutes);
            methodHandlers.Add("DiffSeconds", Method_DbFunctions_DiffSeconds);
            methodHandlers.Add("DiffMilliseconds", Method_DbFunctions_DiffMilliseconds);
            methodHandlers.Add("DiffMicroseconds", Method_DbFunctions_DiffMicroseconds);

            var ret = Utils.Clone(methodHandlers);
            return ret;
        }

        static void Method_Equals(DbMethodCallExpression exp, SqlGenerator generator)
        {
            if (exp.Method.ReturnType != UtilConstants.TypeOfBoolean || exp.Method.IsStatic || exp.Method.GetParameters().Length != 1)
                throw UtilExceptions.NotSupportedMethod(exp.Method);

            DbExpression right = exp.Arguments[0];
            if (right.Type != exp.Object.Type)
            {
                right = DbExpression.Convert(right, exp.Object.Type);
            }

            DbExpression.Equal(exp.Object, right).Accept(generator);
        }

        static void Method_Trim(DbMethodCallExpression exp, SqlGenerator generator)
        {
            EnsureMethod(exp, UtilConstants.MethodInfo_String_Trim);

            generator._sqlBuilder.Append("TRIM(");
            exp.Object.Accept(generator);
            generator._sqlBuilder.Append(")");
        }
        static void Method_TrimStart(DbMethodCallExpression exp, SqlGenerator generator)
        {
            EnsureMethod(exp, UtilConstants.MethodInfo_String_TrimStart);
            EnsureTrimCharArgumentIsSpaces(exp.Arguments[0]);

            generator._sqlBuilder.Append("LTRIM(");
            exp.Object.Accept(generator);
            generator._sqlBuilder.Append(")");
        }
        static void Method_TrimEnd(DbMethodCallExpression exp, SqlGenerator generator)
        {
            EnsureMethod(exp, UtilConstants.MethodInfo_String_TrimEnd);
            EnsureTrimCharArgumentIsSpaces(exp.Arguments[0]);

            generator._sqlBuilder.Append("RTRIM(");
            exp.Object.Accept(generator);
            generator._sqlBuilder.Append(")");
        }
        static void Method_StartsWith(DbMethodCallExpression exp, SqlGenerator generator)
        {
            EnsureMethod(exp, UtilConstants.MethodInfo_String_StartsWith);

            exp.Object.Accept(generator);
            generator._sqlBuilder.Append(" LIKE ");
            exp.Arguments.First().Accept(generator);
            generator._sqlBuilder.Append(" || '%'");
        }
        static void Method_EndsWith(DbMethodCallExpression exp, SqlGenerator generator)
        {
            EnsureMethod(exp, UtilConstants.MethodInfo_String_EndsWith);

            exp.Object.Accept(generator);
            generator._sqlBuilder.Append(" LIKE '%' || ");
            exp.Arguments.First().Accept(generator);
        }
        static void Method_String_Contains(DbMethodCallExpression exp, SqlGenerator generator)
        {
            EnsureMethod(exp, UtilConstants.MethodInfo_String_Contains);

            exp.Object.Accept(generator);
            generator._sqlBuilder.Append(" LIKE '%' || ");
            exp.Arguments.First().Accept(generator);
            generator._sqlBuilder.Append(" || '%'");
        }
        static void Method_String_ToUpper(DbMethodCallExpression exp, SqlGenerator generator)
        {
            EnsureMethod(exp, UtilConstants.MethodInfo_String_ToUpper);

            generator._sqlBuilder.Append("UPPER(");
            exp.Object.Accept(generator);
            generator._sqlBuilder.Append(")");
        }
        static void Method_String_ToLower(DbMethodCallExpression exp, SqlGenerator generator)
        {
            EnsureMethod(exp, UtilConstants.MethodInfo_String_ToLower);

            generator._sqlBuilder.Append("LOWER(");
            exp.Object.Accept(generator);
            generator._sqlBuilder.Append(")");
        }
        static void Method_String_Substring(DbMethodCallExpression exp, SqlGenerator generator)
        {
            generator._sqlBuilder.Append("SUBSTR(");
            exp.Object.Accept(generator);
            generator._sqlBuilder.Append(",");
            exp.Arguments[0].Accept(generator);
            generator._sqlBuilder.Append(" + 1");

            if (exp.Method == UtilConstants.MethodInfo_String_Substring_Int32)
            {

            }
            else if (exp.Method == UtilConstants.MethodInfo_String_Substring_Int32_Int32)
            {
                generator._sqlBuilder.Append(",");
                exp.Arguments[1].Accept(generator);
            }
            else
                throw UtilExceptions.NotSupportedMethod(exp.Method);

            generator._sqlBuilder.Append(")");
        }
        static void Method_String_IsNullOrEmpty(DbMethodCallExpression exp, SqlGenerator generator)
        {
            EnsureMethod(exp, UtilConstants.MethodInfo_String_IsNullOrEmpty);

            DbExpression e = exp.Arguments.First();
            DbEqualExpression equalNullExpression = DbExpression.Equal(e, DbExpression.Constant(null, UtilConstants.TypeOfString));
            DbEqualExpression equalEmptyExpression = DbExpression.Equal(e, DbExpression.Constant(string.Empty));

            DbOrExpression orExpression = DbExpression.Or(equalNullExpression, equalEmptyExpression);

            DbCaseWhenExpression.WhenThenExpressionPair whenThenPair = new DbCaseWhenExpression.WhenThenExpressionPair(orExpression, DbConstantExpression.One);

            List<DbCaseWhenExpression.WhenThenExpressionPair> whenThenExps = new List<DbCaseWhenExpression.WhenThenExpressionPair>(1);
            whenThenExps.Add(whenThenPair);

            DbCaseWhenExpression caseWhenExpression = DbExpression.CaseWhen(whenThenExps, DbConstantExpression.Zero, UtilConstants.TypeOfBoolean);

            var eqExp = DbExpression.Equal(caseWhenExpression, DbConstantExpression.One);
            eqExp.Accept(generator);
        }

        static void Method_Contains(DbMethodCallExpression exp, SqlGenerator generator)
        {
            MethodInfo method = exp.Method;

            if (method.DeclaringType == UtilConstants.TypeOfString)
            {
                Method_String_Contains(exp, generator);
                return;
            }

            List<DbExpression> exps = new List<DbExpression>();
            IEnumerable values = null;
            DbExpression operand = null;

            Type declaringType = method.DeclaringType;

            if (typeof(IList).IsAssignableFrom(declaringType) || (declaringType.IsGenericType() && typeof(ICollection<>).MakeGenericType(declaringType.GetGenericArguments()).IsAssignableFrom(declaringType)))
            {
                DbMemberExpression memberExp = exp.Object as DbMemberExpression;

                if (memberExp == null || !memberExp.IsEvaluable())
                    throw new NotSupportedException(exp.ToString());

                values = DbExpressionExtension.Evaluate(memberExp) as IEnumerable; //Enumerable
                operand = exp.Arguments[0];
                goto constructInState;
            }
            if (method.IsStatic && declaringType == typeof(Enumerable) && exp.Arguments.Count == 2)
            {
                DbMemberExpression memberExp = exp.Arguments[0] as DbMemberExpression;

                if (memberExp == null || !memberExp.IsEvaluable())
                    throw new NotSupportedException(exp.ToString());

                values = DbExpressionExtension.Evaluate(memberExp) as IEnumerable;
                operand = exp.Arguments[1];
                goto constructInState;
            }

            throw UtilExceptions.NotSupportedMethod(exp.Method);

        constructInState:
            foreach (object value in values)
            {
                if (value == null)
                    exps.Add(DbExpression.Constant(null, operand.Type));
                else
                    exps.Add(DbExpression.Parameter(value));
            }
            In(generator, exps, operand);
        }


        static void In(SqlGenerator generator, List<DbExpression> elementExps, DbExpression operand)
        {
            if (elementExps.Count == 0)
            {
                generator._sqlBuilder.Append("1=0");
                return;
            }

            operand.Accept(generator);
            generator._sqlBuilder.Append(" IN (");

            for (int i = 0; i < elementExps.Count; i++)
            {
                if (i > 0)
                    generator._sqlBuilder.Append(",");

                elementExps[i].Accept(generator);
            }

            generator._sqlBuilder.Append(")");

            return;
        }

        static void Method_Count(DbMethodCallExpression exp, SqlGenerator generator)
        {
            EnsureMethodDeclaringType(exp, typeof(AggregateFunctions));
            Aggregate_Count(generator);
        }
        static void Method_LongCount(DbMethodCallExpression exp, SqlGenerator generator)
        {
            EnsureMethodDeclaringType(exp, typeof(AggregateFunctions));
            Aggregate_LongCount(generator);
        }
        static void Method_Sum(DbMethodCallExpression exp, SqlGenerator generator)
        {
            EnsureMethodDeclaringType(exp, typeof(AggregateFunctions));
            Aggregate_Sum(generator, exp.Arguments.First(), exp.Method.ReturnType);
        }
        static void Method_Max(DbMethodCallExpression exp, SqlGenerator generator)
        {
            EnsureMethodDeclaringType(exp, typeof(AggregateFunctions));
            Aggregate_Max(generator, exp.Arguments.First(), exp.Method.ReturnType);
        }
        static void Method_Min(DbMethodCallExpression exp, SqlGenerator generator)
        {
            EnsureMethodDeclaringType(exp, typeof(AggregateFunctions));
            Aggregate_Min(generator, exp.Arguments.First(), exp.Method.ReturnType);
        }
        static void Method_Average(DbMethodCallExpression exp, SqlGenerator generator)
        {
            EnsureMethodDeclaringType(exp, typeof(AggregateFunctions));
            Aggregate_Average(generator, exp.Arguments.First(), exp.Method.ReturnType);
        }


        static void Method_DateTime_AddYears(DbMethodCallExpression exp, SqlGenerator generator)
        {
            EnsureMethodDeclaringType(exp, UtilConstants.TypeOfDateTime);

            DbFunction_DATEADD(generator, "years", exp);
        }
        static void Method_DateTime_AddMonths(DbMethodCallExpression exp, SqlGenerator generator)
        {
            EnsureMethodDeclaringType(exp, UtilConstants.TypeOfDateTime);

            DbFunction_DATEADD(generator, "months", exp);
        }
        static void Method_DateTime_AddDays(DbMethodCallExpression exp, SqlGenerator generator)
        {
            EnsureMethodDeclaringType(exp, UtilConstants.TypeOfDateTime);

            DbFunction_DATEADD(generator, "days", exp);
        }
        static void Method_DateTime_AddHours(DbMethodCallExpression exp, SqlGenerator generator)
        {
            EnsureMethodDeclaringType(exp, UtilConstants.TypeOfDateTime);

            DbFunction_DATEADD(generator, "hours", exp);
        }
        static void Method_DateTime_AddMinutes(DbMethodCallExpression exp, SqlGenerator generator)
        {
            EnsureMethodDeclaringType(exp, UtilConstants.TypeOfDateTime);

            DbFunction_DATEADD(generator, "minutes", exp);
        }
        static void Method_DateTime_AddSeconds(DbMethodCallExpression exp, SqlGenerator generator)
        {
            EnsureMethodDeclaringType(exp, UtilConstants.TypeOfDateTime);

            DbFunction_DATEADD(generator, "seconds", exp);
        }

        static void Method_Parse(DbMethodCallExpression exp, SqlGenerator generator)
        {
            if (exp.Arguments.Count != 1)
                throw UtilExceptions.NotSupportedMethod(exp.Method);

            DbExpression arg = exp.Arguments[0];
            if (arg.Type != UtilConstants.TypeOfString)
                throw UtilExceptions.NotSupportedMethod(exp.Method);

            Type retType = exp.Method.ReturnType;
            EnsureMethodDeclaringType(exp, retType);

            DbExpression e = DbExpression.Convert(arg, retType);
            e.Accept(generator);
        }

        static void Method_Guid_NewGuid(DbMethodCallExpression exp, SqlGenerator generator)
        {
            EnsureMethod(exp, UtilConstants.MethodInfo_Guid_NewGuid);

            throw UtilExceptions.NotSupportedMethod(exp.Method);
        }


        static void Method_DbFunctions_DiffYears(DbMethodCallExpression exp, SqlGenerator generator)
        {
            EnsureMethod(exp, UtilConstants.MethodInfo_DbFunctions_DiffYears);

            Append_DiffYears(generator, exp.Arguments[0], exp.Arguments[1]);
        }
        static void Method_DbFunctions_DiffMonths(DbMethodCallExpression exp, SqlGenerator generator)
        {
            EnsureMethod(exp, UtilConstants.MethodInfo_DbFunctions_DiffMonths);

            DbExpression startDateTimeExp = exp.Arguments[0];
            DbExpression endDateTimeExp = exp.Arguments[1];

            /*
             * This method will generating sql like following:
              (cast(STRFTIME('%Y','2016-07-06 09:01:24') as INTEGER) - cast(STRFTIME('%Y','2015-08-06 09:01:24') as INTEGER)) * 12  + (cast(STRFTIME('%m','2016-07-06 09:01:24') as INTEGER) - cast(STRFTIME('%m','2015-08-06 09:01:24') as INTEGER))
             */

            generator._sqlBuilder.Append("(");

            /* (cast(STRFTIME('%Y','2016-07-06 09:01:24') as INTEGER) - cast(STRFTIME('%Y','2015-08-06 09:01:24') as INTEGER)) * 12 */
            Append_DiffYears(generator, startDateTimeExp, endDateTimeExp);
            generator._sqlBuilder.Append(" * 12");

            generator._sqlBuilder.Append(" + ");

            /* (cast(STRFTIME('%m','2016-07-06 09:01:24') as INTEGER) - cast(STRFTIME('%m','2015-08-06 09:01:24') as INTEGER)) */
            generator._sqlBuilder.Append("(");
            DbFunction_DATEPART(generator, "m", endDateTimeExp);
            generator._sqlBuilder.Append(" - ");
            DbFunction_DATEPART(generator, "m", startDateTimeExp);
            generator._sqlBuilder.Append(")");

            generator._sqlBuilder.Append(")");
        }
        static void Method_DbFunctions_DiffDays(DbMethodCallExpression exp, SqlGenerator generator)
        {
            EnsureMethod(exp, UtilConstants.MethodInfo_DbFunctions_DiffDays);

            Append_DateDiff(generator, exp.Arguments[0], exp.Arguments[1], null);
        }
        static void Method_DbFunctions_DiffHours(DbMethodCallExpression exp, SqlGenerator generator)
        {
            EnsureMethod(exp, UtilConstants.MethodInfo_DbFunctions_DiffHours);

            Append_DateDiff(generator, exp.Arguments[0], exp.Arguments[1], 24);
        }
        static void Method_DbFunctions_DiffMinutes(DbMethodCallExpression exp, SqlGenerator generator)
        {
            EnsureMethod(exp, UtilConstants.MethodInfo_DbFunctions_DiffMinutes);

            Append_DateDiff(generator, exp.Arguments[0], exp.Arguments[1], 24 * 60);
        }
        static void Method_DbFunctions_DiffSeconds(DbMethodCallExpression exp, SqlGenerator generator)
        {
            EnsureMethod(exp, UtilConstants.MethodInfo_DbFunctions_DiffSeconds);

            Append_DateDiff(generator, exp.Arguments[0], exp.Arguments[1], 24 * 60 * 60);
        }
        static void Method_DbFunctions_DiffMilliseconds(DbMethodCallExpression exp, SqlGenerator generator)
        {
            EnsureMethod(exp, UtilConstants.MethodInfo_DbFunctions_DiffMilliseconds);

            throw UtilExceptions.NotSupportedMethod(exp.Method);
        }
        static void Method_DbFunctions_DiffMicroseconds(DbMethodCallExpression exp, SqlGenerator generator)
        {
            EnsureMethod(exp, UtilConstants.MethodInfo_DbFunctions_DiffMicroseconds);

            throw UtilExceptions.NotSupportedMethod(exp.Method);
        }

        static void Append_JULIANDAY(SqlGenerator generator, DbExpression startDateTimeExp, DbExpression endDateTimeExp)
        {
            /* (JULIANDAY(endDateTimeExp)- JULIANDAY(startDateTimeExp)) */

            generator._sqlBuilder.Append("(");

            generator._sqlBuilder.Append("JULIANDAY(");
            endDateTimeExp.Accept(generator);
            generator._sqlBuilder.Append(")");

            generator._sqlBuilder.Append(" - ");

            generator._sqlBuilder.Append("JULIANDAY(");
            startDateTimeExp.Accept(generator);
            generator._sqlBuilder.Append(")");

            generator._sqlBuilder.Append(")");
        }
        static void Append_DiffYears(SqlGenerator generator, DbExpression startDateTimeExp, DbExpression endDateTimeExp)
        {
            /* (CAST(STRFTIME('%Y',endDateTimeExp) as INTEGER) - CAST(STRFTIME('%Y',startDateTimeExp) as INTEGER)) */

            generator._sqlBuilder.Append("(");
            DbFunction_DATEPART(generator, "Y", endDateTimeExp);
            generator._sqlBuilder.Append(" - ");
            DbFunction_DATEPART(generator, "Y", startDateTimeExp);
            generator._sqlBuilder.Append(")");
        }
        static void Append_DateDiff(SqlGenerator generator, DbExpression startDateTimeExp, DbExpression endDateTimeExp, int? multiplier)
        {
            /* CAST((JULIANDAY(endDateTimeExp)- JULIANDAY(startDateTimeExp)) AS INTEGER) */
            /* OR */
            /* CAST((JULIANDAY(endDateTimeExp)- JULIANDAY(startDateTimeExp)) * multiplier AS INTEGER) */

            generator._sqlBuilder.Append("CAST(");

            Append_JULIANDAY(generator, startDateTimeExp, endDateTimeExp);
            if (multiplier != null)
                generator._sqlBuilder.Append(" * ", multiplier.Value.ToString());

            generator._sqlBuilder.Append(" AS INTEGER)");
        }
    }
}
