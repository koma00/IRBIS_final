/* PftConditionalStatement.
 */

#region Using directives

using System;

#endregion

namespace ManagedClient.Pft
{
    /// <summary>
    /// �������� ��������: if then else fi.
    /// </summary>
    [Serializable]
    public sealed class PftConditionalStatement
        : PftAst
    {
        #region Properties

        /// <summary>
        /// ���������� �������.
        /// </summary>
        public PftCondition Condition { get; set; }

        /// <summary>
        /// ����� �����.
        /// </summary>
        public PftStatement ThenStatement { get; set; }

        /// <summary>
        /// ����� �����.
        /// </summary>
        public PftStatement ElseStatement { get; set; }

        #endregion

        #region Construction

        public PftConditionalStatement(PftParser.ConditionalStatementContext node)
            : base(node)
        {
            PftParser.ConditionContext context = node.condition();
            Condition = PftCondition.DispatchContext(context);
            ThenStatement = new PftStatement(node.thenBranch);
            if (node.elseBranch != null)
            {
                ElseStatement = new PftStatement(node.elseBranch);
            }
        }

        #endregion

        #region PftAst members

        public override void Execute
            (
                PftContext context
            )
        {
            if (Condition.Evaluate(context))
            {
                if (ThenStatement != null)
                {
                    ThenStatement.Execute(context);
                }
            }
            else
            {
                if (ElseStatement != null)
                {
                    ElseStatement.Execute(context);
                }
            }
        }

        #endregion
    }
}