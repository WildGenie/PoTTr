/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */

using DeepEqual.Syntax;
using NUnit.Framework.Constraints;

namespace PoTTr.Tests
{
    internal class DeepEqualConstraint: Constraint
    {
        private object expected;

        public DeepEqualConstraint(object expected)
        {
            this.expected = expected;
        }

        public override ConstraintResult ApplyTo<TActual>(TActual actual)
        {
            return new ConstraintResult(this, actual, expected.IsDeepEqual(actual));
        }
    }

    internal class Is : NUnit.Framework.Is
    {
        public static DeepEqualConstraint DeepEqualTo(object expected)
        {
            return new DeepEqualConstraint(expected);
        }
    }

    internal static class CustomConstraintExtensions
    {
        public static DeepEqualConstraint DeepEqualTo(this ConstraintExpression expression, object expected)
        {
            var constraint = new DeepEqualConstraint(expected);
            expression.Append(constraint);
            return constraint;
        }
    }
}
